using CoffeeShop.Models;
using CoffeeShop.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace CoffeeShop.Repositories.Implements
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
                var products = await _dbContext.Products
                                               .Where(p => p.CategoryId == categoryId)
                                               .ToListAsync();

                if (products.Any())
                {
                    throw new InvalidOperationException("Cannot delete category with products in it.");
                }

                category.IsDeleted = true;
                _dbContext.Categories.Update(category);
                await _dbContext.SaveChangesAsync();
            }
        }

        public async Task<IEnumerable<Category>> GetAllCategoriesAsync()
        {
            return await _dbContext.Categories.Where(c => c.IsDeleted == false).ToListAsync();
        }

        public async Task<Category> GetCategoryByIdAsync(Guid categoryId)
        {
            return await _dbContext.Categories.FindAsync(categoryId);
        }

        public async Task<Category> GetCategoryByNameAsync(string categoryName)
        {
            return await _dbContext.Categories.FirstOrDefaultAsync(c => c.CategoryName == categoryName);
        }

        public async Task<IEnumerable<Product>> GetProductByCategoryAsync(Guid categoryId)
        {
            var products = await _dbContext.Products
                .Where(p => p.CategoryId == categoryId)
                .ToListAsync();
            return products;
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
