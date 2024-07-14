using CoffeeShop.DTOs.Responses;
using CoffeeShop.Models;
using CoffeeShop.UnitOfWork;
using Microsoft.IdentityModel.Tokens;

namespace CoffeeShop.Services.Implementations
{
    public class AuthService : IAuthService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly ITokenService _tokenService;
        public AuthService(IUnitOfWork unitOfWork, ITokenService tokenService)
        {
            _unitOfWork = unitOfWork;
            _tokenService = tokenService;
        }
        public async Task<(string token, string refreshToken, UserResponseDTO userResponse)> LoginAsync(string username, string password)
        {
            var user = await _unitOfWork.UserRepository.GetAsync(u => !u.IsDeleted && u.Username == username);
            if (user == null || !BCrypt.Net.BCrypt.Verify(password, user.HashPassword))
            {
                throw new UnauthorizedAccessException("Sai tên đăng nhập hoặc mật khẩu!!!");
            }
            var token = _tokenService.GenerateToken(user);
            var refreshToken = _tokenService.GenerateRefreshToken();
            refreshToken.UserId = user.UserId;

            await _unitOfWork.TokenRepository.AddAsync(new Token
            {
                UserId = user.UserId,
                HashToken = token,
            });
            await _unitOfWork.RefreshTokenRepository.AddAsync(refreshToken);
            await _unitOfWork.CommitAsync();

            var userResponse = new UserResponseDTO
            {
                UserId = user.UserId,
                Username = user.Username,
                FirstName = user.FirstName,
                LastName = user.LastName,
                DateOfBirth = user.DateOfBirth,
                UserPosition = user.UserPosition,
                Role = user.Role,
                PhoneNumber = user.PhoneNumber,
                Gender = user.Gender
            };

            return (token, refreshToken.TokenHash, userResponse);
        }
        public async Task<(string token, string refreshToken, UserResponseDTO userResponse)> RefreshTokenAsync(string refreshToken)
        {
            var token = await _unitOfWork.RefreshTokenRepository.GetAsync(rt => rt.TokenHash == refreshToken).ConfigureAwait(false);
            if (token == null || token.ExpiredAt <= DateTime.Now)
            {
                throw new SecurityTokenException("Refresh token không hợp lệ!!!");
            }

            var user = await _unitOfWork.UserRepository.GetAsync(u => u.UserId == token.UserId);
            if (user == null)
            {
                throw new KeyNotFoundException("Không tìm thấy người dùng!!!");
            }
            var newJwtToken = _tokenService.GenerateToken(user);
            var newRefreshToken = _tokenService.GenerateRefreshToken();
            newRefreshToken.UserId = user.UserId;

            _unitOfWork.RefreshTokenRepository.Delete(token);
            await _unitOfWork.RefreshTokenRepository.AddAsync(newRefreshToken);
            await _unitOfWork.TokenRepository.AddAsync(new Token
            {
                UserId = user.UserId,
                HashToken = newJwtToken,
            });
            await _unitOfWork.CommitAsync();
            var userResponse = new UserResponseDTO
            {
                UserId = user.UserId,
                Username = user.Username,
                FirstName = user.FirstName,
                LastName = user.LastName,
                DateOfBirth = user.DateOfBirth,
                UserPosition = user.UserPosition,
                Role = user.Role,
                PhoneNumber = user.PhoneNumber,
                Gender = user.Gender
            };
            return (newJwtToken, newRefreshToken.TokenHash, userResponse);
        }

        public async Task<int> ChangePasswordAsync(string username, string oldPassword, string newPassword, string refreshToken, string currentToken)
        {
            var user = await _unitOfWork.UserRepository.GetAsync(u => u.Username == username);
            if (user == null)
            {
                throw new KeyNotFoundException("Không tìm thấy người dùng!");
            }

            if (!BCrypt.Net.BCrypt.Verify(oldPassword, user.HashPassword))
            {
                throw new UnauthorizedAccessException("Sai mật khẩu!");
            }
            var passwordSalt = BCrypt.Net.BCrypt.GenerateSalt();
            var hashPassword = BCrypt.Net.BCrypt.HashPassword(newPassword, passwordSalt);

            user.HashPassword = hashPassword;
            user.Salt = passwordSalt;
            user.IsFirstLogin = false;
            _unitOfWork.UserRepository.Update(user);
            var tokens = await _unitOfWork.RefreshTokenRepository.GetAllAsync(rt => rt.TokenHash != refreshToken);
            _unitOfWork.RefreshTokenRepository.RemoveRange(tokens);

            return await _unitOfWork.CommitAsync(); 
        }

        public async Task<int> LogoutAsync(Guid userId)
        {
            var tokens = await _unitOfWork.RefreshTokenRepository.GetAllAsync(rt => rt.UserId == userId);
            _unitOfWork.RefreshTokenRepository.RemoveRange(tokens);
            return await _unitOfWork.CommitAsync();
        }

        public Task<int> ResetPasswordAsync(string username, string newPassword, string refreshToken)
        {
            throw new NotImplementedException();
        }
    }
}
