using CoffeeShop.Models;
using Microsoft.EntityFrameworkCore;

namespace CoffeeShop.Repositories
{
    public class CategoryRepository : ICategoryRepository
    {
        private readonly CoffeeShopDBContext _dbContext;

        public CategoryRepository(CoffeeShopDBContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task AddCategoryAsync(Category category)
        {
            await _dbContext.Categories.AddAsync(category);
            await _dbContext.SaveChangesAsync();
        }

        public async Task DeleteCategoryAsync(int categoryId)
        {
            var category = await _dbContext.Categories.FindAsync(categoryId);
            if (category != null)
            {
                _dbContext.Categories.Remove(category);
                await _dbContext.SaveChangesAsync();
            }
        }

        public async Task<IEnumerable<Category>> GetAllCategoriesAsync()
        {
            return await _dbContext.Categories.ToListAsync();
        }

        public async Task<Category> GetCategoryByIdAsync(int categoryId)
        {
            return await _dbContext.Categories.FindAsync(categoryId);
        }

        public async Task<Category> GetCategoryByNameAsync(string categoryName)
        {
            return await _dbContext.Categories.FirstOrDefaultAsync(c => c.CategoryName == categoryName);
        }

        public async Task UpdateCategoryAsync(int categoryId, Category category)
        {
            var existingCategory = await _dbContext.Categories.FindAsync(categoryId);
            if (existingCategory != null)
            {
                existingCategory.CategoryName = category.CategoryName;
                await _dbContext.SaveChangesAsync();
            }
        }
    }
}
