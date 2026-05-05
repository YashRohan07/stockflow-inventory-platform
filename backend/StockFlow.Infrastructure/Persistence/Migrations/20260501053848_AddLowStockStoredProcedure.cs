using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace StockFlow.Infrastructure.Persistence.Migrations
{
    public partial class AddLowStockStoredProcedure : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql("""
CREATE OR ALTER PROCEDURE dbo.GetLowStockProducts
    @Threshold INT
AS
BEGIN
    SET NOCOUNT ON;

    SELECT 
        Id,
        SKU,
        Name,
        Size,
        Color,
        Quantity,
        PurchasePrice,
        PurchaseDate
    FROM Products
    WHERE Quantity < @Threshold
    ORDER BY Quantity ASC;
END
""");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql("DROP PROCEDURE IF EXISTS dbo.GetLowStockProducts;");
        }
    }
}