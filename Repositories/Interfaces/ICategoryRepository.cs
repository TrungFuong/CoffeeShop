using CoffeeShop.Models;
using Newtonsoft.Json.Bson;
using System.Collections.Generic;
using System.Runtime.CompilerServices;

namespace CoffeeShop.Repositories.Interfaces
{
    public interface ICategoryRepository
    {
        Task<IEnumerable<Category>> GetAllCategoriesAsync();
        Task<Category> GetCategoryByIdAsync(Guid categoryId);
        Task<Category> GetCategoryByNameAsync(string categoryName);
        Task<Category> AddCategoryAsync(Category category);
        Task DeleteCategoryAsync(Guid categoryId);
        Task UpdateCategoryAsync(Guid categoryId, Category category);
        Task<IEnumerable<Product>> GetProductByCategoryAsync(Guid categoryId);
    }
}
