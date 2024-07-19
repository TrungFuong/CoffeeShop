using CoffeeShop.DTOs.Request;
using CoffeeShop.DTOs.Responses;

namespace CoffeeShop.Services
{
    public interface IReceiptService
    {
        Task AddReceiptInfoAsync(Guid receiptId, ReceiptRequestDTO receiptRequestDTO);
        Task<ReceiptResponseDTO> AddReceiptAsync(ReceiptRequestDTO receiptRequestDTO);
        Task<(IEnumerable<ReceiptResponseDTO> data, int totalCount)> GetAllReceiptsAsync(int pageNumber, string? search, DateTime? receiptDate, string? sortOrder, string? sortBy = "receiptDate", string includeProperties = "", Guid? newReceiptId = null);

    }
}
