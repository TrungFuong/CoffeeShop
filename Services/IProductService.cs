using CoffeeShop.DTOs;

namespace CoffeeShop.Services
{
    public interface IProductService
    {
        Task<(IEnumerable<ProductResponseDTO> data, int totalCount)> GetAllProductsAsync(int pageNumber, Guid? category, string? search, string? sortOrder, string? sortBy = "productName", string includeProperties = "", string? newProductName = "");

        Task<ProductResponseDTO> GetProductDetail(Guid productId);

        Task<ProductResponseDTO> CreateProductAsync(ProductRequestDTO productRequest, FileUpload fileUpload);

        Task<ProductResponseDTO> DeleteProductAsync(Guid productId);

        Task<ProductResponseDTO> UpdateProduct(Guid id, ProductRequestDTO productRequest);

        FileUpload ConvertToFileUpload(IFormFile formFile);
    }
}
