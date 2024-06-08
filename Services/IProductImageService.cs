using CoffeeShop.DTOs;
using CoffeeShop.Models;
using Microsoft.Build.Framework;

namespace CoffeeShop.Services
{
    public interface IProductImageService
    {
        Task<IEnumerable<ProductResponseDTO>> GetAllProductImageAsync();
        Task<ProductResponseDTO> GetProductImageByIdAsync(int productImageId);
        Task AddProductImageAsync(ProductImageRequestDTO addProductImageDTO);
        Task DeleteProductImageAsync(int productImageId);
        Task UpdateProductImageAsync(int productImageId, UpdateProductImageDTO updateProductImageDTO);
    }
}
