using CoffeeShop.DTOs;
using CoffeeShop.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace CoffeeShop.Services
{
    public interface ICategoryService
    {
        Task<IEnumerable<CategoryResponseDTO>> GetAllCategoriesAsync();
        Task<CategoryResponseDTO> GetCategoryByIdAsync(int categoryId);
        Task<CategoryResponseDTO> GetCategoryByNameAsync(string categoryName);
        Task AddCategoryAsync(CategoryRequestDTO categoryDTO);
        Task DeleteCategoryAsync(int categoryId);
        Task UpdateCategoryAsync(int categoryId, CategoryRequestDTO categoryDTO);
    }
}