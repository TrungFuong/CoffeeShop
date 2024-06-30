using CoffeeShop.Repositories.Interfaces;

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
        Task<int> CommitAsync();
        int Commit();
    }
}
