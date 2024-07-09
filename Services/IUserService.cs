using CoffeeShop.DTOs;
using CoffeeShop.DTOs.Request;
using CoffeeShop.DTOs.Responses;

namespace CoffeeShop.Services
{
    public interface IUserService
    {
        Task<UserRegisterResponseDTO> AddUserAsync(UserRegisterRequestDTO userRegisterRequest);
        Task<(IEnumerable<UserResponseDTO> data, int totalCount)> GetAllUsersAsync(int pageNumber, string? search, string? sortOrder, string? sortBy = "username", string includeProperties = "", string? newUsername = "");
        Task<UserResponseDTO> GetUserDetailAsync(Guid id);
        Task<UserResponseDTO> UpdateUser(Guid id, UserUpdateRequestDTO userRequestDTO);
        Task<bool> DeleteUser(Guid id);
    }
}
