using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SvarosNamai.Service.ProductAPI.Migrations
{
    /// <inheritdoc />
    public partial class addProductBundle2 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Products_ProductBundle_ProductBundleId",
                table: "Products");

            migrationBuilder.DropIndex(
                name: "IX_Products_ProductBundleId",
                table: "Products");

            migrationBuilder.DropColumn(
                name: "ProductBundleId",
                table: "Products");

            migrationBuilder.AddColumn<int>(
                name: "ProductId",
                table: "ProductBundle",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_ProductBundle_ProductId",
                table: "ProductBundle",
                column: "ProductId");

            migrationBuilder.AddForeignKey(
                name: "FK_ProductBundle_Products_ProductId",
                table: "ProductBundle",
                column: "ProductId",
                principalTable: "Products",
                principalColumn: "ProductId",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ProductBundle_Products_ProductId",
                table: "ProductBundle");

            migrationBuilder.DropIndex(
                name: "IX_ProductBundle_ProductId",
                table: "ProductBundle");

            migrationBuilder.DropColumn(
                name: "ProductId",
                table: "ProductBundle");

            migrationBuilder.AddColumn<int>(
                name: "ProductBundleId",
                table: "Products",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Products_ProductBundleId",
                table: "Products",
                column: "ProductBundleId");

            migrationBuilder.AddForeignKey(
                name: "FK_Products_ProductBundle_ProductBundleId",
                table: "Products",
                column: "ProductBundleId",
                principalTable: "ProductBundle",
                principalColumn: "ProductBundleId");
        }
    }
}
