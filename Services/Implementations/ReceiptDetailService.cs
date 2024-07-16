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

        public async Task<bool> AddReceiptDetailAsync(List<ReceiptDetailDTO> receiptDetailDTO)
        {
            var receipt = await _unitOfWork.ReceiptRepository.GetAsync(rd => rd.ReceiptId == receiptDetailDTO[0].ReceiptId);
            if (receipt == null)
            {
                return false;
            }

            foreach (var item in receiptDetailDTO)
            {
                var receiptDetail = new ReceiptDetail
                {
                    ReceiptId = item.ReceiptId,
                    ProductId = item.ProductId,
                    ProductQuantity = item.ProductQuantity
                };
                await _unitOfWork.ReceiptDetailRepository.AddAsync(receiptDetail);
            }

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
}