using CoffeeShop.Models;
using CoffeeShop.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;
using Microsoft.VisualBasic;

namespace CoffeeShop.Repositories.Implements
{
    public class ProductImageRepository : IProductImageRepository
    {
        private readonly CoffeeShopDBContext _dbContext;

        public ProductImageRepository(CoffeeShopDBContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task AddProductImageAsync(ProductImage productImage)
        {
            _dbContext.ProductImages.AddAsync(productImage);
            await _dbContext.SaveChangesAsync();
        }

        public async Task DeleteProductImageAsync(Guid ProductImageId)
        {
            var currentImage = await GetProductImageByIdAsync(ProductImageId);
            if (currentImage != null)
            {
                _dbContext.ProductImages.Remove(currentImage);
                await _dbContext.SaveChangesAsync();
            }
        }

        public async Task<IEnumerable<ProductImage>> GetAllProductImagesAsync()
        {
            return await _dbContext.ProductImages.ToListAsync();
        }

        public async Task<ProductImage> GetProductImageByIdAsync(Guid ImageId)
        {
            return await _dbContext.ProductImages.FindAsync(ImageId);
        }

        public async Task UpdateProductImageAsync(Guid ProductImageId, ProductImage productImage)
        {
            var existingImage = await GetProductImageByIdAsync(ProductImageId);
            if (existingImage != null)
            {
                existingImage.ProductImagePath = productImage.ProductImagePath;
                await _dbContext.SaveChangesAsync();
            }
        }
    }
}