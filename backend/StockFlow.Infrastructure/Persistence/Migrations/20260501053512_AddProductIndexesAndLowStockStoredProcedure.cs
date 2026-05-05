using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace StockFlow.Infrastructure.Persistence.Migrations
{
    /// <inheritdoc />
    public partial class AddProductIndexesAndLowStockStoredProcedure : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateIndex(
                name: "IX_Products_Name",
                table: "Products",
                column: "Name");

            migrationBuilder.CreateIndex(
                name: "IX_Products_PurchaseDate",
                table: "Products",
                column: "PurchaseDate");

            migrationBuilder.CreateIndex(
                name: "IX_Products_Quantity",
                table: "Products",
                column: "Quantity");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_Products_Name",
                table: "Products");

            migrationBuilder.DropIndex(
                name: "IX_Products_PurchaseDate",
                table: "Products");

            migrationBuilder.DropIndex(
                name: "IX_Products_Quantity",
                table: "Products");
        }
    }
}
