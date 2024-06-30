using CoffeeShop.Models;
using CoffeeShop.Repositories.Interfaces;

namespace CoffeeShop.Repositories.Implements
{
    public class SalaryRepository : GenericRepository<Salary>, ISalaryRepository
    {
        private readonly CoffeeShopDBContext _context;
        public SalaryRepository(CoffeeShopDBContext context) : base(context)
        {
            _context = context;
        }
    }
}
