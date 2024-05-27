using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SvarosNamai.Serivce.OrderAPI.Migrations
{
    /// <inheritdoc />
    public partial class addUniqueValue : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
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

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_OpenSlots_Friday",
                table: "OpenSlots");

            migrationBuilder.DropIndex(
                name: "IX_OpenSlots_Monday",
                table: "OpenSlots");

            migrationBuilder.DropIndex(
                name: "IX_OpenSlots_Saturday",
                table: "OpenSlots");

            migrationBuilder.DropIndex(
                name: "IX_OpenSlots_Sunday",
                table: "OpenSlots");

            migrationBuilder.DropIndex(
                name: "IX_OpenSlots_Thursday",
                table: "OpenSlots");

            migrationBuilder.DropIndex(
                name: "IX_OpenSlots_Tuesday",
                table: "OpenSlots");

            migrationBuilder.DropIndex(
                name: "IX_OpenSlots_Wednesday",
                table: "OpenSlots");
        }
    }
}
