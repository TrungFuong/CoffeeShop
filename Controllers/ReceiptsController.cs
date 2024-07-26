using CoffeeShop.DTOs.Request;
using CoffeeShop.Models.Responses;
using CoffeeShop.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace CoffeeShop.Controllers
{
    [Route("api/receipts")]
    [ApiController]
    public class ReceiptsController : BaseApiController
    {
        private readonly IReceiptService _receiptService;
        public ReceiptsController(IReceiptService receiptService)
        {
            _receiptService = receiptService;
        }

        [HttpPost]
        [Authorize]
        public async Task<IActionResult> AddReceiptAsync([FromBody] ReceiptRequestDTO request)
        {
            try
            {
                var result = await _receiptService.AddReceiptAsync(request);
                if (result != null)
                {
                    return Ok(new GeneralCreateResponse
                    {
                        Success = true,
                        Message = "Tạo hóa đơn thành công!",
                        Data = result
                    });
                }
                return Conflict(new GeneralCreateResponse
                {
                    Success = false,
                    Message = "Tạo hóa đơn thất bại!
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
        [Authorize]
        public async Task<IActionResult> GetAllReceiptsAsync(int page, string? search, DateTime? receiptDate, string? sortOrder, string? sortBy = "receiptDate")
        {
            try
            {
                var receipts = await _receiptService.GetAllReceiptsAsync(
                    page == 0 ? 1 : page,
                    search,
                    receiptDate,
                    sortOrder,
                    sortBy,
                    "User,Customer,ReceiptDetails");
                if (receipts.data.Any())
                {
                    return Ok(new GeneralGetsResponse
                    {
                        Success = true,
                        Message = "Truy vấn hóa đơn thành công!",
                        Data = receipts.data,
                        TotalCount = receipts.totalCount
                    });
                }
                return Conflict(new GeneralGetsResponse
                {
                    Success = false,
                    Message = "No data.",
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
        public async Task<IActionResult> GetReceiptDetailAsync(Guid id)
        {
            try
            {
                var receipt = await _receiptService.GetReceiptDetailAsync(id);
                if (receipt != null)
                {
                    return Ok(new GeneralGetResponse
                    {
                        Success = true,
                        Message = "Truy vấn hóa đơn thành công!",
                        Data = receipt
                    });
                }
                return Conflict(new GeneralGetResponse
                {
                    Success = false,
                    Message = "Không tìm thấy hóa đơn!"
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
