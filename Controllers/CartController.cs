using CoffeeShop.DTOs.Request;
using CoffeeShop.Models.Responses;
using CoffeeShop.Services;
using CoffeeShop.Services.Implementations;
using Microsoft.AspNetCore.Mvc;

namespace CoffeeShop.Controllers
{
    [Route("api/carts")]
    [ApiController]
    public class CartController : BaseApiController
    {
        private readonly ICartService _cartService;
        public CartController(ICartService cartService)
        {
            _cartService = cartService;
        }

        [HttpPost]
        public async Task<IActionResult> AddCartAsync([FromBody] CartRequestDTO request)
        {
            try
            {
                var result = await _cartService.AddCartAsync(request);
                if (result != null)
                {
                    return Ok(new GeneralCreateResponse
                    {
                        Success = true,
                        Message = "Đã thêm vào giỏ hàng!",
                        Data = result
                    });
                }
                return Conflict(new GeneralCreateResponse
                {
                    Success = false,
                    Message = "Thêm vào giỏ hàng thất bại!"
                });
            }
            catch (Exception ex)
            {
                return Conflict(new GeneralCreateResponse
                {
                    Success = false,
                    Message = ex.Message
                });
            }
        }
    }
}
