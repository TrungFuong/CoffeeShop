using CoffeeShop.Models;
using CoffeeShop.Repositories.Interfaces;

namespace CoffeeShop.Repositories.Implements
{
    public class PayRateRepository: GenericRepository<PayRate>, IPayRateRepository
    {
        private readonly CoffeeShopDBContext _context;
        public PayRateRepository(CoffeeShopDBContext context) : base(context)
        {
            _context = context;
        }
    }
}
