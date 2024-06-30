using CoffeeShop.Models;
using CoffeeShop.Repositories.Interfaces;

namespace CoffeeShop.Repositories.Implements
{
    public class ProductRepository : GenericRepository<Product>, IProductRepository
    {
        private readonly CoffeeShopDBContext _context;
        public ProductRepository(CoffeeShopDBContext context) : base(context)
        {
        }
    }
}
