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

        public ReceiptService(IUnitOfWork unitOfWork, IReceiptDetailService receiptDetailService)
        {
            _unitOfWork = unitOfWork;
            _receiptDetailService = receiptDetailService;
        }

        public async Task<ReceiptResponseDTO> AddReceiptAsync(ReceiptRequestDTO receiptRequestDTO)
        {
            var employee = await _unitOfWork.UserRepository.GetAsync(r => r.UserId == receiptRequestDTO.UserId);

            if (employee == null)
            {
                throw new KeyNotFoundException("Không tìm thấy người dùng!");
            }

            var receiptDetail = receiptRequestDTO.receiptDetailDTOs.Select(receiptRequestDTO => new ReceiptDetail
            {
                ReceiptId = receiptRequestDTO.ReceiptId,
                ProductId = receiptRequestDTO.ProductId,
                ProductQuantity = receiptRequestDTO.ProductQuantity
            }).ToList();

            var receiptTotal = receiptDetail.Sum(r => r.Product.ProductPrice * r.ProductQuantity);

            var receipt = new Receipt
            {
                UserId = receiptRequestDTO.UserId,
                CustomerId = receiptRequestDTO.CustomerId,
                ReceiptDate = receiptRequestDTO.ReceiptDate,
                ReceiptTotal = receiptTotal,
                Table = receiptRequestDTO.Table,
                IsDeleted = false,
                ReceiptDetails = receiptDetail
            };

            _unitOfWork.ReceiptRepository.AddAsync(receipt);

            foreach (var item in receiptDetail)
            {
                _unitOfWork.ReceiptDetailRepository.AddAsync(item);
            }

            if (await _unitOfWork.CommitAsync() < 1)
            {
                throw new ArgumentException("Thêm hóa đơn thất bại!");
            }
            else
            {
                return new ReceiptResponseDTO
                {
                    ReceiptId = receipt.ReceiptId,
                    ReceiptDate = receipt.ReceiptDate
                };
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
