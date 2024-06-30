using CoffeeShop.Models;
using CoffeeShop.Repositories.Interfaces;

namespace CoffeeShop.Repositories.Implements
{
    public class ReceiptDetailRepository : GenericRepository<ReceiptDetail>, IReceiptDetailRepository
    {
        private readonly CoffeeShopDBContext _context;
        public ReceiptDetailRepository(CoffeeShopDBContext context) : base(context)
        {
            _context = context;
        }
    }
}
