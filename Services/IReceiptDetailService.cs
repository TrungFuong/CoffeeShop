using CoffeeShop.DTOs;

namespace CoffeeShop.Services
{
    public interface IReceiptDetailService
    {
        Task<bool> AddReceiptDetailAsync(List<ReceiptDetailDTO> receiptDetailDTO);
    }
}
