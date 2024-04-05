using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SvarosNamai.Service.ProductAPI.Migrations
{
    /// <inheritdoc />
    public partial class addProductBundle : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "ProductBundleId",
                table: "Products",
                type: "int",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "ProductBundle",
                columns: table => new
                {
                    ProductBundleId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    BundleId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ProductBundle", x => x.ProductBundleId);
                    table.ForeignKey(
                        name: "FK_ProductBundle_Bundles_BundleId",
                        column: x => x.BundleId,
                        principalTable: "Bundles",
                        principalColumn: "BundleId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Products_ProductBundleId",
                table: "Products",
                column: "ProductBundleId");

            migrationBuilder.CreateIndex(
                name: "IX_ProductBundle_BundleId",
                table: "ProductBundle",
                column: "BundleId");

            migrationBuilder.AddForeignKey(
                name: "FK_Products_ProductBundle_ProductBundleId",
                table: "Products",
                column: "ProductBundleId",
                principalTable: "ProductBundle",
                principalColumn: "ProductBundleId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Products_ProductBundle_ProductBundleId",
                table: "Products");

            migrationBuilder.DropTable(
                name: "ProductBundle");

            migrationBuilder.DropIndex(
                name: "IX_Products_ProductBundleId",
                table: "Products");

            migrationBuilder.DropColumn(
                name: "ProductBundleId",
                table: "Products");
        }
    }
}
