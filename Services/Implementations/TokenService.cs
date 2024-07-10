using CoffeeShop.Models;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace CoffeeShop.Services.Implementations
{
    public class TokenService : ITokenService
    {
        private readonly IConfiguration _configuration;
        public TokenService(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public string GenerateToken(User user)
        {
            var claims = new List<Claim>
        {
            new Claim(ClaimTypes.NameIdentifier, user.Username),
            new Claim(ClaimTypes.Role, user.Role.ToString()),
            new Claim(ClaimTypes.Actor, user.UserId.ToString())
        };
            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["Jwt:Key"]));
            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

            var token = new JwtSecurityToken(
                _configuration["Jwt:Issuer"],
                _configuration["Jwt:Issuer"],
                claims,
                expires: DateTime.Now.AddMinutes(30),
                signingCredentials: creds);

            return new JwtSecurityTokenHandler().WriteToken(token);
        }

        public RefreshToken GenerateRefreshToken()
        {
            var tokenSalt = BCrypt.Net.BCrypt.GenerateSalt();
            DateTime dateTime = DateTime.UtcNow.AddDays(7);
            var refreshToken = new RefreshToken
            {
                TokenSalt = tokenSalt,
                TokenHash = BCrypt.Net.BCrypt.HashPassword(Guid.NewGuid().ToString(), tokenSalt),
                CreatedAt = DateTime.UtcNow,
                ExpiredAt = dateTime
            };

            return refreshToken;
        }

    }
}
