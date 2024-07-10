using CoffeeShop.Models;
using CoffeeShop.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace CoffeeShop.Repositories.Implements
{
    public class TokenRepository : GenericRepository<Token>, ITokenRepository
    {
        private readonly CoffeeShopDBContext _context;
        public TokenRepository(CoffeeShopDBContext context) : base(context)
        {
            _context = context;
        }
    }
}