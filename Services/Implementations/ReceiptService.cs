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

        public ReceiptService(IUnitOfWork unitOfWork, IReceiptDetailService receiptDetailService, IProductService productService)
        {
            _unitOfWork = unitOfWork;
            _receiptDetailService = receiptDetailService;
            _productService = productService;
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
                    var receiptId = Guid.NewGuid();

                    var addReceiptTask = AddReceiptInfoAsync(receiptId, receiptRequestDTO);
                    var addReceiptDetailTask = _receiptDetailService.AddReceiptDetailAsync(receiptId, receiptRequestDTO.receiptDetailDTOs);

                    var tasks = new List<Task> { addReceiptTask, addReceiptDetailTask };

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
            string includeProperties = "",
            Guid? newReceiptId = null)
        {
            Func<IQueryable<Receipt>, IOrderedQueryable<Receipt>>? orderBy = GetOrderQuery(sortOrder, sortBy);
            Expression<Func<Receipt, bool>> filter = await GetFilterQuery(search);
            Expression<Func<Receipt, bool>> prioritizeCondition = null;

            if (newReceiptId != null)
            {
                prioritizeCondition = r => r.ReceiptId == newReceiptId;
            }
            var receipts = await _unitOfWork.ReceiptRepository.GetAllAsync(page, filter, orderBy, includeProperties);
            var receiptResponses = receipts.items.Select(r => new ReceiptResponseDTO
            {
                ReceiptId = r.ReceiptId,
                ReceiptDate = r.ReceiptDate,
                ReceiptTotal = r.ReceiptTotal,
                Table = r.Table,
                CustomerId = r.Customer?.CustomerId,
                UserId = r.User.UserId,
                FullName = r.User.FirstName + " " + r.User.LastName,
                CustomerPhone = r.Customer.CustomerPhone,
            }).ToList();
            return (receiptResponses, receipts.totalCount);
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
                        //REMEMBER TO FINISH
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
