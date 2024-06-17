using System.Collections.Generic;
using System.Threading.Tasks;
using CoffeeShop.DTOs;
using CoffeeShop.Exceptions;
using CoffeeShop.Services;
using Microsoft.AspNetCore.Mvc;

namespace CoffeeShop.Controllers
{
    [Route("api/category")]
    [ApiController]
    public class CategoriesController : ControllerBase
    {
        private readonly ICategoryService _categoryService;

        public CategoriesController(ICategoryService categoryService)
        {
            _categoryService = categoryService;
        }

        // GET: api/Categories
        [HttpGet]
        public async Task<ActionResult<IEnumerable<CategoryResponseDTO>>> GetCategories()
        {
            var categories = await _categoryService.GetAllCategoriesAsync();
            return Ok(categories);
        }

        // GET: api/Categories/5
        [HttpGet("{categoryId}")]
        public async Task<ActionResult<CategoryResponseDTO>> GetCategory(Guid categoryId)
        {
            var category = await _categoryService.GetCategoryByIdAsync(categoryId);
            if (category == null)
            {
                return NotFound();
            }
            return Ok(category);
        }

        // GET: api/cateId/Products
        [HttpGet("{categoryId}/Product")]
        public async Task<ActionResult<IEnumerable<ProductResponseDTO>>> GetProductByCategoryAsync(Guid categoryId) 
        {
            var product = await _categoryService.GetProductByCategory(categoryId);
            if (product == null)
            {
                return NotFound();
            }
            return Ok(product);
        }

        // PUT: api/Categories/5
        [HttpPut("{categoryId}")]
        public async Task<IActionResult> PutCategory(Guid categoryId, CategoryRequestDTO categoryDTO)
        {
            try
            {
                await _categoryService.UpdateCategoryAsync(categoryId, categoryDTO);
                return NoContent();
            }
            catch (NotFoundException ex)
            {
                return NotFound(ex.Message);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        // POST: api/Categories
        [HttpPost]
        public async Task<ActionResult<CategoryResponseDTO>> PostCategory(CategoryRequestDTO categoryDTO)
        {
            await _categoryService.AddCategoryAsync(categoryDTO);
            var addedCategory = await _categoryService.GetCategoryByNameAsync(categoryDTO.CategoryName);
            return CreatedAtAction(nameof(GetCategory), new { id = addedCategory.CategoryId }, addedCategory);
        }

        // DELETE: api/Categories/5
        [HttpDelete("{categoryId}")]
        public async Task<IActionResult> DeleteCategory(Guid categoryId)
        {
            await _categoryService.DeleteCategoryAsync(categoryId);
            return NoContent();
        }
    }
}