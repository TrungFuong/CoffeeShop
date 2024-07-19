using CoffeeShop.DTOs;
using CoffeeShop.Models;

namespace CoffeeShop.Services
{
    public interface IReceiptDetailService
    {
        Task AddReceiptDetailAsync(Guid receiptId, List<ReceiptDetailDTO> receiptDetailDTO);
    }
}
