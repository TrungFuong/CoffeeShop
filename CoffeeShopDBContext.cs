using Microsoft.EntityFrameworkCore;
using CoffeeShop.Models;

namespace CoffeeShop
{
    public class CoffeeShopDBContext : DbContext
    {
        public CoffeeShopDBContext(DbContextOptions<CoffeeShopDBContext> dbContextOptions) : base(dbContextOptions) { }
        public DbSet<Category> Categories { get; set; }
        public DbSet<CheckTime> CheckTimes { get; set; }
        public DbSet<Customer> Customers { get; set; }
        public DbSet<User> Users { get; set; }
        public DbSet<PayRate> PayRates { get; set; }
        public DbSet<Product> Products { get; set; }
        public DbSet<Receipt> Receipts { get; set; }
        public DbSet<Salary> Salaries { get; set; }
        public DbSet<Token> Tokens { get; set; }
        public DbSet<RefreshToken> RefreshTokens { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<PayRate>()
                .HasMany(payRate => payRate.Salaries)
                .WithOne(salary => salary.PayRate);

            modelBuilder.Entity<Salary>()
                .HasOne(salary => salary.PayRate)
                .WithMany(payRate => payRate.Salaries)
                .HasForeignKey(salary => salary.PayrateId);

            modelBuilder.Entity<Salary>()
                .HasOne(salary => salary.User)
                .WithOne(employee => employee.Salary);

            modelBuilder.Entity<User>()
                .HasOne(employee => employee.Salary)
                .WithOne(salary => salary.User);

            modelBuilder.Entity<User>()
                .HasMany(employee => employee.CheckTimes)
                .WithOne(checkTime => checkTime.User);

            modelBuilder.Entity<User>()
                .HasMany(employee => employee.Receipts)
                .WithOne(receipt => receipt.User);

            modelBuilder.Entity<CheckTime>()
                .HasOne(checkTime => checkTime.User)
                .WithMany(employee => employee.CheckTimes)
                .HasForeignKey(checkTime => checkTime.UserId);

            modelBuilder.Entity<Receipt>()
                .HasOne(receipt => receipt.User)
                .WithMany(employee => employee.Receipts)
                .HasForeignKey(receipt => receipt.UserId);

            modelBuilder.Entity<Receipt>()
                .HasOne(receipt => receipt.Customer)
                .WithOne(customer => customer.Receipt)
                .HasForeignKey<Receipt>(receipt => receipt.CustomerId);

            modelBuilder.Entity<Receipt>()
                .HasMany(receipt => receipt.ReceiptDetails)
                .WithOne(receiptDetail => receiptDetail.Receipt);

            modelBuilder.Entity<Customer>()
                .HasOne(customer => customer.Receipt)
                .WithOne(receipt => receipt.Customer);

            modelBuilder.Entity<Product>()
                .HasOne(product => product.Category)
                .WithMany(category => category.Products)
                .HasForeignKey(product => product.CategoryId);

            modelBuilder.Entity<Product>()
                .HasMany(product => product.ReceiptDetails)
                .WithOne(receiptDetail => receiptDetail.Product);

            modelBuilder.Entity<ReceiptDetail>()
                .HasKey(receiptDetail => new { receiptDetail.ReceiptId, receiptDetail.ProductId });

            modelBuilder.Entity<ReceiptDetail>()
                .HasOne(receiptDetail => receiptDetail.Product)
                .WithMany(product => product.ReceiptDetails)
                .HasForeignKey(receiptDetail => receiptDetail.ProductId);

            modelBuilder.Entity<ReceiptDetail>()
                .HasOne(receiptDetail => receiptDetail.Receipt)
                .WithMany(receipt => receipt.ReceiptDetails)
                .HasForeignKey(receiptDetail => receiptDetail.ReceiptId);

            modelBuilder.Entity<Category>()
                .HasMany(category => category.Products)
                .WithOne(product => product.Category);

        }
    }
}

