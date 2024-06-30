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

            //Seed data
            var adminId = Guid.NewGuid();
            var normalUserId = Guid.NewGuid();
            var bossId = Guid.NewGuid();
            var employeeId = Guid.NewGuid();
            var customerId = Guid.NewGuid();
            var receiptId = Guid.NewGuid();
            var coffeeCateId = Guid.NewGuid();
            var teaCateId = Guid.NewGuid();
            var pastryCateId = Guid.NewGuid();
            var productId1 = Guid.NewGuid();
            var productId2 = Guid.NewGuid();
            var productId3 = Guid.NewGuid();
            var productId4 = Guid.NewGuid();
            var payRateId1 = Guid.NewGuid();
            var payRateId2 = Guid.NewGuid();
            var payRateId3 = Guid.NewGuid();
            var salaryId1 = Guid.NewGuid();
            

            modelBuilder.Entity<Category>().HasData(
              new Category { CategoryId = coffeeCateId, CategoryName = "Coffee", IsDeleted = false },
              new Category { CategoryId = teaCateId, CategoryName = "Tea", IsDeleted = false },
              new Category { CategoryId = pastryCateId, CategoryName = "Pastry", IsDeleted = false }
          );

            modelBuilder.Entity<Product>().HasData(
                new Product { ProductId = productId1, ProductName = "Espresso", CategoryId = coffeeCateId, ProductPrice = 25000, ProductDescription = "Coffee shot", IsDeleted = false, ImageUrl = "" },
                new Product { ProductId = productId2, ProductName = "Cappuccino", CategoryId = coffeeCateId, ProductPrice = 30000, ProductDescription = "Milky coffee", IsDeleted = false, ImageUrl = "" },
                new Product { ProductId = productId3, ProductName = "Green Tea", CategoryId = teaCateId, ProductPrice = 15000, ProductDescription = "Green thing" , IsDeleted = false, ImageUrl = "" },
                new Product { ProductId = productId4, ProductName = "Croissant", CategoryId = pastryCateId, ProductPrice = 20000, ProductDescription = "It's pronounced \"KhoaSoong\" ", IsDeleted = false, ImageUrl = "" }
            );

            modelBuilder.Entity<User>().HasData(
                new User { UserId = employeeId, Username = "test", IsDeleted = false, Role = Models.Enums.EnumRole.Employee}
                );

            modelBuilder.Entity<PayRate>().HasData(
                new PayRate { PayRateId = payRateId1, PayrateName = "Hoc viec", PayrateValue = 20000, IsDeleted = false },
                new PayRate { PayRateId = payRateId2, PayrateName = "Junior", PayrateValue = 25000 , IsDeleted = false },
                new PayRate { PayRateId = payRateId3, PayrateName = "Senior", PayrateValue = 30000 , IsDeleted = false }
            );

            modelBuilder.Entity<Salary>().HasData(
                new Salary { SalaryId = salaryId1, UserId = employeeId, PayrateId = payRateId1, TotalSalary = 250000, IsDeleted = false }
            );

            modelBuilder.Entity<Customer>().HasData(
                new Customer { CustomerId = customerId, CustomerName = "Jane Smith", CustomerPhone = "0934516636", CustomerBirthday = DateTime.Now, IsDeleted = false }
            );

            modelBuilder.Entity<Receipt>().HasData(
                new Receipt { ReceiptId = receiptId, CustomerId = customerId, UserId = employeeId, Table = 1, ReceiptDate = DateTime.Now, ReceiptTotal = 70000, IsDeleted = false }
            );

            modelBuilder.Entity<ReceiptDetail>().HasData(
                new ReceiptDetail { ReceiptId = receiptId, ProductId = productId1, ProductQuantity = 2 },
                new ReceiptDetail { ReceiptId = receiptId, ProductId = productId4, ProductQuantity = 1 }
            );
        }
    }
}

