using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SvarosNamai.Service.ProductAPI.Migrations
{
    /// <inheritdoc />
    public partial class productDbChange : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ProductBundle");

            migrationBuilder.DropColumn(
                name: "Discount",
                table: "Bundles");

            migrationBuilder.RenameColumn(
                name: "Price",
                table: "Bundles",
                newName: "HourPrice");

            migrationBuilder.AddColumn<double>(
                name: "Price",
                table: "Products",
                type: "float",
                nullable: false,
                defaultValue: 0.0);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Price",
                table: "Products");

            migrationBuilder.RenameColumn(
                name: "HourPrice",
                table: "Bundles",
                newName: "Price");

            migrationBuilder.AddColumn<double>(
                name: "Discount",
                table: "Bundles",
                type: "float",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "ProductBundle",
                columns: table => new
                {
                    ProductBundleId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    BundleId = table.Column<int>(type: "int", nullable: false),
                    ProductId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ProductBundle", x => x.ProductBundleId);
                });
        }
    }
}
