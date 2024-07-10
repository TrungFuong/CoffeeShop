using CoffeeShop.Models;

namespace CoffeeShop.Services
{
    public interface ITokenService
    {
        string GenerateToken(User user);
        RefreshToken GenerateRefreshToken();
    }
}
