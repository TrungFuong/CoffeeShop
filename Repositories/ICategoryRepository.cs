using CoffeeShop.Models;
using System.Collections.Generic;

namespace CoffeeShop.Repositories
{
    public interface ICategoryRepository
    {
        Task<IEnumerable<Category>> GetAllCategoriesAsync();
        Task<Category> GetCategoryByIdAsync(Guid categoryId);
        Task<Category> GetCategoryByNameAsync(string categoryName);
        Task AddCategoryAsync(Category category);
        Task DeleteCategoryAsync(Guid categoryId);
        Task UpdateCategoryAsync(Guid categoryId, Category category);
        Task<IEnumerable<Product>> GetProductByCategoryAsync(Guid categoryId);
    }
}
