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
        public async Task<IActionResult> GetAllProductsAsync(int pageNumber, Guid? category, string? search, string? sortOrder, string? sortBy = "productName", string? newProductName = "")
        {
            try
            {
                var products = await _productService.GetAllProductsAsync(pageNumber == 0 ? 1 : pageNumber, category, search, sortOrder, sortBy, "Category", newProductName);
                if (products.data.Any())
                {
                    return Ok(new GeneralGetsResponse
                    {
                        Success = true,
                        Message = "Products retrieved successfully.",
                        Data = products.data,
                        TotalCount = products.totalCount
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

        [HttpPost]
        [Consumes("multipart/form-data")]
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
                        Message = "Product creation failed."
                    });
                }
                return Ok(new GeneralCreateResponse
                {
                    Success = true,
                    Message = "Product created successfully.",
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
                        Message = "Product deletion failed."
                    });
                }
                return Ok(new GeneralCreateResponse
                {
                    Success = true,
                    Message = "Product deleted successfully.",
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
                response.Message = "Update successfully";
                response.Data = result;
                return Ok(response);
            }
            catch (Exception ex)
            {
                response.Success = false;
                response.Message = ex.Message;
                return Conflict(response);
            }
        }
    }
}