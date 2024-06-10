using Microsoft.EntityFrameworkCore;
using CoffeeShop.Models;

namespace CoffeeShop
{
    public class CoffeeShopDBContext : DbContext
    {
        public CoffeeShopDBContext(DbContextOptions<CoffeeShopDBContext> dbContextOptions) : base(dbContextOptions) { }
        public DbSet<Account> Accounts { get; set; }
        public DbSet<Category> Categories { get; set; }
        public DbSet<CheckTime> CheckTimes { get; set; }
        public DbSet<Customer> Customers { get; set; }
        public DbSet<Employee> Employees { get; set; }
        public DbSet<PayRate> PayRates { get; set; }
        public DbSet<Product> Products { get; set; }
        public DbSet<ProductImage> ProductImages { get; set; }
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
                .HasOne(salary => salary.Employee)
                .WithOne(employee => employee.Salary);

            modelBuilder.Entity<Employee>()
                .HasOne(employee => employee.Salary)
                .WithOne(salary => salary.Employee);

            modelBuilder.Entity<Employee>()
                .HasOne(employee => employee.Account)
                .WithOne(account => account.Employee)
                .HasForeignKey<Employee>(account => account.AccountId);

            modelBuilder.Entity<Employee>()
                .HasMany(employee => employee.CheckTimes)
                .WithOne(checkTime => checkTime.Employee);

            modelBuilder.Entity<Employee>()
                .HasMany(employee => employee.Receipts)
                .WithOne(receipt => receipt.Employee);

            modelBuilder.Entity<CheckTime>()
                .HasOne(checkTime => checkTime.Employee)
                .WithMany(employee => employee.CheckTimes)
                .HasForeignKey(checkTime => checkTime.EmployeeId);

            modelBuilder.Entity<Receipt>()
                .HasOne(receipt => receipt.Employee)
                .WithMany(employee => employee.Receipts)
                .HasForeignKey(receipt => receipt.EmployeeId);

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
                .HasMany(product => product.ProductImages)
                .WithOne(productImage => productImage.Product);

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

            modelBuilder.Entity<ProductImage>()
                .HasOne(productImage => productImage.Product)
                .WithMany(product => product.ProductImages)
                .HasForeignKey(productImage => productImage.ProductId);

            //Seed data
            var adminId = Guid.NewGuid();
            var normalEmployeeId = Guid.NewGuid();
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
              new Category { CategoryId = coffeeCateId, CategoryName = "Coffee" },
              new Category { CategoryId = teaCateId, CategoryName = "Tea" },
              new Category { CategoryId = pastryCateId, CategoryName = "Pastry" }
          );

            modelBuilder.Entity<Product>().HasData(
                new Product { ProductId = productId1, ProductName = "Espresso", CategoryId = coffeeCateId, ProductPrice = 25000, ProductDescription = "Coffee shot" },
                new Product { ProductId = productId2, ProductName = "Cappuccino", CategoryId = coffeeCateId, ProductPrice = 30000, ProductDescription = "Milky coffee" },
                new Product { ProductId = productId3, ProductName = "Green Tea", CategoryId = teaCateId, ProductPrice = 15000, ProductDescription = "Green thing" },
                new Product { ProductId = productId4, ProductName = "Croissant", CategoryId = pastryCateId, ProductPrice = 20000, ProductDescription = "It's pronounced \"KhoaSoong\" " }
            );

            modelBuilder.Entity<ProductImage>().HasData(
               new ProductImage
               {
                   ProductImageId = Guid.NewGuid(),
                   ProductImagePath = "https://cdn.tgdd.vn/Files/2023/07/11/1537842/espresso-la-gi-nguyen-tac-pha-espresso-dung-chuan-202307120715077669.jpg",
                   ProductImageDescription = "Espresso coffee shot",
                   ProductId = productId1
               },
               new ProductImage
               {
                   ProductImageId = Guid.NewGuid(),
                   ProductImagePath = "https://upload.wikimedia.org/wikipedia/commons/thumb/c/c8/Cappuccino_at_Sightglass_Coffee.jpg/1200px-Cappuccino_at_Sightglass_Coffee.jpg",
                   ProductImageDescription = "Cappuccino with milk foam",
                   ProductId = productId2
               });

            modelBuilder.Entity<Account>().HasData(
                new Account { AccountId = adminId, AccountUsername = "admin", AccountPassword = "admin", Role = Account.UserRole.Admin },
                new Account { AccountId = normalEmployeeId, AccountUsername = "cashier", AccountPassword = "1", Role = Account.UserRole.Employee }
            );

            modelBuilder.Entity<Employee>().HasData(
                new Employee { EmployeeId = bossId, EmployeeName = "John The Boss", AccountId = adminId, EmployeePosition = "Owner", EmployeeWorkingHour = 10 },
                new Employee { EmployeeId = employeeId, EmployeeName = "Jane Cashier", AccountId = normalEmployeeId, EmployeePosition = "Cashier", EmployeeWorkingHour = 10 }
            );

            modelBuilder.Entity<PayRate>().HasData(
                new PayRate { PayRateId = payRateId1, PayrateName = "Hoc viec", PayrateValue = 20000 },
                new PayRate { PayRateId = payRateId2, PayrateName = "Junior", PayrateValue = 25000 },
                new PayRate { PayRateId = payRateId3, PayrateName = "Senior", PayrateValue = 30000 }
            );

            modelBuilder.Entity<Salary>().HasData(
                new Salary { SalaryId = salaryId1, EmployeeId = employeeId, PayrateId = payRateId1, TotalSalary = 250000 }
            );

            modelBuilder.Entity<Customer>().HasData(
                new Customer { CustomerId = customerId, CustomerName = "Jane Smith", CustomerPhone = "0934516636", CustomerBirthday = DateTime.Now }
            );

            modelBuilder.Entity<Receipt>().HasData(
                new Receipt { ReceiptId = receiptId, CustomerId = customerId, EmployeeId = employeeId, ReceiptDate = DateTime.Now, ReceiptTotal = 70000 }
            );

            modelBuilder.Entity<ReceiptDetail>().HasData(
                new ReceiptDetail { ReceiptId = receiptId, ProductId = productId1, ProductQuantity = 2 },
                new ReceiptDetail { ReceiptId = receiptId, ProductId = productId4, ProductQuantity = 1 }
            );
        }
    }
}

