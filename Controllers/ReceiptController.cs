using CoffeeShop.Models.Responses;
using CoffeeShop.Services;
using Microsoft.AspNetCore.Mvc;

namespace CoffeeShop.Controllers
{
    [Route("api/receipts")]
    [ApiController]
    public class ReceiptController : BaseApiController
    {
        private readonly IReceiptService _receiptService;
        public ReceiptController(IReceiptService receiptService)
        {
            _receiptService = receiptService;
        }



        [HttpGet]
        public async Task<IActionResult> GetAllReceiptsAsync(int page, string? search, DateTime? receiptDate, string? sortOrder, string? sortBy = "receiptDate", Guid? newReceiptId = null)
        {
            try
            {
                var receipts = await _receiptService.GetAllReceiptsAsync(
                    page == 0 ? 1 : page,
                    search,
                    receiptDate,
                    sortOrder,
                    sortBy,
                    "User, Customer, Product",
                    newReceiptId);
                if (receipts.data.Any())
                {
                    return Ok(new GeneralGetsResponse
                    {
                        Success = true,
                        Message = "Receipts retrieved successfully.",
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
    }
}
