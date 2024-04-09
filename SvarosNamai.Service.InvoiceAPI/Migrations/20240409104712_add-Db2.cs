﻿using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SvarosNamai.Service.InvoiceAPI.Migrations
{
    /// <inheritdoc />
    public partial class addDb2 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "OrderId",
                table: "Invoices",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "OrderId",
                table: "Invoices");
        }
    }
}
