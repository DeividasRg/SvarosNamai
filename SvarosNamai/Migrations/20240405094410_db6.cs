using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SvarosNamai.Service.ProductAPI.Migrations
{
    /// <inheritdoc />
    public partial class db6 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ProductBundle_Bundles_BundleId",
                table: "ProductBundle");

            migrationBuilder.DropForeignKey(
                name: "FK_ProductBundle_Products_ProductId",
                table: "ProductBundle");

            migrationBuilder.DropIndex(
                name: "IX_ProductBundle_BundleId",
                table: "ProductBundle");

            migrationBuilder.DropIndex(
                name: "IX_ProductBundle_ProductId",
                table: "ProductBundle");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateIndex(
                name: "IX_ProductBundle_BundleId",
                table: "ProductBundle",
                column: "BundleId");

            migrationBuilder.CreateIndex(
                name: "IX_ProductBundle_ProductId",
                table: "ProductBundle",
                column: "ProductId");

            migrationBuilder.AddForeignKey(
                name: "FK_ProductBundle_Bundles_BundleId",
                table: "ProductBundle",
                column: "BundleId",
                principalTable: "Bundles",
                principalColumn: "BundleId",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_ProductBundle_Products_ProductId",
                table: "ProductBundle",
                column: "ProductId",
                principalTable: "Products",
                principalColumn: "ProductId",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
