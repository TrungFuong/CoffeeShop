using CoffeeShop.Models;
using System.Collections.Generic;

namespace CoffeeShop.Repositories
{
    public interface ICategoryRepository
    {
        Task<IEnumerable<Category>> GetAllCategoriesAsync();
        Task<Category> GetCategoryByIdAsync(int categoryId);
        Task<Category> GetCategoryByNameAsync(string categoryName);
        Task AddCategoryAsync(Category category);
        Task DeleteCategoryAsync(int categoryId);
        Task UpdateCategoryAsync(int categoryId, Category category);
    }
}
