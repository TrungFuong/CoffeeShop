using CoffeeShop.DTOs.Responses;

namespace CoffeeShop.Services
{
    public interface IAuthService
    {
        Task<(string token, string refreshToken, UserResponseDTO userResponse)> LoginAsync(string username, string password);

        Task<(string token, string refreshToken, UserResponseDTO userResponse)> RefreshTokenAsync(string refreshToken);

        Task<int> LogoutAsync(Guid userId);

        Task<int> ResetPasswordAsync(string username, string newPassword, string refreshToken);

        Task<int> ChangePasswordAsync(string username, string oldPassword, string newPassword, string refreshToken, string currentToken);
    }
}
