using CoffeeShop.Models;
using CoffeeShop.Repositories.Interfaces;

namespace CoffeeShop.Repositories.Implements
{
    public class UserRepository : GenericRepository<User>, IUserRepository
    {
        private readonly CoffeeShopDBContext _context;
        public UserRepository(CoffeeShopDBContext context) : base(context)
        {
            _context = context;
        }
    }
}
