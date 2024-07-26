using CoffeeShop.Models;
using CoffeeShop.Repositories.Interfaces;

namespace CoffeeShop.Repositories.Implements
{
    public class RefreshTokenRepository : GenericRepository<RefreshToken>, IRefreshTokenRepository
    {
        private readonly CoffeeShopDBContext _context;
        public RefreshTokenRepository(CoffeeShopDBContext context) : base(context)
        {
            _context = context;
        }
    }
}
