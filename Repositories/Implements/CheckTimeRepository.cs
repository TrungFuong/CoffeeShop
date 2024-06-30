using CoffeeShop.Models;
using CoffeeShop.Repositories.Interfaces;

namespace CoffeeShop.Repositories.Implements
{
    public class CheckTimeRepository : GenericRepository<CheckTime>, ICheckTimeRepository
    {
        private readonly CoffeeShopDBContext _context;
        public CheckTimeRepository(CoffeeShopDBContext context) : base(context) 
        {
            _context = context;
        }
    }
}
