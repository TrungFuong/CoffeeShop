using CoffeeShop.DTOs;
using CoffeeShop.Models;
using CoffeeShop.Repositories;

namespace CoffeeShop.Services
{
    public class ProductImageService : IProductImageService
    {
        private readonly IProductImageRepository _productImageRepository;
        public ProductImageService(IProductImageRepository productImageRepository)
        {
            _productImageRepository = productImageRepository;
        }

        public async Task AddProductImageAsync(ProductImageRequestDTO productImageDTO)
        {
            var productImage = new ProductImage
            {
                ProductImagePath = productImageDTO.ProductImagePath,
                ProductImageDescription = productImageDTO.ProductImageDescription,
                ProductId = productImageDTO.ProductId
            };
            await _productImageRepository.AddProductImageAsync(productImage);
        }

        public async Task DeleteProductImageAsync(Guid productImageId)
        {
            var currentProductImage = await _productImageRepository.GetProductImageByIdAsync(productImageId);
            if (currentProductImage != null)
            {
                await _productImageRepository.DeleteProductImageAsync(productImageId);
            }
        }

        public async Task<IEnumerable<ProductImageResponseDTO>> GetAllProductImageAsync()
        {
            var productImages = await _productImageRepository.GetAllProductImagesAsync();
            return productImages.Select(pi => new ProductImageResponseDTO
            {
                ProductImageId = pi.ProductImageId,
                ProductImagePath = pi.ProductImagePath,
                ProductImageDescription = pi.ProductImageDescription,
                ProductId = pi.ProductImageId
            });
        }

        public async Task<ProductImageResponseDTO> GetProductImageByIdAsync(Guid ProductImageId)
        {
            var productImage = await _productImageRepository.GetProductImageByIdAsync(ProductImageId);
            return new ProductImageResponseDTO
            {
                ProductImageId = productImage.ProductImageId,
                ProductImagePath = productImage.ProductImagePath,
                ProductImageDescription = productImage.ProductImageDescription,
                ProductId = productImage.ProductId
            };
        }


        public async Task UpdateProductImageAsync(Guid productImageId, ProductImageRequestDTO productImageDTO)
        {
            var currentProductImage = await _productImageRepository.GetProductImageByIdAsync(productImageId);

            currentProductImage.ProductImagePath = productImageDTO.ProductImagePath;
            currentProductImage.ProductImageDescription = productImageDTO.ProductImageDescription;
            currentProductImage.ProductId = productImageDTO.ProductId;
            await _productImageRepository.UpdateProductImageAsync(productImageId, currentProductImage);

        }
    }
}