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

        public async Task AddReceiptDetailAsync(Guid receiptId, List<ReceiptDetailDTO> receiptDetailDTO)
        {
            
            var receiptDetails = receiptDetailDTO.Select(dto => new ReceiptDetail
            {
                ReceiptId = receiptId,
                ProductId = dto.ProductId,
                ProductQuantity = dto.ProductQuantity
            }).ToList();

            await _unitOfWork.ReceiptDetailRepository.AddRangeAsync(receiptDetails); 
        }
    }
}
