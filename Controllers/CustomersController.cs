using CoffeeShop.DTOs.Request;
using CoffeeShop.DTOs.Responses;
using CoffeeShop.Models.Responses;
using CoffeeShop.Services;
using CoffeeShop.Services.Implementations;
using Microsoft.AspNetCore.Mvc;
using NuGet.ContentModel;

namespace CoffeeShop.Controllers
{
    public class CustomersController : BaseApiController
    {
        private readonly ICustomerService _customerService;

        public CustomersController(ICustomerService customerService)
        {
            _customerService = customerService;
        }

        [HttpPut("{phone}")]
        public async Task<IActionResult> UpdateCustomer(string phone, CustomerRequestDTO customerRequest)
        {
            try
            {
                var customer = await _customerService.UpdateCustomer(phone, customerRequest);
                return Ok(new GeneralGetResponse
                {
                    Data = customer,
                    Message = "Cập nhật thông tin khách hàng thành công!",
                    Success = true
                });
            }
            catch (Exception e)
            {
                return Conflict(new GeneralGetResponse
                {
                    Success = false,
                    Message = e.Message
                });
            }
        }

        [HttpGet]
        public async Task<IActionResult> GetAllCustomersAsync(int pageNumber, string? search, string? sortOrder, string? sortBy = "customerName", string includeProperties = "")
        {
            try
            {
                var customers = await _customerService.GetAllCustomersAsync(pageNumber, search, sortOrder, sortBy, "receipts");
                if (customers.data.Any())
                {
                    return Ok(new GeneralGetsResponse
                    {
                        Success = true,
                        Message = "Lấy dữ liệu khách hàng thành công!",
                        Data = customers.data,
                        TotalCount = customers.totalCount
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

        [HttpGet("{phone}")]
        public async Task<ActionResult<CustomerResponseDTO>> GetCustomerDetailAsync(string phone)
        {
            try
            {
                var customer = await _customerService.GetCustomerDetailAsync(phone);
                if (customer != null)
                {
                    return Ok(new GeneralGetResponse
                    {
                        Success = true,
                        Message = "Truy vấn khách hàng thành công!",
                        Data = customer 
                    });
                }
                return Conflict(new GeneralGetResponse
                {
                    Success = false,
                    Message = "Không có dữ liệu!"
                });
            }
            catch (KeyNotFoundException ex)
            {
                return NotFound(new GeneralGetResponse
                {
                    Success = false,
                    Message = ex.Message
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
