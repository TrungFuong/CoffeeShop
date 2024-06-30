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
                name: "Users",
                columns: table => new
                {
                    UserId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Username = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: false),
                    FirstName = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: true),
                    LastName = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: true),
                    HashPassword = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: true),
                    Salt = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: true),
                    UserPosition = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: true),
                    UserWorkingHour = table.Column<byte>(type: "tinyint", nullable: true),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false),
                    Role = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Users", x => x.UserId);
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
                    ImageUrl = table.Column<string>(type: "nvarchar(max)", nullable: true),
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
                    UserId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CheckTimes", x => x.RecordId);
                    table.ForeignKey(
                        name: "FK_CheckTimes_Users_UserId",
                        column: x => x.UserId,
                        principalTable: "Users",
                        principalColumn: "UserId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Receipts",
                columns: table => new
                {
                    ReceiptId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    UserId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    CustomerId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
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
                        principalColumn: "CustomerId");
                    table.ForeignKey(
                        name: "FK_Receipts_Users_UserId",
                        column: x => x.UserId,
                        principalTable: "Users",
                        principalColumn: "UserId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Salaries",
                columns: table => new
                {
                    SalaryId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    UserId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    PayrateId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    TotalSalary = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Salaries", x => x.SalaryId);
                    table.ForeignKey(
                        name: "FK_Salaries_PayRates_PayrateId",
                        column: x => x.PayrateId,
                        principalTable: "PayRates",
                        principalColumn: "PayRateId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Salaries_Users_UserId",
                        column: x => x.UserId,
                        principalTable: "Users",
                        principalColumn: "UserId",
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
                table: "Categories",
                columns: new[] { "CategoryId", "CategoryName", "IsDeleted" },
                values: new object[,]
                {
                    { new Guid("1ab2fb68-0f7e-40a4-959a-4fc031ba78dc"), "Pastry", false },
                    { new Guid("48252c2b-85c1-41fc-a53a-0823d05e4d9a"), "Tea", false },
                    { new Guid("b34b4d69-a99e-446c-a023-f2395e4b5084"), "Coffee", false }
                });

            migrationBuilder.InsertData(
                table: "Customers",
                columns: new[] { "CustomerId", "CustomerBirthday", "CustomerName", "CustomerPhone", "IsDeleted" },
                values: new object[] { new Guid("0ec3030b-556c-462b-80b4-6ceaf841ae6f"), new DateTime(2024, 6, 26, 2, 42, 50, 585, DateTimeKind.Local).AddTicks(4139), "Jane Smith", "0934516636", false });

            migrationBuilder.InsertData(
                table: "PayRates",
                columns: new[] { "PayRateId", "IsDeleted", "PayrateName", "PayrateValue" },
                values: new object[,]
                {
                    { new Guid("1f37aa86-4f0a-4381-a8e2-b132188c1cb5"), false, "Hoc viec", 20000m },
                    { new Guid("bd75ea48-cd13-45c4-9122-ff6a0a35a40b"), false, "Junior", 25000m },
                    { new Guid("c8d0624d-d234-4a15-b895-5123949d6258"), false, "Senior", 30000m }
                });

            migrationBuilder.InsertData(
                table: "Users",
                columns: new[] { "UserId", "FirstName", "HashPassword", "IsDeleted", "LastName", "Role", "Salt", "UserPosition", "UserWorkingHour", "Username" },
                values: new object[] { new Guid("6cdad382-f4f9-44d7-9d85-87edad1f6ef0"), null, null, false, null, 0, null, null, null, "test" });

            migrationBuilder.InsertData(
                table: "Products",
                columns: new[] { "ProductId", "CategoryId", "ImageUrl", "IsDeleted", "ProductDescription", "ProductName", "ProductPrice" },
                values: new object[,]
                {
                    { new Guid("0611210e-8678-41a1-ab10-71599db5e6f7"), new Guid("48252c2b-85c1-41fc-a53a-0823d05e4d9a"), "", false, "Green thing", "Green Tea", 15000m },
                    { new Guid("56da2756-a0a7-40eb-bb64-95551324a52c"), new Guid("1ab2fb68-0f7e-40a4-959a-4fc031ba78dc"), "", false, "It's pronounced \"KhoaSoong\" ", "Croissant", 20000m },
                    { new Guid("5c2fb199-bb90-4d12-a71c-6d7624b63413"), new Guid("b34b4d69-a99e-446c-a023-f2395e4b5084"), "", false, "Milky coffee", "Cappuccino", 30000m },
                    { new Guid("ba4bbea4-e343-4dd2-8df3-530355f6cf20"), new Guid("b34b4d69-a99e-446c-a023-f2395e4b5084"), "", false, "Coffee shot", "Espresso", 25000m }
                });

            migrationBuilder.InsertData(
                table: "Receipts",
                columns: new[] { "ReceiptId", "CustomerId", "IsDeleted", "ReceiptDate", "ReceiptTotal", "Table", "UserId" },
                values: new object[] { new Guid("0f45d45a-9d42-4fc7-8db0-ea0cfd1cd1f7"), new Guid("0ec3030b-556c-462b-80b4-6ceaf841ae6f"), false, new DateTime(2024, 6, 26, 2, 42, 50, 585, DateTimeKind.Local).AddTicks(4168), 70000m, 1, new Guid("6cdad382-f4f9-44d7-9d85-87edad1f6ef0") });

            migrationBuilder.InsertData(
                table: "Salaries",
                columns: new[] { "SalaryId", "IsDeleted", "PayrateId", "TotalSalary", "UserId" },
                values: new object[] { new Guid("774a58bc-191e-442f-adaa-0355f7901197"), false, new Guid("1f37aa86-4f0a-4381-a8e2-b132188c1cb5"), 250000m, new Guid("6cdad382-f4f9-44d7-9d85-87edad1f6ef0") });

            migrationBuilder.InsertData(
                table: "ReceiptDetail",
                columns: new[] { "ProductId", "ReceiptId", "ProductPrice", "ProductQuantity" },
                values: new object[,]
                {
                    { new Guid("56da2756-a0a7-40eb-bb64-95551324a52c"), new Guid("0f45d45a-9d42-4fc7-8db0-ea0cfd1cd1f7"), 0m, 1 },
                    { new Guid("ba4bbea4-e343-4dd2-8df3-530355f6cf20"), new Guid("0f45d45a-9d42-4fc7-8db0-ea0cfd1cd1f7"), 0m, 2 }
                });

            migrationBuilder.CreateIndex(
                name: "IX_CheckTimes_UserId",
                table: "CheckTimes",
                column: "UserId");

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
                unique: true,
                filter: "[CustomerId] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_Receipts_UserId",
                table: "Receipts",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_Salaries_PayrateId",
                table: "Salaries",
                column: "PayrateId");

            migrationBuilder.CreateIndex(
                name: "IX_Salaries_UserId",
                table: "Salaries",
                column: "UserId",
                unique: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "CheckTimes");

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
                name: "Users");
        }
    }
}
