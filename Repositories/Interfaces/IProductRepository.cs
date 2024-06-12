using CoffeeShop.Models;

namespace CoffeeShop.Repositories.Interfaces
{
    public interface IProductRepository
    {
        Task<IEnumerable<Product>> GetAllProductsAsync();
        Task<Product> GetProductByIdAsync(int productId);
        Task<Product> GetProductByNameAsync(string productName);
        void AddProduct(Product product);
        void UpdateProduct(Product product, int categoryId);
        void DeleteProduct(int productId);
    }
}
