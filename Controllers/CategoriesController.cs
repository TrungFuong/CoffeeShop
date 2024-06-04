using System.Collections.Generic;
using System.Threading.Tasks;
using CoffeeShop.DTOs;
using CoffeeShop.Services;
using Microsoft.AspNetCore.Mvc;

namespace CoffeeShop.Controllers
{
    [Route("api/[controller]")]
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
        public async Task<ActionResult<IEnumerable<GetCategoryDTO>>> GetCategories()
        {
            var categories = await _categoryService.GetAllCategoriesAsync();
            return Ok(categories);
        }

        // GET: api/Categories/5
        [HttpGet("{id}")]
        public async Task<ActionResult<GetCategoryDTO>> GetCategory(int id)
        {
            var category = await _categoryService.GetCategoryByIdAsync(id);
            if (category == null)
            {
                return NotFound();
            }
            return Ok(category);
        }

        // PUT: api/Categories/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutCategory(int id, UpdateCategoryDTO categoryDTO)
        {
            if (id != categoryDTO.CategoryId)
            {
                return BadRequest();
            }

            await _categoryService.UpdateCategoryAsync(id, categoryDTO);
            return NoContent();
        }

        // POST: api/Categories
        [HttpPost]
        public async Task<ActionResult<GetCategoryDTO>> PostCategory(AddCategoryDTO categoryDTO)
        {
            await _categoryService.AddCategoryAsync(categoryDTO);
            var addedCategory = await _categoryService.GetCategoryByNameAsync(categoryDTO.CategoryName);
            return CreatedAtAction(nameof(GetCategory), new { id = addedCategory.CategoryId }, addedCategory);
        }

        // DELETE: api/Categories/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteCategory(int id)
        {
            await _categoryService.DeleteCategoryAsync(id);
            return NoContent();
        }
    }
}