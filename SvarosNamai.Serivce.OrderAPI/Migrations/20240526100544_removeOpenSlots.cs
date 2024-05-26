using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SvarosNamai.Serivce.OrderAPI.Migrations
{
    /// <inheritdoc />
    public partial class removeOpenSlots : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "OpenSlots");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "OpenSlots",
                columns: table => new
                {
                    Friday = table.Column<int>(type: "int", nullable: false),
                    Monday = table.Column<int>(type: "int", nullable: false),
                    Saturday = table.Column<int>(type: "int", nullable: false),
                    Sunday = table.Column<int>(type: "int", nullable: false),
                    Thursday = table.Column<int>(type: "int", nullable: false),
                    Tuesday = table.Column<int>(type: "int", nullable: false),
                    Wednesday = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                });

            migrationBuilder.CreateIndex(
                name: "IX_OpenSlots_Friday",
                table: "OpenSlots",
                column: "Friday",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_OpenSlots_Monday",
                table: "OpenSlots",
                column: "Monday",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_OpenSlots_Saturday",
                table: "OpenSlots",
                column: "Saturday",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_OpenSlots_Sunday",
                table: "OpenSlots",
                column: "Sunday",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_OpenSlots_Thursday",
                table: "OpenSlots",
                column: "Thursday",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_OpenSlots_Tuesday",
                table: "OpenSlots",
                column: "Tuesday",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_OpenSlots_Wednesday",
                table: "OpenSlots",
                column: "Wednesday",
                unique: true);
        }
    }
}
