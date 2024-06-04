using CoffeeShop.DTOs;
using CoffeeShop.Models;
using CoffeeShop.Repositories;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CoffeeShop.Services
{
    public class CategoryService : ICategoryService
    {
        private readonly ICategoryRepository _categoryRepository;

        public CategoryService(ICategoryRepository categoryRepository)
        {
            _categoryRepository = categoryRepository;
        }

        public async Task AddCategoryAsync(AddCategoryDTO categoryDTO)
        {
            var category = new Category
            {
                CategoryName = categoryDTO.CategoryName
            };
            await _categoryRepository.AddCategoryAsync(category);
        }

        public async Task DeleteCategoryAsync(int categoryId)
        {
            var currentCategory = await _categoryRepository.GetCategoryByIdAsync(categoryId);
            if (currentCategory != null)
            {
                await _categoryRepository.DeleteCategoryAsync(categoryId);
            }
        }

        public async Task<IEnumerable<GetCategoryDTO>> GetAllCategoriesAsync()
        {
            var categories = await _categoryRepository.GetAllCategoriesAsync();
            return categories.Select(c => new GetCategoryDTO
            {
                CategoryId = c.CategoryId,
                CategoryName = c.CategoryName
            });
        }

        public async Task<GetCategoryDTO> GetCategoryByIdAsync(int categoryId)
        {
            var category = await _categoryRepository.GetCategoryByIdAsync(categoryId);
            if (category == null) return null;
            return new GetCategoryDTO
            {
                CategoryId = category.CategoryId,
                CategoryName = category.CategoryName
            };
        }

        public async Task<GetCategoryDTO> GetCategoryByNameAsync(string categoryName)
        {
            var category = await _categoryRepository.GetCategoryByNameAsync(categoryName);
            if (category == null) return null;
            return new GetCategoryDTO
            {
                CategoryId = category.CategoryId,
                CategoryName = category.CategoryName
            };
        }

        public async Task UpdateCategoryAsync(int categoryId, UpdateCategoryDTO categoryDTO)
        {
            var currentCategory = await _categoryRepository.GetCategoryByIdAsync(categoryId);
            if (currentCategory != null)
            {
                currentCategory.CategoryName = categoryDTO.CategoryName;
                await _categoryRepository.UpdateCategoryAsync(categoryId, currentCategory);
            }
        }
    }
}
