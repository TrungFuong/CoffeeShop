using CoffeeShop.Repositories.Implements;
using CoffeeShop.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore.Storage;

namespace CoffeeShop.UnitOfWork
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly CoffeeShopDBContext _context;
        private ICategoryRepository _categoryRepository;
        private ICheckTimeRepository _checkTimeRepository;
        private ICustomerRepository _customerRepository;
        private IPayRateRepository _payRateRepository;
        private IProductRepository _productRepository;
        private IReceiptDetailRepository _receiptDetailRepository;
        private IReceiptRepository _receiptRepository;
        private ISalaryRepository _salaryRepository;
        private IUserRepository _userRepository;
        private IRefreshTokenRepository _refreshTokenRepository;
        private ITokenRepository _tokenRepository;

        public UnitOfWork(CoffeeShopDBContext context)
        {
            _context = context;
        }

        public ICategoryRepository CategoryRepository
            => _categoryRepository ??= new CategoryRepository(_context);

        public ICheckTimeRepository CheckTimeRepository
            => _checkTimeRepository ??= new CheckTimeRepository(_context);

        public ICustomerRepository CustomerRepository
            => _customerRepository ??= new CustomerRepository(_context);

        public IPayRateRepository PayRateRepository
            => _payRateRepository ??= new PayRateRepository(_context);

        public IProductRepository ProductRepository
            => _productRepository ??= new ProductRepository(_context);
        
        public IReceiptDetailRepository ReceiptDetailRepository
            => _receiptDetailRepository ??= new ReceiptDetailRepository(_context);

        public IReceiptRepository ReceiptRepository
            => _receiptRepository ??= new ReceiptRepository(_context);

        public ISalaryRepository SalaryRepository
            => _salaryRepository ??= new SalaryRepository(_context);
       
        public IUserRepository  UserRepository
            => _userRepository ??= new UserRepository(_context);
        public IRefreshTokenRepository RefreshTokenRepository
            => _refreshTokenRepository ??= new RefreshTokenRepository(_context);

        public ITokenRepository TokenRepository
            => _tokenRepository ??= new TokenRepository(_context);

        public async Task<int> CommitAsync()
        {
            return await _context.SaveChangesAsync();
        }
        public int Commit()
        {
            return _context.SaveChanges();
        }

        public async Task<IDbContextTransaction> BeginTransactionAsync()
        {
            return await _context.Database.BeginTransactionAsync();
        }
    }
}
