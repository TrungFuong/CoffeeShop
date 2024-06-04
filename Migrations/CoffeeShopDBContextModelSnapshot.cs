﻿// <auto-generated />
using System;
using CoffeeShop;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace CoffeeShop.Migrations
{
    [DbContext(typeof(CoffeeShopDBContext))]
    partial class CoffeeShopDBContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "8.0.6")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder);

            modelBuilder.Entity("CoffeeShop.Models.Account", b =>
                {
                    b.Property<Guid>("AccountId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("AccountPassword")
                        .IsRequired()
                        .HasMaxLength(255)
                        .HasColumnType("nvarchar(255)");

                    b.Property<string>("AccountUsername")
                        .IsRequired()
                        .HasMaxLength(255)
                        .HasColumnType("nvarchar(255)");

                    b.Property<int>("RoleId")
                        .HasColumnType("int");

                    b.HasKey("AccountId");

                    b.HasIndex("RoleId");

                    b.ToTable("Accounts");

                    b.HasData(
                        new
                        {
                            AccountId = new Guid("b5edf14d-9c1f-455f-bab7-c0ad2b87febc"),
                            AccountPassword = "admin",
                            AccountUsername = "admin",
                            RoleId = 1
                        },
                        new
                        {
                            AccountId = new Guid("a38c4fea-a34b-45c1-bf75-133a8eee9d29"),
                            AccountPassword = "1",
                            AccountUsername = "cashier",
                            RoleId = 1
                        });
                });

            modelBuilder.Entity("CoffeeShop.Models.Category", b =>
                {
                    b.Property<int>("CategoryId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("CategoryId"));

                    b.Property<string>("CategoryName")
                        .IsRequired()
                        .HasMaxLength(255)
                        .HasColumnType("nvarchar(255)");

                    b.HasKey("CategoryId");

                    b.ToTable("Categories");

                    b.HasData(
                        new
                        {
                            CategoryId = 1,
                            CategoryName = "Coffee"
                        },
                        new
                        {
                            CategoryId = 2,
                            CategoryName = "Tea"
                        },
                        new
                        {
                            CategoryId = 3,
                            CategoryName = "Pastry"
                        });
                });

            modelBuilder.Entity("CoffeeShop.Models.CheckTime", b =>
                {
                    b.Property<int>("RecordId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("RecordId"));

                    b.Property<DateTime>("CheckinTime")
                        .HasColumnType("datetime2");

                    b.Property<DateTime>("CheckoutTime")
                        .HasColumnType("datetime2");

                    b.Property<Guid>("EmployeeId")
                        .HasColumnType("uniqueidentifier");

                    b.HasKey("RecordId");

                    b.HasIndex("EmployeeId");

                    b.ToTable("CheckTimes");
                });

            modelBuilder.Entity("CoffeeShop.Models.Customer", b =>
                {
                    b.Property<Guid>("CustomerId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<DateTime>("CustomerBirthday")
                        .HasMaxLength(255)
                        .HasColumnType("datetime2");

                    b.Property<string>("CustomerName")
                        .IsRequired()
                        .HasMaxLength(255)
                        .HasColumnType("nvarchar(255)");

                    b.Property<string>("CustomerPhone")
                        .IsRequired()
                        .HasMaxLength(255)
                        .HasColumnType("nvarchar(255)");

                    b.HasKey("CustomerId");

                    b.ToTable("Customers");

                    b.HasData(
                        new
                        {
                            CustomerId = new Guid("3b351c5f-519b-40b0-9979-a385323bd452"),
                            CustomerBirthday = new DateTime(2024, 6, 5, 3, 19, 37, 270, DateTimeKind.Local).AddTicks(6854),
                            CustomerName = "Jane Smith",
                            CustomerPhone = "0934516636"
                        });
                });

            modelBuilder.Entity("CoffeeShop.Models.Employee", b =>
                {
                    b.Property<Guid>("EmployeeId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid>("AccountId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("EmployeeName")
                        .IsRequired()
                        .HasMaxLength(255)
                        .HasColumnType("nvarchar(255)");

                    b.Property<string>("EmployeePosition")
                        .IsRequired()
                        .HasMaxLength(255)
                        .HasColumnType("nvarchar(255)");

                    b.Property<byte>("EmployeeWorkingHour")
                        .HasColumnType("tinyint");

                    b.HasKey("EmployeeId");

                    b.HasIndex("AccountId")
                        .IsUnique();

                    b.ToTable("Employees");

                    b.HasData(
                        new
                        {
                            EmployeeId = new Guid("01831e1e-e1cc-4937-a743-064311205f4d"),
                            AccountId = new Guid("b5edf14d-9c1f-455f-bab7-c0ad2b87febc"),
                            EmployeeName = "John The Boss",
                            EmployeePosition = "Owner",
                            EmployeeWorkingHour = (byte)10
                        },
                        new
                        {
                            EmployeeId = new Guid("cbc1d18e-c1ae-4c37-b3c3-9dfa794e13ae"),
                            AccountId = new Guid("a38c4fea-a34b-45c1-bf75-133a8eee9d29"),
                            EmployeeName = "Jane Cashier",
                            EmployeePosition = "Cashier",
                            EmployeeWorkingHour = (byte)10
                        });
                });

            modelBuilder.Entity("CoffeeShop.Models.PayRate", b =>
                {
                    b.Property<int>("PayRateId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("PayRateId"));

                    b.Property<string>("PayrateName")
                        .IsRequired()
                        .HasMaxLength(255)
                        .HasColumnType("nvarchar(255)");

                    b.Property<decimal>("PayrateValue")
                        .HasColumnType("decimal(18,2)");

                    b.HasKey("PayRateId");

                    b.ToTable("PayRates");

                    b.HasData(
                        new
                        {
                            PayRateId = 1,
                            PayrateName = "Hoc viec",
                            PayrateValue = 20000m
                        },
                        new
                        {
                            PayRateId = 2,
                            PayrateName = "Junior",
                            PayrateValue = 25000m
                        },
                        new
                        {
                            PayRateId = 3,
                            PayrateName = "Senior",
                            PayrateValue = 30000m
                        });
                });

            modelBuilder.Entity("CoffeeShop.Models.Product", b =>
                {
                    b.Property<int>("ProductId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("ProductId"));

                    b.Property<int>("CategoryId")
                        .HasColumnType("int");

                    b.Property<string>("ProductDescription")
                        .IsRequired()
                        .HasMaxLength(255)
                        .HasColumnType("nvarchar(255)");

                    b.Property<string>("ProductName")
                        .IsRequired()
                        .HasMaxLength(255)
                        .HasColumnType("nvarchar(255)");

                    b.Property<decimal>("ProductPrice")
                        .HasColumnType("decimal(18,2)");

                    b.HasKey("ProductId");

                    b.HasIndex("CategoryId");

                    b.ToTable("Products");

                    b.HasData(
                        new
                        {
                            ProductId = 1,
                            CategoryId = 1,
                            ProductDescription = "Coffee shot",
                            ProductName = "Espresso",
                            ProductPrice = 25000m
                        },
                        new
                        {
                            ProductId = 2,
                            CategoryId = 1,
                            ProductDescription = "Milky coffee",
                            ProductName = "Cappuccino",
                            ProductPrice = 30000m
                        },
                        new
                        {
                            ProductId = 3,
                            CategoryId = 2,
                            ProductDescription = "Green thing",
                            ProductName = "Green Tea",
                            ProductPrice = 15000m
                        },
                        new
                        {
                            ProductId = 4,
                            CategoryId = 3,
                            ProductDescription = "It's pronounced \"KhoaSoong\" ",
                            ProductName = "Croissant",
                            ProductPrice = 20000m
                        });
                });

            modelBuilder.Entity("CoffeeShop.Models.ProductImage", b =>
                {
                    b.Property<int>("ProductImageId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("ProductImageId"));

                    b.Property<int>("ProductId")
                        .HasColumnType("int");

                    b.Property<string>("ProductImageDescription")
                        .IsRequired()
                        .HasMaxLength(255)
                        .HasColumnType("nvarchar(255)");

                    b.Property<string>("ProductImagePath")
                        .IsRequired()
                        .HasMaxLength(255)
                        .HasColumnType("nvarchar(255)");

                    b.HasKey("ProductImageId");

                    b.HasIndex("ProductId");

                    b.ToTable("ProductImages");

                    b.HasData(
                        new
                        {
                            ProductImageId = 1,
                            ProductId = 1,
                            ProductImageDescription = "Espresso coffee shot",
                            ProductImagePath = "https://cdn.tgdd.vn/Files/2023/07/11/1537842/espresso-la-gi-nguyen-tac-pha-espresso-dung-chuan-202307120715077669.jpg"
                        },
                        new
                        {
                            ProductImageId = 2,
                            ProductId = 2,
                            ProductImageDescription = "Cappuccino with milk foam",
                            ProductImagePath = "https://upload.wikimedia.org/wikipedia/commons/thumb/c/c8/Cappuccino_at_Sightglass_Coffee.jpg/1200px-Cappuccino_at_Sightglass_Coffee.jpg"
                        });
                });

            modelBuilder.Entity("CoffeeShop.Models.Receipt", b =>
                {
                    b.Property<Guid>("ReceiptId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid>("CustomerId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid>("EmployeeId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<DateTime>("ReceiptDate")
                        .HasColumnType("datetime2");

                    b.Property<decimal>("ReceiptTotal")
                        .HasColumnType("decimal(18,2)");

                    b.HasKey("ReceiptId");

                    b.HasIndex("CustomerId")
                        .IsUnique();

                    b.HasIndex("EmployeeId");

                    b.ToTable("Receipts");

                    b.HasData(
                        new
                        {
                            ReceiptId = new Guid("a8392033-e36a-4c64-97e7-047bbd3bd8f0"),
                            CustomerId = new Guid("3b351c5f-519b-40b0-9979-a385323bd452"),
                            EmployeeId = new Guid("cbc1d18e-c1ae-4c37-b3c3-9dfa794e13ae"),
                            ReceiptDate = new DateTime(2024, 6, 5, 3, 19, 37, 270, DateTimeKind.Local).AddTicks(6890),
                            ReceiptTotal = 70000m
                        });
                });

            modelBuilder.Entity("CoffeeShop.Models.ReceiptDetail", b =>
                {
                    b.Property<Guid>("ReceiptId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<int>("ProductId")
                        .HasColumnType("int");

                    b.Property<decimal>("ProductPrice")
                        .HasColumnType("decimal(18,2)");

                    b.Property<int>("ProductQuantity")
                        .HasColumnType("int");

                    b.HasKey("ReceiptId", "ProductId");

                    b.HasIndex("ProductId");

                    b.ToTable("ReceiptDetail");

                    b.HasData(
                        new
                        {
                            ReceiptId = new Guid("a8392033-e36a-4c64-97e7-047bbd3bd8f0"),
                            ProductId = 1,
                            ProductPrice = 0m,
                            ProductQuantity = 2
                        },
                        new
                        {
                            ReceiptId = new Guid("a8392033-e36a-4c64-97e7-047bbd3bd8f0"),
                            ProductId = 4,
                            ProductPrice = 0m,
                            ProductQuantity = 1
                        });
                });

            modelBuilder.Entity("CoffeeShop.Models.Role", b =>
                {
                    b.Property<int>("RoleId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("RoleId"));

                    b.Property<string>("RoleName")
                        .IsRequired()
                        .HasMaxLength(255)
                        .HasColumnType("nvarchar(255)");

                    b.HasKey("RoleId");

                    b.ToTable("Role");

                    b.HasData(
                        new
                        {
                            RoleId = 1,
                            RoleName = "Admin"
                        },
                        new
                        {
                            RoleId = 2,
                            RoleName = "Employee"
                        });
                });

            modelBuilder.Entity("CoffeeShop.Models.Salary", b =>
                {
                    b.Property<int>("SalaryId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("SalaryId"));

                    b.Property<Guid>("EmployeeId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<int>("PayrateId")
                        .HasColumnType("int");

                    b.Property<decimal>("TotalSalary")
                        .HasColumnType("decimal(18,2)");

                    b.HasKey("SalaryId");

                    b.HasIndex("EmployeeId")
                        .IsUnique();

                    b.HasIndex("PayrateId");

                    b.ToTable("Salaries");

                    b.HasData(
                        new
                        {
                            SalaryId = 1,
                            EmployeeId = new Guid("cbc1d18e-c1ae-4c37-b3c3-9dfa794e13ae"),
                            PayrateId = 1,
                            TotalSalary = 250000m
                        });
                });

            modelBuilder.Entity("CoffeeShop.Models.Account", b =>
                {
                    b.HasOne("CoffeeShop.Models.Role", "Role")
                        .WithMany("Accounts")
                        .HasForeignKey("RoleId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Role");
                });

            modelBuilder.Entity("CoffeeShop.Models.CheckTime", b =>
                {
                    b.HasOne("CoffeeShop.Models.Employee", "Employee")
                        .WithMany("CheckTimes")
                        .HasForeignKey("EmployeeId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Employee");
                });

            modelBuilder.Entity("CoffeeShop.Models.Employee", b =>
                {
                    b.HasOne("CoffeeShop.Models.Account", "Account")
                        .WithOne("Employee")
                        .HasForeignKey("CoffeeShop.Models.Employee", "AccountId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Account");
                });

            modelBuilder.Entity("CoffeeShop.Models.Product", b =>
                {
                    b.HasOne("CoffeeShop.Models.Category", "Category")
                        .WithMany("Products")
                        .HasForeignKey("CategoryId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Category");
                });

            modelBuilder.Entity("CoffeeShop.Models.ProductImage", b =>
                {
                    b.HasOne("CoffeeShop.Models.Product", "Product")
                        .WithMany("ProductImages")
                        .HasForeignKey("ProductId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Product");
                });

            modelBuilder.Entity("CoffeeShop.Models.Receipt", b =>
                {
                    b.HasOne("CoffeeShop.Models.Customer", "Customer")
                        .WithOne("Receipt")
                        .HasForeignKey("CoffeeShop.Models.Receipt", "CustomerId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("CoffeeShop.Models.Employee", "Employee")
                        .WithMany("Receipts")
                        .HasForeignKey("EmployeeId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Customer");

                    b.Navigation("Employee");
                });

            modelBuilder.Entity("CoffeeShop.Models.ReceiptDetail", b =>
                {
                    b.HasOne("CoffeeShop.Models.Product", "Product")
                        .WithMany("ReceiptDetails")
                        .HasForeignKey("ProductId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("CoffeeShop.Models.Receipt", "Receipt")
                        .WithMany("ReceiptDetails")
                        .HasForeignKey("ReceiptId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Product");

                    b.Navigation("Receipt");
                });

            modelBuilder.Entity("CoffeeShop.Models.Salary", b =>
                {
                    b.HasOne("CoffeeShop.Models.Employee", "Employee")
                        .WithOne("Salary")
                        .HasForeignKey("CoffeeShop.Models.Salary", "EmployeeId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("CoffeeShop.Models.PayRate", "PayRate")
                        .WithMany("Salaries")
                        .HasForeignKey("PayrateId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Employee");

                    b.Navigation("PayRate");
                });

            modelBuilder.Entity("CoffeeShop.Models.Account", b =>
                {
                    b.Navigation("Employee")
                        .IsRequired();
                });

            modelBuilder.Entity("CoffeeShop.Models.Category", b =>
                {
                    b.Navigation("Products");
                });

            modelBuilder.Entity("CoffeeShop.Models.Customer", b =>
                {
                    b.Navigation("Receipt")
                        .IsRequired();
                });

            modelBuilder.Entity("CoffeeShop.Models.Employee", b =>
                {
                    b.Navigation("CheckTimes");

                    b.Navigation("Receipts");

                    b.Navigation("Salary");
                });

            modelBuilder.Entity("CoffeeShop.Models.PayRate", b =>
                {
                    b.Navigation("Salaries");
                });

            modelBuilder.Entity("CoffeeShop.Models.Product", b =>
                {
                    b.Navigation("ProductImages");

                    b.Navigation("ReceiptDetails");
                });

            modelBuilder.Entity("CoffeeShop.Models.Receipt", b =>
                {
                    b.Navigation("ReceiptDetails");
                });

            modelBuilder.Entity("CoffeeShop.Models.Role", b =>
                {
                    b.Navigation("Accounts");
                });
#pragma warning restore 612, 618
        }
    }
}
