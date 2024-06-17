using CoffeeShop.DTOs;
using CoffeeShop.Models;
using CoffeeShop.Exceptions;
using CoffeeShop.Repositories.Interfaces;

namespace CoffeeShop.Services
{
    public class CategoryService : ICategoryService
    {
        private readonly ICategoryRepository _categoryRepository;

        public CategoryService(ICategoryRepository categoryRepository)
        {
            _categoryRepository = categoryRepository;
        }

        public async Task AddCategoryAsync(CategoryRequestDTO categoryDTO)
        {
            var category = new Category
            {
                CategoryName = categoryDTO.CategoryName
            };
            _categoryRepository.AddCategoryAsync(category);
        }

        public async Task DeleteCategoryAsync(Guid categoryId)
        {
            var currentCategory = await _categoryRepository.GetCategoryByIdAsync(categoryId);
            if (currentCategory != null)
            {
                 _categoryRepository.DeleteCategoryAsync(categoryId);
            }
            else
            {
                throw new NotFoundException(typeof(Category), categoryId);
            }
        }

        public async Task<IEnumerable<CategoryResponseDTO>> GetAllCategoriesAsync()
        {
            var categories = await _categoryRepository.GetAllCategoriesAsync();
            if (categories == null)
            {
                throw new NullReferenceException("No category found.");
            }
            return categories.Select(c => new CategoryResponseDTO
            {
                CategoryId = c.CategoryId,
                CategoryName = c.CategoryName
            });
        }

        public async Task<CategoryResponseDTO> GetCategoryByIdAsync(Guid categoryId)
        {
            var category = await _categoryRepository.GetCategoryByIdAsync(categoryId);
            if (category == null)
            {
                throw new NotFoundException(typeof(Category), categoryId);
            }
            return new CategoryResponseDTO
            {
                CategoryId = category.CategoryId,
                CategoryName = category.CategoryName
            };
        }

        public async Task<CategoryResponseDTO> GetCategoryByNameAsync(string categoryName)
        {
            var category = await _categoryRepository.GetCategoryByNameAsync(categoryName);
            if (category == null)
            {
                throw new NotFoundException(typeof(Category), Guid.Empty);
            }
            return new CategoryResponseDTO
            {
                CategoryId = category.CategoryId,
                CategoryName = category.CategoryName
            };
        }

        public async Task<IEnumerable<ProductResponseDTO>> GetProductByCategory(Guid categoryId)
        {
            var category = await _categoryRepository.GetCategoryByIdAsync(categoryId);
            var products = await _categoryRepository.GetProductByCategoryAsync(categoryId);
            return products.Select(p => new ProductResponseDTO
            {
                ProductId = p.ProductId,
                ProductName = p.ProductName,
                ProductDescription = p.ProductDescription,
                ProductPrice = p.ProductPrice,
                CategoryId = p.CategoryId
            });
        }

        public async Task UpdateCategoryAsync(Guid categoryId, CategoryRequestDTO categoryDTO)
        {
            var currentCategory = await _categoryRepository.GetCategoryByIdAsync(categoryId);
            if (currentCategory != null)
            {
                currentCategory.CategoryName = categoryDTO.CategoryName;
                _categoryRepository.UpdateCategoryAsync(categoryId, currentCategory);
            }
            else
            {
                throw new NotFoundException(typeof(Category), categoryId);
            }
        }
    }
}
