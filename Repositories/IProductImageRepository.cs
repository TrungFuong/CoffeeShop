using CoffeeShop.Models;

namespace CoffeeShop.Repositories
{
    public interface IProductImageRepository
    {
        Task<IEnumerable<ProductImage>> GetAllProductImagesAsync();
        Task<ProductImage> GetProductImageByIdAsync(int ProductImageId);
        Task AddProductImageAsync(ProductImage productImage);
        Task DeleteProductImageAsync(int ProductImageId);
        Task UpdateProductImageAsync(int ProductImageId, ProductImage productImage);
    }
}
