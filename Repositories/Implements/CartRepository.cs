using CoffeeShop.Models;
using CoffeeShop.Repositories.Interfaces;

namespace CoffeeShop.Repositories.Implements
{
    public class CartRepository : GenericRepository<Cart>, ICartRepository
    {
        private readonly CoffeeShopDBContext _context;
        public CartRepository(CoffeeShopDBContext context) : base(context)
        {
            _context = context;
        }
    }
}
