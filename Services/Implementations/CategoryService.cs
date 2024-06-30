using CoffeeShop.DTOs;
using CoffeeShop.Models;
using CoffeeShop.Exceptions;
using CoffeeShop.Repositories.Interfaces;
using CoffeeShop.UnitOfWork;

namespace CoffeeShop.Services.Implementations
{
    public class CategoryService : ICategoryService
    {
        private readonly IUnitOfWork _unitOfWork;

        public CategoryService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<CategoryResponseDTO> CreateCategoryAsync(CategoryRequestDTO categoryRequest)
        {
            var categoryNameExisted = await _unitOfWork.CategoryRepository.GetAsync(c => c.CategoryName == categoryRequest.CategoryName);
            if (categoryNameExisted != null)
            {
                throw new ArgumentException("Category is already existed. Please enter a different category");
            }

            var newCategory = new Category
            {
                CategoryName = categoryRequest.CategoryName,
            };

            await _unitOfWork.CategoryRepository.AddAsync(newCategory);
            if (await _unitOfWork.CommitAsync() > 0)
            {
                var categoryResponse = new CategoryResponseDTO
                {
                    CategoryId = newCategory.CategoryId,
                    CategoryName = newCategory.CategoryName
                };
                return categoryResponse;
            }
            else
            {
                throw new Exception("Failed to create category");
            }
        }

        public async Task<IEnumerable<CategoryResponseDTO>> GetAllCategoriesAsync()
        {
            var categories = await _unitOfWork.CategoryRepository.GetAllAsync(c => !c.IsDeleted);
            return categories.Select(c => new CategoryResponseDTO
            {
                CategoryId = c.CategoryId,
                CategoryName = c.CategoryName,
            });
        }
    }
}
