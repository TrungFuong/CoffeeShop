using CoffeeShop.DTOs.Request;
using CoffeeShop.DTOs.Responses;

namespace CoffeeShop.Services
{
    public interface ICartService
    {
        Task AddCartInfoAsync(Guid cartId, CartRequestDTO cartRequest);
        Task<CartResponseDTO> AddCartAsync(CartRequestDTO cartRequest);
        Task<(IEnumerable<CartResponseDTO> data, int totalCount)> GetAllCartsAsync(int pageNumber, string? sortOrder, string? sortBy = "carttime", string includeProperties = "");
        Task<CartResponseDTO> GetCartDetailAsync(Guid id);
        Task DeleteCartAsync(string table);
    }
}
