using CoffeeShop.Models;
using System.Collections.Generic;

namespace CoffeeShop.Repositories.Interfaces
{
    public interface IProductRepostory
    {
        Task<IEnumerable<Product>> GetAllProductsAsync();
        Task<Product> GetProductByIdAsync(Guid productId);
        Task<Product> AddProductAsync(Product product);
        void UpdateProductAsync(Product product);
    }
}
