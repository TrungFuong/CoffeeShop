using CoffeeShop.DTOs;
using CoffeeShop.Models;

namespace CoffeeShop.Services
{
    public interface IReceiptDetailService
    {
        Task<bool> AddReceiptDetailAsync(Guid receiptId, List<ReceiptDetailDTO> receiptDetailDTO);
    }
}
