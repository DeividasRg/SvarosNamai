using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SvarosNamai.Serivce.OrderAPI.Migrations
{
    /// <inheritdoc />
    public partial class removehours_addOpenSlots : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Hour",
                table: "Reservations");

            migrationBuilder.CreateTable(
                name: "OpenSlots",
                columns: table => new
                {
                    Monday = table.Column<int>(type: "int", nullable: false),
                    Tuesday = table.Column<int>(type: "int", nullable: false),
                    Wednesday = table.Column<int>(type: "int", nullable: false),
                    Thursday = table.Column<int>(type: "int", nullable: false),
                    Friday = table.Column<int>(type: "int", nullable: false),
                    Saturday = table.Column<int>(type: "int", nullable: false),
                    Sunday = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "OpenSlots");

            migrationBuilder.AddColumn<int>(
                name: "Hour",
                table: "Reservations",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }
    }
}
