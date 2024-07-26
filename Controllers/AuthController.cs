using CoffeeShop.DTOs.Request;
using CoffeeShop.Models.Responses;
using CoffeeShop.Services;
using Google.Apis.Auth.OAuth2.Requests;
using Microsoft.AspNetCore.Mvc;

namespace CoffeeShop.Controllers
{
    public class AuthController : BaseApiController
    {
        private readonly IAuthService _authService;
        public AuthController(IAuthService authService)
        {
            _authService = authService;
        }

        [HttpPost("login")]
        public async Task<IActionResult> LoginAsync([FromBody] UserLoginRequestDTO request)
        {
            try
            {
                var (token, refreshToken, user) = await _authService.LoginAsync(request.Username, request.Password);
                var response = new GeneralGetResponse
                {
                    Message = "Đăng nhập thành công!",
                    Data = new { token, refreshToken, user }
                };
                return Ok(response);
            }
            catch (Exception ex)
            {
                var response = new GeneralBoolResponse
                {
                    Success = false,
                    Message = ex.Message
                };
                return Conflict(response);
            }
        }

        [HttpPost("change-password")]
        public async Task<IActionResult> ChangePasswordAsync([FromBody] ChangePasswordRequest request)
        {
            try
            {
                await _authService.ChangePasswordAsync(request.Username, request.OldPassword, request.NewPassword, request.RefreshToken, CurrentToken);
                var response = new GeneralBoolResponse
                {
                    Message = "Đổi mật khẩu thành công!",
                };
                return Ok(response);
            }
            catch (Exception ex)
            {
                var response = new GeneralBoolResponse
                {
                    Success = false,
                    Message = ex.Message
                };
                return Conflict(response);
            }
        }



        [HttpPost("refresh-token")]
        public async Task<IActionResult> RefreshTokenAsync([FromBody] RefreshTokenRequest refreshTokenRequest)
        {
            try
            {
                var (token, refreshToken, user) = await _authService.RefreshTokenAsync(refreshTokenRequest.RefreshToken);
                var response = new GeneralGetResponse
                {
                    Message = "Làm mới token thành công!",
                    Data = new { token, refreshToken, user }
                };
                return Ok(response);
            }
            catch (Exception ex)
            {
                var response = new GeneralBoolResponse
                {
                    Success = false,
                    Message = ex.Message
                };
                return Conflict(response);
            }
        }
        
    }
}

