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
                    { new Guid("df2645f1-008c-4e07-9f28-d7222dba8811"), "Tea", false },
                    { new Guid("e0fcd8f9-65fd-44ea-b28c-9cd2b1aba532"), "Coffee", false },
                    { new Guid("fd5bfcfb-378d-4812-92a5-6954dcf54942"), "Pastry", false }
                });

            migrationBuilder.InsertData(
                table: "Customers",
                columns: new[] { "CustomerId", "CustomerBirthday", "CustomerName", "CustomerPhone", "IsDeleted" },
                values: new object[] { new Guid("8e2b5b72-0ed5-4e21-910e-99573884cd7c"), new DateTime(2024, 6, 17, 10, 24, 47, 530, DateTimeKind.Local).AddTicks(6409), "Jane Smith", "0934516636", false });

            migrationBuilder.InsertData(
                table: "PayRates",
                columns: new[] { "PayRateId", "IsDeleted", "PayrateName", "PayrateValue" },
                values: new object[,]
                {
                    { new Guid("02c9adc0-e8db-40aa-bcf3-72f19782c0cc"), false, "Hoc viec", 20000m },
                    { new Guid("17d7d325-1523-4786-90fc-f41f0c8e75a7"), false, "Senior", 30000m },
                    { new Guid("ccba49c8-4bfc-48bb-a723-1a703b13a0cb"), false, "Junior", 25000m }
                });

            migrationBuilder.InsertData(
                table: "Users",
                columns: new[] { "UserId", "FirstName", "HashPassword", "IsDeleted", "LastName", "Role", "Salt", "UserPosition", "UserWorkingHour", "Username" },
                values: new object[] { new Guid("18839781-6a5d-4297-9cf0-ac8e898630fb"), null, null, false, null, 0, null, null, null, "test" });

            migrationBuilder.InsertData(
                table: "Products",
                columns: new[] { "ProductId", "CategoryId", "IsDeleted", "ProductDescription", "ProductName", "ProductPrice" },
                values: new object[,]
                {
                    { new Guid("3e2f883b-329b-4340-bb32-7e69e51e3b87"), new Guid("e0fcd8f9-65fd-44ea-b28c-9cd2b1aba532"), false, "Coffee shot", "Espresso", 25000m },
                    { new Guid("ac12a34c-93af-40a7-a7eb-8faaa0644477"), new Guid("e0fcd8f9-65fd-44ea-b28c-9cd2b1aba532"), false, "Milky coffee", "Cappuccino", 30000m },
                    { new Guid("be6b678e-aaa0-4141-b12e-48d87317747e"), new Guid("df2645f1-008c-4e07-9f28-d7222dba8811"), false, "Green thing", "Green Tea", 15000m },
                    { new Guid("f4c1859e-7e1c-47d8-b76f-d6f044928a95"), new Guid("fd5bfcfb-378d-4812-92a5-6954dcf54942"), false, "It's pronounced \"KhoaSoong\" ", "Croissant", 20000m }
                });

            migrationBuilder.InsertData(
                table: "Receipts",
                columns: new[] { "ReceiptId", "CustomerId", "IsDeleted", "ReceiptDate", "ReceiptTotal", "Table", "UserId" },
                values: new object[] { new Guid("348103dd-e57b-44c3-bf42-dd27dec27a27"), new Guid("8e2b5b72-0ed5-4e21-910e-99573884cd7c"), false, new DateTime(2024, 6, 17, 10, 24, 47, 530, DateTimeKind.Local).AddTicks(6444), 70000m, 1, new Guid("18839781-6a5d-4297-9cf0-ac8e898630fb") });

            migrationBuilder.InsertData(
                table: "Salaries",
                columns: new[] { "SalaryId", "IsDeleted", "PayrateId", "TotalSalary", "UserId" },
                values: new object[] { new Guid("f37d9174-1bd3-47e1-bf55-3691d5ce1501"), false, new Guid("02c9adc0-e8db-40aa-bcf3-72f19782c0cc"), 250000m, new Guid("18839781-6a5d-4297-9cf0-ac8e898630fb") });

            migrationBuilder.InsertData(
                table: "ReceiptDetail",
                columns: new[] { "ProductId", "ReceiptId", "ProductPrice", "ProductQuantity" },
                values: new object[,]
                {
                    { new Guid("3e2f883b-329b-4340-bb32-7e69e51e3b87"), new Guid("348103dd-e57b-44c3-bf42-dd27dec27a27"), 0m, 2 },
                    { new Guid("f4c1859e-7e1c-47d8-b76f-d6f044928a95"), new Guid("348103dd-e57b-44c3-bf42-dd27dec27a27"), 0m, 1 }
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
