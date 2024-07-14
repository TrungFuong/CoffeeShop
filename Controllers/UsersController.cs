using CoffeeShop.DTOs.Request;
using CoffeeShop.Models.Responses;
using CoffeeShop.Services;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

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
                        Message = "User registered successfully.",
                        Data = result
                    });
                }
                return Conflict(new GeneralGetResponse
                {
                    Success = false,
                    Message = "User registration failed."
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
        public async Task<IActionResult> GetAllUsersAsync(int pageNumber, string? search, string? sortOrder, string? sortBy = "username", string includeProperties = "", string? newUsername = "")
        {
            try
            {
                var users = await _userService.GetAllUsersAsync(pageNumber == 0 ? 1 : pageNumber, search, sortOrder, sortBy, includeProperties, newUsername);
                return Ok(new GeneralGetsResponse
                {
                    Success = true,
                    Message = "Users retrieved successfully!",
                    Data = users.data
                });
            }
            catch (Exception ex)
            {
                return Conflict(new GeneralGetsResponse
                {
                    Success = false,
                    Message = ex.Message
                });
            }
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetUserDetailAsync(Guid id)
        {
            try
            {
                var user = _userService.GetUserDetailAsync(id);
                return Ok(new GeneralGetResponse
                {
                    Success = true,
                    Message = "User retrieved successfully!",
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

        [HttpPut]
        public async Task<IActionResult> UpdateUserAsync(Guid id, UserUpdateRequestDTO userUpdateRequest)
        {
            try
            {
                var result = await _userService.UpdateUser(id, userUpdateRequest);
                return Ok(new GeneralBoolResponse
                {
                    Success = true,
                    Message = "User updated successfully."
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
                        Message = "User deleted successfully."
                    });
                }
                return Conflict(new GeneralGetResponse
                {
                    Success = false,
                    Message = "User deletion failed."
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
