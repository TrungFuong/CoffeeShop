using CoffeeShop.DTOs;
using CoffeeShop.Models;
using CoffeeShop.Repositories;
using Microsoft.CodeAnalysis.CSharp;

namespace CoffeeShop.Services
{
    public class ProductImageService : IProductImageService
    {
        private readonly IProductImageRepository _productImageRepository;
        public ProductImageService(IProductImageRepository productImageRepository)
        {
            _productImageRepository = productImageRepository;
        }

        public async Task AddProductImageAsync(AddProductImageDTO addProductImageDTO)
        {
            var productImage = new ProductImage
            {
                ProductImagePath = addProductImageDTO.ProductImagePath,
                ProductImageDescription = addProductImageDTO.ProductImageDescription,
                ProductId = addProductImageDTO.ProductId
            };
            await _productImageRepository.AddProductImageAsync(productImage);
        }

        public async Task DeleteProductImageAsync(int productImageId)
        {
            var currentProductImage = await _productImageRepository.GetProductImageByIdAsync(productImageId);
            if (currentProductImage != null)
            {
                await _productImageRepository.DeleteProductImageAsync(productImageId);
            }
        }

        public async Task<IEnumerable<GetProductImageDTO>> GetAllProductImageAsync()
        {
            var productImages = await _productImageRepository.GetAllProductImagesAsync();
            return productImages.Select(pi => new GetProductImageDTO
            {
                ProductImageId = pi.ProductImageId,
                ProductImagePath = pi.ProductImagePath,
                ProductImageDescription = pi.ProductImageDescription,
                ProductId = pi.ProductImageId
            });
        }

        public async Task<GetProductImageDTO> GetProductImageByIdAsync(int ProductImageId)
        {
            var productImage = await _productImageRepository.GetProductImageByIdAsync(ProductImageId);
            if (productImage == null) return null;
            return new GetProductImageDTO
            {
                ProductImageId = productImage.ProductImageId,
                ProductImagePath = productImage.ProductImagePath,
                ProductImageDescription = productImage.ProductImageDescription,
                ProductId = productImage.ProductId
            };
        }

        public async Task UpdateProductImageAsync(int productImageId, UpdateProductImageDTO updateProductImageDTO)
        {
            var currentProductImage = await _productImageRepository.GetProductImageByIdAsync(productImageId);
            if (currentProductImage == null)
            {
                currentProductImage.ProductImagePath = updateProductImageDTO.ProductImagePath;
                currentProductImage.ProductImageDescription = updateProductImageDTO.ProductImageDescription;
                currentProductImage.ProductId = updateProductImageDTO.ProductId;
            };
        }
    }
}
