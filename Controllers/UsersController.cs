using CoffeeShop.Constants;
using CoffeeShop.DTOs.Request;
using CoffeeShop.Models;
using CoffeeShop.Models.Responses;
using CoffeeShop.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;


namespace CoffeeShop.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsersController : BaseApiController
    {
        private readonly IUserService _userService;
        public UsersController(IUserService userService)
        {
            _userService = userService;
        }

        [HttpPost]
        public async Task<IActionResult> RegisterUserAsync([FromBody] UserRegisterRequestDTO userRegisterRequest)
        {
            try
            {
                var result = await _userService.AddUserAsync(userRegisterRequest);
                if (result != null)
                {
                    return Ok(new GeneralGetResponse
                    {
                        Success = true,
                        Message = "Đăng ký tài khoản thành công!",
                        Data = result
                    });
                }
                return Conflict(new GeneralGetResponse
                {
                    Success = false,
                    Message = "Đăng ký tài khoản thất bại"
                });
            }
            catch (Exception ex)
            {
                return Conflict(new GeneralGetResponse
                {
                    Success = false,
                    Message = ex.Message
                });
            }
        }

        [HttpGet]
        [Authorize]
        public async Task<IActionResult> GetAllUsersAsync(int pageNumber, string? search, string? sortOrder, string? sortBy = "username", string? includeProperties = "", string? newUsername = "")
        {
            try
            {
                var users = await _userService.GetAllUsersAsync(pageNumber == 0 ? 1 : pageNumber, search, sortOrder, sortBy, includeProperties, newUsername);
                if(users.data.Any())
                {
                    return Ok(new GeneralGetsResponse
                    {
                        Success = true,
                        Message = "Truy vấn người dùng thành công!",
                        Data = users.data,
                        TotalCount = users.totalCount
                    });
                }
                return Conflict(new GeneralGetsResponse
                {
                    Success = false,
                    Message = "Không có dữ liệu!",
                });
            }
            catch (Exception ex)
            {
                return Conflict(new GeneralGetsResponse
                {
                    Success = false,
                    Message = ex.Message,
                });
            }
        }

        [HttpGet("{id}")]
        [Authorize]
        public async Task<IActionResult> GetUserDetailAsync(Guid id)
        {
            try
            {
                var user = await _userService.GetUserDetailAsync(id);
                return Ok(new GeneralGetResponse
                {
                    Success = true,
                    Message = "Truy vấn người dùng thành công!",
                    Data = user
                });
            }
            catch (Exception ex)
            {
                return Conflict(new GeneralGetResponse
                {
                    Success = false,
                    Message = ex.Message
                });
            }
        }

        [HttpPut("{id}")]
        [Authorize]
        public async Task<IActionResult> UpdateUserAsync(Guid id, UserUpdateRequestDTO userUpdateRequest)
        {
            try
            {
                var result = await _userService.UpdateUser(id, userUpdateRequest);
                return Ok(new GeneralBoolResponse
                {
                    Success = true,
                    Message = "Cập nhật người dùng thất bại!"
                });
            }
            catch (Exception ex)
            {
                return Conflict(new GeneralBoolResponse
                {
                    Success = false,
                    Message = ex.Message
                });
            }
        }

        [HttpDelete("{id}")]
        [Authorize(Roles = RoleConstant.ADMIN)]
        public async Task<IActionResult> DeleteUser(Guid id)
        {
            try
            {
                var result = await _userService.DeleteUser(id);
                if (result)
                {
                    return Ok(new GeneralGetResponse
                    {
                        Success = true,
                        Message = "Xóa người dùng thành công!"
                    });
                }
                return Conflict(new GeneralGetResponse
                {
                    Success = false,
                    Message = "Xóa người dùng thất bại!"
                });
            }
            catch (Exception ex)
            {
                return Conflict(new GeneralGetResponse
                {
                    Success = false,
                    Message = ex.Message
                });
            }
        }
    }
}
