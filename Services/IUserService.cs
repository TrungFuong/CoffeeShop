using CoffeeShop.DTOs.Request;
using CoffeeShop.DTOs.Responses;

namespace CoffeeShop.Services
{
    public interface IUserService
    {
        Task<UserRegisterResponseDTO> AddUserAsync(UserRegisterRequestDTO userRegisterRequest);

    }
}
