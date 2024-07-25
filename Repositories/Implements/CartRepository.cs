using CoffeeShop.Models;
using CoffeeShop.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace CoffeeShop.Repositories.Implements
{
    public class CartRepository : GenericRepository<Cart>, ICartRepository
    {
        private readonly CoffeeShopDBContext _context;
        public CartRepository(CoffeeShopDBContext context) : base(context)
        {
            _context = context;
        }
        public async Task<Cart?> GetCartDetailAsync(Guid id)
        {
            return await _context.Carts
                .Include(x => x.Customer)
                .Include(x => x.CartDetails)
                .ThenInclude(x => x.Product)
                .FirstOrDefaultAsync(x => x.CartId == id);
        }
    }
}
