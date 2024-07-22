using CoffeeShop.Constants;
using CoffeeShop.Models.Responses;
using CoffeeShop.Models;
using CoffeeShop.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http.HttpResults;
using CoffeeShop.DTOs;
using NuGet.ContentModel;
using Microsoft.AspNetCore.Http;
using CoffeeShop.DTOs.Request;
using System.Drawing.Printing;
using System.Globalization;

namespace CoffeeShop.Controllers
{

    [Route("api/products")]
    [ApiController]
    public class ProductsController : BaseApiController
    {
        private readonly IProductService _productService;
        public ProductsController(IProductService productService)
        {
            _productService = productService;
        }

        [HttpGet]
        public async Task<IActionResult> GetAllProductsAsync(
            int pageNumber, 
            Guid? category, 
            string? search, 
            string? sortOrder, 
            string? sortBy = "productName", 
            string? newProductName = "")
        {
            try
            {
                var products = await _productService.GetAllProductsAsync(pageNumber == 0 ? 1 : pageNumber, category, search, sortOrder, sortBy, "Category", newProductName);
                if (products.data.Any())
                {
                    return Ok(new GeneralGetsResponse
                    {
                        Success = true,
                        Message = "Truy vấn sản phẩm thành công!",
                        Data = products.data,
                        TotalCount = products.totalCount
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
        public async Task<IActionResult> GetProductByIdAsync(Guid id)
        {
            try
            {
                var product = await _productService.GetProductDetail(id);
                if (product != null)
                {
                    return Ok(new GeneralGetResponse
                    {
                        Success = true,
                        Message = "Truy vấn sản phẩm thành công!",
                        Data = product
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


        [HttpPost]
        //[Consumes("multipart/form-data")]
        public async Task<IActionResult> CreateProductAsync([FromBody] ProductRequestDTO productRequest
            //, IFormFile fileUpload
            )
        {
            try
            {
                //var file = _productService.ConvertToFileUpload(fileUpload);
                var product = await _productService.CreateProductAsync(productRequest
                    //, file
                    );
                if (product == null)
                {
                    return Conflict(new GeneralBoolResponse
                    {
                        Success = false,
                        Message = "Thêm sản phẩm thất bại!"
                    });
                }
                return Ok(new GeneralCreateResponse
                {
                    Success = true,
                    Message = "Thêm sản phẩm thành công!",
                    Data = product
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
        public async Task<IActionResult> DeleteProductAsync(Guid id)
        {
            try
            {
                var result = await _productService.DeleteProductAsync(id);
                if (result == null)
                {
                    return Conflict(new GeneralBoolResponse
                    {
                        Success = false,
                        Message = "Xóa sản phẩm thất bại!"
                    });
                }
                return Ok(new GeneralCreateResponse
                {
                    Success = true,
                    Message = "Xóa sản phẩm thành công!",
                    Data = result
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

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateProduct(Guid id, ProductRequestDTO productRequest)
        {
            var response = new GeneralGetResponse();
            try
            {
                var result = await _productService.UpdateProduct(id, productRequest);
                response.Success = true;
                response.Message = "Cập nhật thông tin sản phẩm thành công!";
                response.Data = result;
                return Ok(response);
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
                response.Success = false;
                response.Message = ex.Message;
                return Conflict(response);
            }
        }

        [HttpGet("reports")]
        public async Task<IActionResult> GetReportAsync(int pageNumber, string? search, string? sortOrder, string sortBy = "productName")
        {
            try
            {
                var (reports, count) = await _productService.GetReports(pageNumber == 0 ? 1 : pageNumber, search, sortOrder, sortBy, "Receipt, ReceiptDetails");
                if (reports.Any())
                {
                    return Ok(new GeneralGetsResponse
                    {
                        Success = true,
                        Message = "Truy vấn báo cáo sản phẩm thành công!",
                        Data = reports,
                        TotalCount = count
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
    }
}