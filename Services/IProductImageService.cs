using CoffeeShop.DTOs;
using CoffeeShop.Models;
using Microsoft.Build.Framework;

namespace CoffeeShop.Services
{
    public interface IProductImageService
    {
        Task<IEnumerable<GetProductImageDTO>> GetAllProductImageAsync();
        Task<GetProductImageDTO> GetProductImageByIdAsync(int productImageId);
        Task AddProductImageAsync(AddProductImageDTO addProductImageDTO);
        Task DeleteProductImageAsync(int productImageId);
        Task UpdateProductImageAsync(int productImageId, UpdateProductImageDTO updateProductImageDTO);
    }
}
