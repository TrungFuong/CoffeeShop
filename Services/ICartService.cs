using CoffeeShop.DTOs.Request;
using CoffeeShop.DTOs.Responses;

namespace CoffeeShop.Services
{
    public interface ICartService
    {
        Task AddCartInfoAsync(Guid cartId, CartRequestDTO cartRequest);
        Task<CartResponseDTO> AddCartAsync(CartRequestDTO cartRequest);

    }
}
