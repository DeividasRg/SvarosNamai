using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SvarosNamai.Serivce.OrderAPI.Migrations
{
    /// <inheritdoc />
    public partial class updateavailableSlots : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_AvailableTimeSlots",
                table: "AvailableTimeSlots");

            migrationBuilder.DropColumn(
                name: "Date",
                table: "AvailableTimeSlots");

            migrationBuilder.AlterColumn<DateOnly>(
                name: "DayDate",
                table: "AvailableTimeSlots",
                type: "date",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.AddColumn<string>(
                name: "DayName",
                table: "AvailableTimeSlots",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddPrimaryKey(
                name: "PK_AvailableTimeSlots",
                table: "AvailableTimeSlots",
                column: "DayDate");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_AvailableTimeSlots",
                table: "AvailableTimeSlots");

            migrationBuilder.DropColumn(
                name: "DayName",
                table: "AvailableTimeSlots");

            migrationBuilder.AlterColumn<string>(
                name: "DayDate",
                table: "AvailableTimeSlots",
                type: "nvarchar(max)",
                nullable: false,
                oldClrType: typeof(DateOnly),
                oldType: "date");

            migrationBuilder.AddColumn<DateOnly>(
                name: "Date",
                table: "AvailableTimeSlots",
                type: "date",
                nullable: false,
                defaultValue: new DateOnly(1, 1, 1));

            migrationBuilder.AddPrimaryKey(
                name: "PK_AvailableTimeSlots",
                table: "AvailableTimeSlots",
                column: "Date");
        }
    }
}
