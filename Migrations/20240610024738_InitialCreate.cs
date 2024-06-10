using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace CoffeeShop.Migrations
{
    /// <inheritdoc />
    public partial class InitialCreate : Migration
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
                    Role = table.Column<int>(type: "int", nullable: false)
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
                    CategoryName = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: false)
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
                    CustomerBirthday = table.Column<DateTime>(type: "datetime2", maxLength: 255, nullable: false)
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
                    PayrateValue = table.Column<decimal>(type: "decimal(18,2)", nullable: false)
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
                    AccountId = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
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
                    CategoryId = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
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
                    EmployeeId = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
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
                    ReceiptTotal = table.Column<decimal>(type: "decimal(18,2)", nullable: false)
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
                    TotalSalary = table.Column<decimal>(type: "decimal(18,2)", nullable: false)
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
                    ProductId = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
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
                columns: new[] { "AccountId", "AccountPassword", "AccountUsername", "Role" },
                values: new object[,]
                {
                    { new Guid("32441298-dcc0-4262-a2e8-4ed514c7d98c"), "1", "cashier", 1 },
                    { new Guid("b3ca757d-f598-48a7-accc-2ecea9d75bd9"), "admin", "admin", 0 }
                });

            migrationBuilder.InsertData(
                table: "Categories",
                columns: new[] { "CategoryId", "CategoryName" },
                values: new object[,]
                {
                    { new Guid("ba21fe7e-02bc-4cdf-8f50-23cf62ac1526"), "Pastry" },
                    { new Guid("cfccc484-b52c-4755-87b7-6674ae199084"), "Coffee" },
                    { new Guid("f6cdf913-ca5f-46d1-9970-5f5fe1d09190"), "Tea" }
                });

            migrationBuilder.InsertData(
                table: "Customers",
                columns: new[] { "CustomerId", "CustomerBirthday", "CustomerName", "CustomerPhone" },
                values: new object[] { new Guid("6182c9c6-d3f1-4cf1-876b-71ca48b1ae50"), new DateTime(2024, 6, 10, 9, 47, 35, 502, DateTimeKind.Local).AddTicks(6410), "Jane Smith", "0934516636" });

            migrationBuilder.InsertData(
                table: "PayRates",
                columns: new[] { "PayRateId", "PayrateName", "PayrateValue" },
                values: new object[,]
                {
                    { new Guid("b425cc0e-f042-4dd7-b65d-bfae7a44e80d"), "Senior", 30000m },
                    { new Guid("c5f43c9c-8c96-4476-ad9f-30b8e423c782"), "Junior", 25000m },
                    { new Guid("d9d61f5c-0766-4ea2-87f8-d796c39dd102"), "Hoc viec", 20000m }
                });

            migrationBuilder.InsertData(
                table: "Employees",
                columns: new[] { "EmployeeId", "AccountId", "EmployeeName", "EmployeePosition", "EmployeeWorkingHour" },
                values: new object[,]
                {
                    { new Guid("3a7e6fcc-3e77-45b8-811b-cd9136e0b996"), new Guid("32441298-dcc0-4262-a2e8-4ed514c7d98c"), "Jane Cashier", "Cashier", (byte)10 },
                    { new Guid("fa82020c-63cf-46b6-821c-a22e9e1e888b"), new Guid("b3ca757d-f598-48a7-accc-2ecea9d75bd9"), "John The Boss", "Owner", (byte)10 }
                });

            migrationBuilder.InsertData(
                table: "Products",
                columns: new[] { "ProductId", "CategoryId", "ProductDescription", "ProductName", "ProductPrice" },
                values: new object[,]
                {
                    { new Guid("42edc201-2ecf-454c-a18e-df3d0a48748a"), new Guid("ba21fe7e-02bc-4cdf-8f50-23cf62ac1526"), "It's pronounced \"KhoaSoong\" ", "Croissant", 20000m },
                    { new Guid("90d8ca42-c9b3-45e5-97b4-ac025c0cc57c"), new Guid("f6cdf913-ca5f-46d1-9970-5f5fe1d09190"), "Green thing", "Green Tea", 15000m },
                    { new Guid("b5f3af44-bfe3-407b-8d4b-e62ee6b87cfc"), new Guid("cfccc484-b52c-4755-87b7-6674ae199084"), "Milky coffee", "Cappuccino", 30000m },
                    { new Guid("e86ea75a-8130-4f4a-92ed-0cae63867694"), new Guid("cfccc484-b52c-4755-87b7-6674ae199084"), "Coffee shot", "Espresso", 25000m }
                });

            migrationBuilder.InsertData(
                table: "ProductImages",
                columns: new[] { "ProductImageId", "ProductId", "ProductImageDescription", "ProductImagePath" },
                values: new object[,]
                {
                    { new Guid("0b4eb839-2cb9-40d8-ae7c-af3bee8258a8"), new Guid("b5f3af44-bfe3-407b-8d4b-e62ee6b87cfc"), "Cappuccino with milk foam", "https://upload.wikimedia.org/wikipedia/commons/thumb/c/c8/Cappuccino_at_Sightglass_Coffee.jpg/1200px-Cappuccino_at_Sightglass_Coffee.jpg" },
                    { new Guid("caa6c741-ab1b-4250-b819-07054ea4d2b1"), new Guid("e86ea75a-8130-4f4a-92ed-0cae63867694"), "Espresso coffee shot", "https://cdn.tgdd.vn/Files/2023/07/11/1537842/espresso-la-gi-nguyen-tac-pha-espresso-dung-chuan-202307120715077669.jpg" }
                });

            migrationBuilder.InsertData(
                table: "Receipts",
                columns: new[] { "ReceiptId", "CustomerId", "EmployeeId", "ReceiptDate", "ReceiptTotal" },
                values: new object[] { new Guid("3830a779-bc3a-4a78-bbde-8f181e21b932"), new Guid("6182c9c6-d3f1-4cf1-876b-71ca48b1ae50"), new Guid("3a7e6fcc-3e77-45b8-811b-cd9136e0b996"), new DateTime(2024, 6, 10, 9, 47, 35, 502, DateTimeKind.Local).AddTicks(6447), 70000m });

            migrationBuilder.InsertData(
                table: "Salaries",
                columns: new[] { "SalaryId", "EmployeeId", "PayrateId", "TotalSalary" },
                values: new object[] { new Guid("0bb8de08-2005-49bf-b26d-90413f666b58"), new Guid("3a7e6fcc-3e77-45b8-811b-cd9136e0b996"), new Guid("d9d61f5c-0766-4ea2-87f8-d796c39dd102"), 250000m });

            migrationBuilder.InsertData(
                table: "ReceiptDetail",
                columns: new[] { "ProductId", "ReceiptId", "ProductPrice", "ProductQuantity" },
                values: new object[,]
                {
                    { new Guid("42edc201-2ecf-454c-a18e-df3d0a48748a"), new Guid("3830a779-bc3a-4a78-bbde-8f181e21b932"), 0m, 1 },
                    { new Guid("e86ea75a-8130-4f4a-92ed-0cae63867694"), new Guid("3830a779-bc3a-4a78-bbde-8f181e21b932"), 0m, 2 }
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
