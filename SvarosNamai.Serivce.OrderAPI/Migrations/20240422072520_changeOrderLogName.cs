using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SvarosNamai.Serivce.OrderAPI.Migrations
{
    /// <inheritdoc />
    public partial class changeOrderLogName : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "OrderStatus",
                table: "OrderLogs",
                newName: "NewOrderStatus");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "NewOrderStatus",
                table: "OrderLogs",
                newName: "OrderStatus");
        }
    }
}
