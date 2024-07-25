using CoffeeShop.DTOs;
using CoffeeShop.DTOs.Request;

namespace CoffeeShop.Services
{
    public interface ICartDetailService
    {
        Task AddCartDetailAsync(Guid cartId, List<CartDetailRequestDTO> cartDetailRequestDTO);

    }
}
