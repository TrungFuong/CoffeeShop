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

        public async Task DeleteCategoryAsync(Guid categoryId)
        {
            var category = await GetCategoryByIdAsync(categoryId);
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

        public async Task<Category> GetCategoryByIdAsync(Guid categoryId)
        {
            return await _dbContext.Categories.FindAsync(categoryId);
        }

        public async Task<Category> GetCategoryByNameAsync(string categoryName)
        {
            return await _dbContext.Categories.FirstOrDefaultAsync(c => c.CategoryName == categoryName);
        }

        public async Task UpdateCategoryAsync(Guid categoryId, Category category)
        {
            var currentCategory = await GetCategoryByIdAsync(categoryId);
            if (currentCategory != null)
            {
                //existingCategory.CategoryName = category.CategoryName;
                _dbContext.Update(currentCategory);
                await _dbContext.SaveChangesAsync();
            }
        }
    }
}
