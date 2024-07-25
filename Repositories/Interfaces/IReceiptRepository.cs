using CoffeeShop.Models;

namespace CoffeeShop.Repositories.Interfaces
{
    public interface IReceiptRepository : IGenericRepository<Receipt>
    {
        Task<Receipt?> GetReceiptDetailAsync(Guid id);
    }
}
