using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SvarosNamai.Service.ProductAPI.Migrations
{
    /// <inheritdoc />
    public partial class addDates : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<DateTime>(
                name: "DateCreated",
                table: "Bundles",
                type: "datetime2",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "DateUpdated",
                table: "Bundles",
                type: "datetime2",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "DateCreated",
                table: "Bundles");

            migrationBuilder.DropColumn(
                name: "DateUpdated",
                table: "Bundles");
        }
    }
}
