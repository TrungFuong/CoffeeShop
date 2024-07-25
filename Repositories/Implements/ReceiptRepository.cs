using CoffeeShop.Models;
using CoffeeShop.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;


namespace CoffeeShop.Repositories.Implements
{
    public class ReceiptRepository : GenericRepository<Receipt>,  IReceiptRepository  
    {
        private readonly CoffeeShopDBContext _context;
        public ReceiptRepository(CoffeeShopDBContext context) : base(context)
        {
            _context = context;
        }
        public async Task<Receipt?> GetReceiptDetailAsync(Guid id)
        {
            return await _context.Receipts
                .Include(x => x.User)
                .Include(x => x.Customer)
                .Include(x => x.ReceiptDetails)
                .ThenInclude(x => x.Product)
                .FirstOrDefaultAsync(x => x.ReceiptId == id);
        }
    }
}
