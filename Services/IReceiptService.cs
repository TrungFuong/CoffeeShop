using CoffeeShop.DTOs.Request;
using CoffeeShop.DTOs.Responses;

namespace CoffeeShop.Services
{
    public interface IReceiptService
    {
        Task<ReceiptResponseDTO> AddReceiptAsync(ReceiptRequestDTO receiptRequestDTO);
        //Task<IEnumerable<ReceiptResponseDTO>> GetAllReceiptsAsync(int pageNumber, string? search, string? sortOrder, string? sortBy = "productName", string includeProperties = "");
    }
}
