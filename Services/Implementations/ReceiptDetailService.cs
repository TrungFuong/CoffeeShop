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

        public async void AddReceiptDetailAsync(ReceiptDetailDTO receiptDetailDTO)
        {
            var receiptDetail = new ReceiptDetail
            {
                ReceiptId = receiptDetailDTO.ReceiptId,
                ProductId = receiptDetailDTO.ProductId,
                ProductQuantity = receiptDetailDTO.ProductQuantity
            };

            await _unitOfWork.ReceiptDetailRepository.AddAsync(receiptDetail);
            if (await _unitOfWork.CommitAsync() <1)
            {
                throw new Exception("Thêm thông tin chi tiết hóa đơn thất bại!");
            }
        }
    }
}
