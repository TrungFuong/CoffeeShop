using CoffeeShop.DTOs;
using CoffeeShop.Models;
using Microsoft.Build.Framework;

namespace CoffeeShop.Services
{
    public interface IProductImageService
    {
        Task<IEnumerable<ProductImageResponseDTO>> GetAllProductImageAsync();
        Task<ProductImageResponseDTO> GetProductImageByIdAsync(Guid productImageId);
        Task AddProductImageAsync(ProductImageRequestDTO addProductImageDTO);
        Task DeleteProductImageAsync(Guid productImageId);
        Task UpdateProductImageAsync(Guid productImageId, ProductImageRequestDTO updateProductImageDTO);
    }
}
