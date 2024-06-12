using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace CoffeeShop.Migrations
{
    /// <inheritdoc />
    public partial class IniCreate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Accounts",
                columns: table => new
                {
                    AccountId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    AccountUsername = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: false),
                    AccountPassword = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: false),
                    Role = table.Column<int>(type: "int", nullable: false),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Accounts", x => x.AccountId);
                });

            migrationBuilder.CreateTable(
                name: "Categories",
                columns: table => new
                {
                    CategoryId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    CategoryName = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: false),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Categories", x => x.CategoryId);
                });

            migrationBuilder.CreateTable(
                name: "Customers",
                columns: table => new
                {
                    CustomerId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    CustomerName = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: false),
                    CustomerPhone = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: false),
                    CustomerBirthday = table.Column<DateTime>(type: "datetime2", maxLength: 255, nullable: false),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Customers", x => x.CustomerId);
                });

            migrationBuilder.CreateTable(
                name: "PayRates",
                columns: table => new
                {
                    PayRateId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    PayrateName = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: false),
                    PayrateValue = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PayRates", x => x.PayRateId);
                });

            migrationBuilder.CreateTable(
                name: "Employees",
                columns: table => new
                {
                    EmployeeId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    EmployeeName = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: false),
                    EmployeePosition = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: false),
                    EmployeeWorkingHour = table.Column<byte>(type: "tinyint", nullable: false),
                    AccountId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Employees", x => x.EmployeeId);
                    table.ForeignKey(
                        name: "FK_Employees_Accounts_AccountId",
                        column: x => x.AccountId,
                        principalTable: "Accounts",
                        principalColumn: "AccountId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Products",
                columns: table => new
                {
                    ProductId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    ProductName = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: false),
                    ProductPrice = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    ProductDescription = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: false),
                    CategoryId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Products", x => x.ProductId);
                    table.ForeignKey(
                        name: "FK_Products_Categories_CategoryId",
                        column: x => x.CategoryId,
                        principalTable: "Categories",
                        principalColumn: "CategoryId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "CheckTimes",
                columns: table => new
                {
                    RecordId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    CheckinTime = table.Column<DateTime>(type: "datetime2", nullable: false),
                    CheckoutTime = table.Column<DateTime>(type: "datetime2", nullable: false),
                    EmployeeId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CheckTimes", x => x.RecordId);
                    table.ForeignKey(
                        name: "FK_CheckTimes_Employees_EmployeeId",
                        column: x => x.EmployeeId,
                        principalTable: "Employees",
                        principalColumn: "EmployeeId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Receipts",
                columns: table => new
                {
                    ReceiptId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    EmployeeId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    CustomerId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    ReceiptDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    ReceiptTotal = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    Table = table.Column<int>(type: "int", nullable: false),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Receipts", x => x.ReceiptId);
                    table.ForeignKey(
                        name: "FK_Receipts_Customers_CustomerId",
                        column: x => x.CustomerId,
                        principalTable: "Customers",
                        principalColumn: "CustomerId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Receipts_Employees_EmployeeId",
                        column: x => x.EmployeeId,
                        principalTable: "Employees",
                        principalColumn: "EmployeeId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Salaries",
                columns: table => new
                {
                    SalaryId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    EmployeeId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    PayrateId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    TotalSalary = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Salaries", x => x.SalaryId);
                    table.ForeignKey(
                        name: "FK_Salaries_Employees_EmployeeId",
                        column: x => x.EmployeeId,
                        principalTable: "Employees",
                        principalColumn: "EmployeeId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Salaries_PayRates_PayrateId",
                        column: x => x.PayrateId,
                        principalTable: "PayRates",
                        principalColumn: "PayRateId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "ProductImages",
                columns: table => new
                {
                    ProductImageId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    ProductImagePath = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: false),
                    ProductImageDescription = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: false),
                    ProductId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ProductImages", x => x.ProductImageId);
                    table.ForeignKey(
                        name: "FK_ProductImages_Products_ProductId",
                        column: x => x.ProductId,
                        principalTable: "Products",
                        principalColumn: "ProductId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "ReceiptDetail",
                columns: table => new
                {
                    ProductId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    ReceiptId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    ProductQuantity = table.Column<int>(type: "int", nullable: false),
                    ProductPrice = table.Column<decimal>(type: "decimal(18,2)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ReceiptDetail", x => new { x.ReceiptId, x.ProductId });
                    table.ForeignKey(
                        name: "FK_ReceiptDetail_Products_ProductId",
                        column: x => x.ProductId,
                        principalTable: "Products",
                        principalColumn: "ProductId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ReceiptDetail_Receipts_ReceiptId",
                        column: x => x.ReceiptId,
                        principalTable: "Receipts",
                        principalColumn: "ReceiptId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "Accounts",
                columns: new[] { "AccountId", "AccountPassword", "AccountUsername", "IsDeleted", "Role" },
                values: new object[,]
                {
                    { new Guid("78dd2fac-9cb6-424f-8a92-a4f8857f527e"), "admin", "admin", false, 0 },
                    { new Guid("973edf72-0a06-49c5-b630-d005583ba1ad"), "1", "cashier", false, 1 }
                });

            migrationBuilder.InsertData(
                table: "Categories",
                columns: new[] { "CategoryId", "CategoryName", "IsDeleted" },
                values: new object[,]
                {
                    { new Guid("4364fa40-5733-43a9-917a-3253d18b7e4b"), "Pastry", false },
                    { new Guid("537503bf-adca-44e1-9a32-99bf05d2b2d9"), "Tea", false },
                    { new Guid("d10594b0-1c6a-4a4f-bd62-aa670af7c7b2"), "Coffee", false }
                });

            migrationBuilder.InsertData(
                table: "Customers",
                columns: new[] { "CustomerId", "CustomerBirthday", "CustomerName", "CustomerPhone", "IsDeleted" },
                values: new object[] { new Guid("972ab33a-8295-4e32-b351-715359f7283c"), new DateTime(2024, 6, 11, 22, 49, 2, 53, DateTimeKind.Local).AddTicks(9399), "Jane Smith", "0934516636", false });

            migrationBuilder.InsertData(
                table: "PayRates",
                columns: new[] { "PayRateId", "IsDeleted", "PayrateName", "PayrateValue" },
                values: new object[,]
                {
                    { new Guid("30b8d7b5-7dde-45a4-9060-7b064de7bddf"), false, "Hoc viec", 20000m },
                    { new Guid("bdbd4de4-f13d-4e45-8fb7-7a88822e853f"), false, "Junior", 25000m },
                    { new Guid("dc5135a8-9490-41bc-9aa0-8752c4986fa4"), false, "Senior", 30000m }
                });

            migrationBuilder.InsertData(
                table: "Employees",
                columns: new[] { "EmployeeId", "AccountId", "EmployeeName", "EmployeePosition", "EmployeeWorkingHour", "IsDeleted" },
                values: new object[,]
                {
                    { new Guid("042e72a4-b4b2-4b98-8297-557c7d5d0921"), new Guid("78dd2fac-9cb6-424f-8a92-a4f8857f527e"), "John The Boss", "Owner", (byte)10, false },
                    { new Guid("8a5f677e-d77d-4c1e-b044-44ca391a1979"), new Guid("973edf72-0a06-49c5-b630-d005583ba1ad"), "Jane Cashier", "Cashier", (byte)10, false }
                });

            migrationBuilder.InsertData(
                table: "Products",
                columns: new[] { "ProductId", "CategoryId", "IsDeleted", "ProductDescription", "ProductName", "ProductPrice" },
                values: new object[,]
                {
                    { new Guid("275f9714-236e-457e-a5c5-39034a510d04"), new Guid("d10594b0-1c6a-4a4f-bd62-aa670af7c7b2"), false, "Milky coffee", "Cappuccino", 30000m },
                    { new Guid("6f8b6c6e-7d78-4ab3-950d-a4f6ed87f35e"), new Guid("4364fa40-5733-43a9-917a-3253d18b7e4b"), false, "It's pronounced \"KhoaSoong\" ", "Croissant", 20000m },
                    { new Guid("8dd7a8c4-1c22-4b23-b0ca-a9cf1864f192"), new Guid("537503bf-adca-44e1-9a32-99bf05d2b2d9"), false, "Green thing", "Green Tea", 15000m },
                    { new Guid("c9b3e98b-5612-4f26-a6b5-bd2d893e46e3"), new Guid("d10594b0-1c6a-4a4f-bd62-aa670af7c7b2"), false, "Coffee shot", "Espresso", 25000m }
                });

            migrationBuilder.InsertData(
                table: "ProductImages",
                columns: new[] { "ProductImageId", "IsDeleted", "ProductId", "ProductImageDescription", "ProductImagePath" },
                values: new object[,]
                {
                    { new Guid("4c9b4a27-577d-46ad-95d4-3c8f1a2e4b48"), false, new Guid("275f9714-236e-457e-a5c5-39034a510d04"), "Cappuccino with milk foam", "https://upload.wikimedia.org/wikipedia/commons/thumb/c/c8/Cappuccino_at_Sightglass_Coffee.jpg/1200px-Cappuccino_at_Sightglass_Coffee.jpg" },
                    { new Guid("8e395b9f-bc9d-4956-95ef-8093a7fd797a"), false, new Guid("c9b3e98b-5612-4f26-a6b5-bd2d893e46e3"), "Espresso coffee shot", "https://cdn.tgdd.vn/Files/2023/07/11/1537842/espresso-la-gi-nguyen-tac-pha-espresso-dung-chuan-202307120715077669.jpg" }
                });

            migrationBuilder.InsertData(
                table: "Receipts",
                columns: new[] { "ReceiptId", "CustomerId", "EmployeeId", "IsDeleted", "ReceiptDate", "ReceiptTotal", "Table" },
                values: new object[] { new Guid("5c51c26e-da45-4fa1-8298-ef58ce4c1ba8"), new Guid("972ab33a-8295-4e32-b351-715359f7283c"), new Guid("8a5f677e-d77d-4c1e-b044-44ca391a1979"), false, new DateTime(2024, 6, 11, 22, 49, 2, 53, DateTimeKind.Local).AddTicks(9436), 70000m, 1 });

            migrationBuilder.InsertData(
                table: "Salaries",
                columns: new[] { "SalaryId", "EmployeeId", "IsDeleted", "PayrateId", "TotalSalary" },
                values: new object[] { new Guid("cf44dcd4-1901-4e86-95b0-19a17686c21a"), new Guid("8a5f677e-d77d-4c1e-b044-44ca391a1979"), false, new Guid("30b8d7b5-7dde-45a4-9060-7b064de7bddf"), 250000m });

            migrationBuilder.InsertData(
                table: "ReceiptDetail",
                columns: new[] { "ProductId", "ReceiptId", "ProductPrice", "ProductQuantity" },
                values: new object[,]
                {
                    { new Guid("6f8b6c6e-7d78-4ab3-950d-a4f6ed87f35e"), new Guid("5c51c26e-da45-4fa1-8298-ef58ce4c1ba8"), 0m, 1 },
                    { new Guid("c9b3e98b-5612-4f26-a6b5-bd2d893e46e3"), new Guid("5c51c26e-da45-4fa1-8298-ef58ce4c1ba8"), 0m, 2 }
                });

            migrationBuilder.CreateIndex(
                name: "IX_CheckTimes_EmployeeId",
                table: "CheckTimes",
                column: "EmployeeId");

            migrationBuilder.CreateIndex(
                name: "IX_Employees_AccountId",
                table: "Employees",
                column: "AccountId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_ProductImages_ProductId",
                table: "ProductImages",
                column: "ProductId");

            migrationBuilder.CreateIndex(
                name: "IX_Products_CategoryId",
                table: "Products",
                column: "CategoryId");

            migrationBuilder.CreateIndex(
                name: "IX_ReceiptDetail_ProductId",
                table: "ReceiptDetail",
                column: "ProductId");

            migrationBuilder.CreateIndex(
                name: "IX_Receipts_CustomerId",
                table: "Receipts",
                column: "CustomerId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Receipts_EmployeeId",
                table: "Receipts",
                column: "EmployeeId");

            migrationBuilder.CreateIndex(
                name: "IX_Salaries_EmployeeId",
                table: "Salaries",
                column: "EmployeeId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Salaries_PayrateId",
                table: "Salaries",
                column: "PayrateId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "CheckTimes");

            migrationBuilder.DropTable(
                name: "ProductImages");

            migrationBuilder.DropTable(
                name: "ReceiptDetail");

            migrationBuilder.DropTable(
                name: "Salaries");

            migrationBuilder.DropTable(
                name: "Products");

            migrationBuilder.DropTable(
                name: "Receipts");

            migrationBuilder.DropTable(
                name: "PayRates");

            migrationBuilder.DropTable(
                name: "Categories");

            migrationBuilder.DropTable(
                name: "Customers");

            migrationBuilder.DropTable(
                name: "Employees");

            migrationBuilder.DropTable(
                name: "Accounts");
        }
    }
}
