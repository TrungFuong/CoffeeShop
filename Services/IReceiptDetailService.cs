using CoffeeShop.DTOs;
using CoffeeShop.DTOs.Responses;
using CoffeeShop.Models;

namespace CoffeeShop.Services
{
    public interface IReceiptDetailService
    {
        Task AddReceiptDetailAsync(Guid receiptId, List<ReceiptDetailDTO> receiptDetailDTO);
        //Task<IEnumerable<ReceiptDetailResponseDTO>> GetSoldProductsAsync(Guid id);
    }
}
