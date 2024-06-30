using CoffeeShop.Models;
using CoffeeShop.Repositories.Interfaces;

namespace CoffeeShop.Repositories.Implements
{
    public class CustomerRepository : GenericRepository<Customer>, ICustomerRepository
    {
        private readonly CoffeeShopDBContext _context;
        public CustomerRepository(CoffeeShopDBContext context) : base(context)
        {
            _context = context;
        }
    }
}
