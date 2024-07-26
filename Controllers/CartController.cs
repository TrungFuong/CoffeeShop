using CoffeeShop.DTOs.Request;
using CoffeeShop.Models.Responses;
using CoffeeShop.Services;
using CoffeeShop.Services.Implementations;
using Microsoft.AspNetCore.Authorization;
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

        [HttpGet]
        public async Task<IActionResult> GetAllCartsAsync(
            int pageNumber,
            string? sortOrder = "desc",
            string? sortBy = "carttime")
        {
            try
            {
                var carts = await _cartService.GetAllCartsAsync(pageNumber == 0 ? 1 : pageNumber, sortOrder, sortBy, "CartDetails,Customer");
                if (carts.data.Any())
                {
                    return Ok(new GeneralGetsResponse
                    {
                        Success = true,
                        Message = "Truy vấn giỏ hàng thành công!",
                        Data = carts.data,
                        TotalCount = carts.totalCount
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
        public async Task<IActionResult> GetCartDetail(Guid id)
        {
            try
            {
                var cart = await _cartService.GetCartDetailAsync(id);
                if (cart != null)
                {
                    return Ok(new GeneralGetResponse
                    {
                        Success = true,
                        Message = "Truy vấn giỏ hàng thành công!",
                        Data = cart
                    });
                }
                return Conflict(new GeneralGetResponse
                {
                    Success = false,
                    Message = "Không tìm thấy giỏ hàng!"
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
