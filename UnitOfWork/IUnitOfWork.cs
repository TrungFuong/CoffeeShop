using CoffeeShop.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore.Storage;

namespace CoffeeShop.UnitOfWork
{
    public interface IUnitOfWork
    {
        ICategoryRepository CategoryRepository { get; }
        ICheckTimeRepository CheckTimeRepository { get; }
        ICustomerRepository CustomerRepository { get; }
        IProductRepository ProductRepository { get; }
        IPayRateRepository PayRateRepository { get; }
        IReceiptDetailRepository ReceiptDetailRepository { get; }
        IReceiptRepository ReceiptRepository { get; }
        ISalaryRepository SalaryRepository { get; }
        IUserRepository UserRepository { get; }
        IRefreshTokenRepository RefreshTokenRepository { get; }
        ITokenRepository TokenRepository { get; }
        ICartRepository CartRepository { get; }
        ICartDetailRepository CartDetailRepository { get; }
        Task<int> CommitAsync();
        int Commit();
        Task<IDbContextTransaction> BeginTransactionAsync();
    }
}
