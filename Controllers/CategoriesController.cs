using System.Collections.Generic;
using System.Threading.Tasks;
using CoffeeShop.DTOs;
using CoffeeShop.DTOs.Request;
using CoffeeShop.Exceptions;
using CoffeeShop.Models.Responses;
using CoffeeShop.Services;
using Microsoft.AspNetCore.Mvc;

namespace CoffeeShop.Controllers
{
    [Route("api/category")]
    [ApiController]
    public class CategoriesController : BaseApiController
    {
        private readonly ICategoryService _categoryService;

        public CategoriesController(ICategoryService categoryService)
        {
            _categoryService = categoryService;
        }

        [HttpGet]
        public async Task<IActionResult> GetAllCategoriesAsync()
        {
            try
            {
                var categories = await _categoryService.GetAllCategoriesAsync();
                if (categories != null && categories.Any())
                {
                    return Ok(new GeneralGetsResponse
                    {
                        Success = true,
                        Message = "Truy vấn danh mục thành công!",
                        Data = categories,
                    });
                }
                else
                {
                    return Conflict(new GeneralBoolResponse
                    {
                        Success = false,
                        Message = "No category."
                    });
                }
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
                return Conflict(new GeneralGetsResponse
                {
                    Success = false,
                    Message = ex.Message,
                });
            }
        }

        [HttpPost]
        public async Task<IActionResult> CreateCategoryAsync([FromBody] CategoryRequestDTO request)
        {
            try
            {
                var result = await _categoryService.CreateCategoryAsync(request);
                if (result != null)
                {
                    return Ok(new GeneralGetResponse
                    {
                        Success = true,
                        Message = "Category added successfully.",
                        Data = result,
                    });
                }
                else
                {
                    return Conflict(new GeneralBoolResponse
                    {
                        Success = false,
                        Message = "Category added failed."
                    });
                }
            }
            catch (Exception ex)
            {
                return Conflict(new GeneralBoolResponse
                {
                    Success = false,
                    Message = ex.Message,
                });
            }
        }
    }
}