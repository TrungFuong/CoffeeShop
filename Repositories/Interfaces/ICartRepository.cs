using CoffeeShop.Models;

namespace CoffeeShop.Repositories.Interfaces
{
    public interface ICartRepository : IGenericRepository<Cart>
    {
        Task<Cart?> GetCartDetailAsync(Guid id);

    }
}
