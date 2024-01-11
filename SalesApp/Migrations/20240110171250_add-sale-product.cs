using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SalesApp.Migrations
{
    /// <inheritdoc />
    public partial class addsaleproduct : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Qty",
                table: "Products");

            migrationBuilder.CreateTable(
                name: "SaleProducts",
                columns: table => new
                {
                    saleProductId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    productId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    dateSale = table.Column<DateTime>(type: "datetime2", nullable: false),
                    qty = table.Column<int>(type: "int", nullable: false),
                    totalPrice = table.Column<float>(type: "real", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SaleProducts", x => x.saleProductId);
                    table.ForeignKey(
                        name: "FK_SaleProducts_Products_productId",
                        column: x => x.productId,
                        principalTable: "Products",
                        principalColumn: "ProductId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_SaleProducts_productId",
                table: "SaleProducts",
                column: "productId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "SaleProducts");

            migrationBuilder.AddColumn<int>(
                name: "Qty",
                table: "Products",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }
    }
}
