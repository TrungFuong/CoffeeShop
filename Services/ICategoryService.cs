using CoffeeShop.DTOs;
using CoffeeShop.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace CoffeeShop.Services
{
    public interface ICategoryService
    {
        Task<IEnumerable<GetCategoryDTO>> GetAllCategoriesAsync();
        Task<GetCategoryDTO> GetCategoryByIdAsync(int categoryId);
        Task<GetCategoryDTO> GetCategoryByNameAsync(string categoryName);
        Task AddCategoryAsync(AddCategoryDTO categoryDTO);
        Task DeleteCategoryAsync(int categoryId);
        Task UpdateCategoryAsync(int categoryId, UpdateCategoryDTO categoryDTO);
    }
}