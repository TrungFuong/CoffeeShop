using CoffeeShop.DTOs;
using CoffeeShop.DTOs.Request;
using CoffeeShop.DTOs.Responses;
using CoffeeShop.Models;
using CoffeeShop.UnitOfWork;
using Google.Cloud.Storage.V1;
using Microsoft.EntityFrameworkCore.Metadata.Conventions;
using Mono.TextTemplating;
using System.Linq.Expressions;

namespace CoffeeShop.Services.Implementations
{
    public class ReceiptService : IReceiptService
    {

        private readonly IUnitOfWork _unitOfWork;
        private readonly IReceiptDetailService _receiptDetailService;
        private readonly IProductService _productService;
        private readonly ICustomerService _customerService;
        private readonly ICartService _cartService;
        private readonly ICartDetailService _cartDetailService;

        public ReceiptService(IUnitOfWork unitOfWork, IReceiptDetailService receiptDetailService, IProductService productService, ICustomerService customerService, ICartService cartService, ICartDetailService cartDetailService)
        {
            _unitOfWork = unitOfWork;
            _receiptDetailService = receiptDetailService;
            _productService = productService;
            _customerService = customerService;
            _cartService = cartService;
            _cartDetailService = cartDetailService;
        }

        public async Task AddReceiptInfoAsync(Guid receiptId, ReceiptRequestDTO receiptRequestDTO)
        {
            var employee = await _unitOfWork.UserRepository.GetAsync(r => r.UserId == receiptRequestDTO.UserId);

            if (employee == null)
            {
                throw new KeyNotFoundException("Không tìm thấy người dùng!");
            }

            decimal receiptTotal = 0;

            foreach (var item in receiptRequestDTO.receiptDetailDTOs)
            {
                var product = await _unitOfWork.ProductRepository.GetAsync(p => p.ProductId == item.ProductId);
                receiptTotal += product.ProductPrice * item.ProductQuantity;
            }

            var receipt = new Receipt
            {
                ReceiptId = receiptId,
                UserId = receiptRequestDTO.UserId,
                CustomerId = receiptRequestDTO.CustomerId,
                ReceiptDate = receiptRequestDTO.ReceiptDate,
                ReceiptTotal = receiptTotal,
                Table = receiptRequestDTO.Table,
                IsDeleted = false
            };

            await _unitOfWork.ReceiptRepository.AddAsync(receipt);
        }

        public async Task<ReceiptResponseDTO> AddReceiptAsync(ReceiptRequestDTO receiptRequestDTO)
        {
            using (var transaction = await _unitOfWork.BeginTransactionAsync())
            {
                try
                {
                    Task addCustomerTask = null;
                    Task deleteCartTask = null;
                    Task deleteCartDetailTask = null;
                    var customerId = Guid.NewGuid();
                    if (receiptRequestDTO.CustomerPhone != null)
                    {
                        var existingCustomer = await _customerService.GetCustomerDetailAsync(receiptRequestDTO.CustomerPhone);
                        if (existingCustomer == null)
                        {
                            var customer = new CustomerRequestDTO
                            {
                                CustomerId = customerId,
                                CustomerName = receiptRequestDTO.CustomerName,
                                CustomerPhone = receiptRequestDTO.CustomerPhone,
                                CustomerBirthday = receiptRequestDTO.CustomerBirthday
                            };
                            addCustomerTask = _customerService.AddCustomerAsync(customer);
                            receiptRequestDTO.CustomerId = customerId;
                        }
                        else
                        {
                            receiptRequestDTO.CustomerId = existingCustomer.CustomerId;
                        }
                    }


                    var receiptId = Guid.NewGuid();
                    var addReceiptTask = AddReceiptInfoAsync(receiptId, receiptRequestDTO);
                    var addReceiptDetailTask = _receiptDetailService.AddReceiptDetailAsync(receiptId, receiptRequestDTO.receiptDetailDTOs);

                    var tasks = new List<Task> { addReceiptTask, addReceiptDetailTask };

                    var cart = await _unitOfWork.CartRepository.GetAsync(c => c.Table == receiptRequestDTO.Table);
                    if (cart != null)
                    {
                        deleteCartTask = _cartService.DeleteCartAsync(receiptRequestDTO.Table);
                        deleteCartDetailTask = _cartDetailService.DeleteCartDetailAsync(cart.CartId);
                        tasks.Add(deleteCartTask);
                        tasks.Add(deleteCartDetailTask);
                    }
                    if (addCustomerTask != null)
                    {
                        tasks.Add(addCustomerTask);
                    }

                    await Task.WhenAll(tasks);
                    await _unitOfWork.CommitAsync();
                    await transaction.CommitAsync();

                    return new ReceiptResponseDTO
                    {
                        ReceiptId = receiptId,
                        ReceiptDate = receiptRequestDTO.ReceiptDate
                    };
                }
                catch (Exception)
                {
                    await transaction.RollbackAsync();
                    throw new ArgumentException("Thêm hóa đơn thất bại!");
                }
            }
        }


        public async Task<(IEnumerable<ReceiptResponseDTO> data, int totalCount)> GetAllReceiptsAsync(
            int page,
            string? search,
            DateTime? receiptDate,
            string? sortOrder,
            string? sortBy = "receiptDate",
            string includeProperties = "")
        {
            Func<IQueryable<Receipt>, IOrderedQueryable<Receipt>>? orderBy = GetOrderQuery(sortOrder, sortBy);
            Expression<Func<Receipt, bool>> filter = await GetFilterQuery(search);

            var receipts = await _unitOfWork.ReceiptRepository.GetAllAsync(page, filter, orderBy, includeProperties);
            var receiptResponses = receipts.items.Select(r => new ReceiptResponseDTO
            {
                ReceiptId = r.ReceiptId,
                ReceiptDate = r.ReceiptDate,
                ReceiptTotal = r.ReceiptTotal,
                Table = r.Table,
                UserId = r.User.UserId,
                FullName = r.User.FirstName + " " + r.User.LastName,
                CustomerPhone = r.Customer?.CustomerPhone
            }).ToList();
            return (receiptResponses, receipts.totalCount);
        }


        public async Task<ReceiptResponseDTO> GetReceiptDetailAsync(Guid id)
        {
            var receipt = await _unitOfWork.ReceiptRepository.GetReceiptDetailAsync(id);
            if (receipt == null)
            {
                throw new KeyNotFoundException("Không tìm thấy hóa đơn!");
            }
            return new ReceiptResponseDTO
            {
                ReceiptId = receipt.ReceiptId,
                ReceiptDate = receipt.ReceiptDate,
                ReceiptTotal = receipt.ReceiptTotal,
                Table = receipt.Table,
                UserId = receipt.User.UserId,
                FullName = receipt.User.FirstName + " " + receipt.User.LastName,
                CustomerName = receipt.Customer.CustomerName,
                CustomerPhone = receipt.Customer.CustomerPhone,
                ReceiptDetails = receipt.ReceiptDetails.Select(rd => new ReceiptDetailResponseDTO
                {
                    ProductId = rd.ProductId,
                    ProductName = rd.Product.ProductName,
                    ProductPrice = rd.Product.ProductPrice,
                    ProductQuantity = rd.ProductQuantity
                }).ToList()
            };
        } 

        private async Task<Expression<Func<Receipt, bool>>>? GetFilterQuery(string? search)
        {
            // Determine the filtering criteria
            Expression<Func<Receipt, bool>>? filter = null;
            var parameter = Expression.Parameter(typeof(Receipt), "x");
            var conditions = new List<Expression>();

            // Add IsDelete
            var isDeletedCondition = Expression.Equal(Expression.Property(parameter, nameof(Receipt.IsDeleted)),
                Expression.Constant(false));
            conditions.Add(isDeletedCondition);

            // Add search conditions
            if (!string.IsNullOrEmpty(search))
            {
                var searchCondition =
                    Expression.Call(
                        Expression.Property(parameter, nameof(Receipt.User.FirstName)),
                        nameof(string.Contains),
                        Type.EmptyTypes,
                        Expression.Constant(search)
                    );
                conditions.Add(searchCondition);
            }


            // Combine all conditions with AndAlso
            if (conditions.Any())
            {
                var combinedCondition = conditions.Aggregate((left, right) => Expression.AndAlso(left, right));
                filter = Expression.Lambda<Func<Receipt, bool>>(combinedCondition, parameter);
            }
            return filter;
        }

        private Func<IQueryable<Receipt>, IOrderedQueryable<Receipt>>? GetOrderQuery(string? sortOrder, string? sortBy)
        {
            Func<IQueryable<Receipt>, IOrderedQueryable<Receipt>>? orderBy;
            switch (sortBy?.ToLower())
            {
                case "productname":
                    orderBy = x => sortOrder != "desc" ? x.OrderBy(r => r.ReceiptDetails.Select(rd => rd.Product.ProductName)) : x.OrderByDescending(r => r.ReceiptDetails.Select(rd => rd.Product.ProductName));
                    break;

                case "receiptdate":
                    orderBy = x => sortOrder != "desc" ? x.OrderBy(r => r.ReceiptDate) : x.OrderByDescending(r => r.ReceiptDate);
                    break;

                default:
                    orderBy = null;
                    break;
            }
            return orderBy;
        }
    }
}
