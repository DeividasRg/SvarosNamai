using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SvarosNamai.Serivce.OrderAPI.Migrations
{
    /// <inheritdoc />
    public partial class addReservation : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "OrderDate",
                table: "Orders");

            migrationBuilder.RenameColumn(
                name: "Hour",
                table: "Orders",
                newName: "ReservationId");

            migrationBuilder.CreateTable(
                name: "Reservations",
                columns: table => new
                {
                    ReservationId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Date = table.Column<DateOnly>(type: "date", nullable: false),
                    Hour = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Reservations", x => x.ReservationId);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Orders_ReservationId",
                table: "Orders",
                column: "ReservationId");

            migrationBuilder.AddForeignKey(
                name: "FK_Orders_Reservations_ReservationId",
                table: "Orders",
                column: "ReservationId",
                principalTable: "Reservations",
                principalColumn: "ReservationId",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Orders_Reservations_ReservationId",
                table: "Orders");

            migrationBuilder.DropTable(
                name: "Reservations");

            migrationBuilder.DropIndex(
                name: "IX_Orders_ReservationId",
                table: "Orders");

            migrationBuilder.RenameColumn(
                name: "ReservationId",
                table: "Orders",
                newName: "Hour");

            migrationBuilder.AddColumn<DateOnly>(
                name: "OrderDate",
                table: "Orders",
                type: "date",
                nullable: false,
                defaultValue: new DateOnly(1, 1, 1));
        }
    }
}
