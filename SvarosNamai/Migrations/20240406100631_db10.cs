using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SvarosNamai.Service.ProductAPI.Migrations
{
    /// <inheritdoc />
    public partial class db10 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Products_Bundles_BundleId",
                table: "Products");

            migrationBuilder.DropIndex(
                name: "IX_Products_BundleId",
                table: "Products");

            migrationBuilder.DropColumn(
                name: "BundleId",
                table: "Products");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "BundleId",
                table: "Products",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Products_BundleId",
                table: "Products",
                column: "BundleId");

            migrationBuilder.AddForeignKey(
                name: "FK_Products_Bundles_BundleId",
                table: "Products",
                column: "BundleId",
                principalTable: "Bundles",
                principalColumn: "BundleId");
        }
    }
}
