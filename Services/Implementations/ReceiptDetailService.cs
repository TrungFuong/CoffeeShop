using CoffeeShop.DTOs;
using CoffeeShop.Models;
using CoffeeShop.UnitOfWork;

namespace CoffeeShop.Services.Implementations
{
    public class ReceiptDetailService : IReceiptDetailService
    {
        private readonly IUnitOfWork _unitOfWork;

        public ReceiptDetailService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<bool> AddReceiptDetailAsync(Guid receiptId, List<ReceiptDetailDTO> receiptDetailDTO)
        {
            var receipt = await _unitOfWork.ReceiptRepository.GetAsync(rd => rd.ReceiptId == receiptId);
            if (receipt == null)
            {
                throw new KeyNotFoundException("Không tìm thấy hóa đơn!");
            }

            var receiptDetails = receiptDetailDTO.Select(dto => new ReceiptDetail
            {
                ReceiptId = receiptId,
                ProductId = dto.ProductId,
                ProductQuantity = dto.ProductQuantity
            }).ToList();

            await _unitOfWork.ReceiptDetailRepository.AddRangeAsync(receiptDetails);

            decimal receiptTotal = 0;
            foreach (var item in receiptDetails)
            {
                item.Product = await _unitOfWork.ProductRepository.GetAsync(p => p.ProductId == item.ProductId);
                receiptTotal += item.Product.ProductPrice * item.ProductQuantity;
            }

            receipt.ReceiptTotal = receiptTotal;

            _unitOfWork.ReceiptRepository.Update(receipt);

            if (_unitOfWork.Commit() > 0)
            {
                return true;
            }
            else
            {
                throw new ArgumentException("Thêm chi tiết hóa đơn thất bại!");
            }
        }
    }
}
