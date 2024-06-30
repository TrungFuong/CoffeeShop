using CoffeeShop.Models;
using CoffeeShop.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace CoffeeShop.Repositories.Implements
{
    public class CategoryRepository : GenericRepository<Category>, ICategoryRepository
    {
        private readonly CoffeeShopDBContext _context;

        public CategoryRepository(CoffeeShopDBContext context) : base(context)
        {
        }
    }
}
