using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SalesApp.Migrations
{
    /// <inheritdoc />
    public partial class sale_model : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_SaleProducts_Products_productId",
                table: "SaleProducts");

            migrationBuilder.RenameColumn(
                name: "productId",
                table: "SaleProducts",
                newName: "product_id");

            migrationBuilder.RenameIndex(
                name: "IX_SaleProducts_productId",
                table: "SaleProducts",
                newName: "IX_SaleProducts_product_id");

            migrationBuilder.AddForeignKey(
                name: "FK_SaleProducts_Products_product_id",
                table: "SaleProducts",
                column: "product_id",
                principalTable: "Products",
                principalColumn: "ProductId",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_SaleProducts_Products_product_id",
                table: "SaleProducts");

            migrationBuilder.RenameColumn(
                name: "product_id",
                table: "SaleProducts",
                newName: "productId");

            migrationBuilder.RenameIndex(
                name: "IX_SaleProducts_product_id",
                table: "SaleProducts",
                newName: "IX_SaleProducts_productId");

            migrationBuilder.AddForeignKey(
                name: "FK_SaleProducts_Products_productId",
                table: "SaleProducts",
                column: "productId",
                principalTable: "Products",
                principalColumn: "ProductId",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
