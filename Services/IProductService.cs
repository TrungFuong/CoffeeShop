using CoffeeShop.DTOs;
using CoffeeShop.DTOs.Request;
using CoffeeShop.DTOs.Responses;

namespace CoffeeShop.Services
{
    public interface IProductService
    {
        Task<(IEnumerable<ProductResponseDTO> data, int totalCount)> GetAllProductsAsync(int pageNumber, Guid? category, string? search, string? sortOrder, string? sortBy = "productName", string includeProperties = "", string? newProductName = "");

        Task<ProductDetailResponseDTO> GetProductDetail(Guid productId);

        Task<ProductResponseDTO> CreateProductAsync(ProductRequestDTO productRequest, IFormFile imageFile);

        Task<ProductResponseDTO> DeleteProductAsync(Guid productId);

        Task<ProductResponseDTO> UpdateProduct(Guid id, ProductUpdateRequestDTO productRequest, IFormFile imageFile);

        Task<(IEnumerable<ReportResponseDTO>, int count)> GetReports(DateTime? startDate, DateTime? endDate, int pageNumber, string? search, string? sortOrder, string? sortBy = "productName", string includeProperties = "");

        Task<byte[]> ExportToExcelAsync(DateTime? startDate, DateTime? endDate);
    }
}
