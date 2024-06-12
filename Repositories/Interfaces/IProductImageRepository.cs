using CoffeeShop.Models;

namespace CoffeeShop.Repositories.Interfaces
{
    public interface IProductImageRepository
    {
        Task<IEnumerable<ProductImage>> GetAllProductImagesAsync();
        Task<ProductImage> GetProductImageByIdAsync(Guid ProductImageId);
        Task AddProductImageAsync(ProductImage productImage);
        Task DeleteProductImageAsync(Guid ProductImageId);
        Task UpdateProductImageAsync(Guid ProductImageId, ProductImage productImage);
    }
}
