using CoffeeShop.Models;
using CoffeeShop.Repositories.Interfaces;

namespace CoffeeShop.Repositories.Implements
{
    public class ReceiptRepository : GenericRepository<Receipt>,  IReceiptRepository  
    {
        private readonly CoffeeShopDBContext _context;
        public ReceiptRepository(CoffeeShopDBContext context) : base(context)
        {
            _context = context;
        }
    }
}
