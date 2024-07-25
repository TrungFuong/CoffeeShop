using CoffeeShop.Models;
using CoffeeShop.Repositories.Interfaces;

namespace CoffeeShop.Repositories.Implements
{
    public class CartDetailRepository : GenericRepository<CartDetail>, ICartDetailRepository
    {
        private readonly CoffeeShopDBContext _context;
        public CartDetailRepository(CoffeeShopDBContext context) : base(context)
        {
            _context = context;
        }
    }
}
