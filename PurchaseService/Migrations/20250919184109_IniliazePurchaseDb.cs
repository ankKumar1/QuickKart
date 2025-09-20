using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace PurchaseService.Migrations
{
    /// <inheritdoc />
    public partial class IniliazePurchaseDb : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Purchases",
                columns: table => new
                {
                    PurchaseId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    EmailId = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ProductId = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    QuantityPurchased = table.Column<int>(type: "int", nullable: false),
                    TotalPrice = table.Column<decimal>(type: "decimal(18,2)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Purchases", x => x.PurchaseId);
                });

            migrationBuilder.InsertData(
                table: "Purchases",
                columns: new[] { "PurchaseId", "EmailId", "ProductId", "QuantityPurchased", "TotalPrice" },
                values: new object[,]
                {
                    { 1, "Franken@gmail.com", "P101", 2, 9000m },
                    { 2, "Franken@gmail.com", "P143", 1, 1114.52m },
                    { 3, "SamRock@gmail.com", "P120", 4, 2085m },
                    { 4, "PaulGrey@gmail.com", "P148", 1, 2561.51m },
                    { 5, "PaulGrey@gmail.com", "P101", 10, 1265m }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Purchases");
        }
    }
}
