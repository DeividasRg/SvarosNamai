using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace SvarosNamai.Serivce.OrderAPI.Migrations
{
    /// <inheritdoc />
    public partial class addSlots : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Slots",
                columns: table => new
                {
                    Weekday = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    OpenSlots = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Slots", x => x.Weekday);
                });

            migrationBuilder.InsertData(
                table: "Slots",
                columns: new[] { "Weekday", "OpenSlots" },
                values: new object[,]
                {
                    { "Friday", 0 },
                    { "Monday", 0 },
                    { "Saturday", 0 },
                    { "Sunday", 0 },
                    { "Thursday", 0 },
                    { "Tuesday", 0 },
                    { "Wednesday", 0 }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Slots");
        }
    }
}
