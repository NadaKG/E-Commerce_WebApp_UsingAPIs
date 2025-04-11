using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Infra.Data.Migrations
{
    /// <inheritdoc />
    public partial class orderrrrr : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Cart_Order_OrderId",
                table: "Cart");

            migrationBuilder.DropIndex(
                name: "IX_Cart_OrderId",
                table: "Cart");

            migrationBuilder.DropColumn(
                name: "CreatedAt",
                table: "Order");

            migrationBuilder.DropColumn(
                name: "OrderId",
                table: "Cart");

            migrationBuilder.RenameColumn(
                name: "UnitPrice",
                table: "Order_Details",
                newName: "Price");

            migrationBuilder.RenameColumn(
                name: "TotalPrice",
                table: "Order",
                newName: "TotalAmount");

            migrationBuilder.AddColumn<string>(
                name: "OrderNumber",
                table: "Order",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "Status",
                table: "Order",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "OrderNumber",
                table: "Order");

            migrationBuilder.DropColumn(
                name: "Status",
                table: "Order");

            migrationBuilder.RenameColumn(
                name: "Price",
                table: "Order_Details",
                newName: "UnitPrice");

            migrationBuilder.RenameColumn(
                name: "TotalAmount",
                table: "Order",
                newName: "TotalPrice");

            migrationBuilder.AddColumn<DateTime>(
                name: "CreatedAt",
                table: "Order",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<int>(
                name: "OrderId",
                table: "Cart",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Cart_OrderId",
                table: "Cart",
                column: "OrderId");

            migrationBuilder.AddForeignKey(
                name: "FK_Cart_Order_OrderId",
                table: "Cart",
                column: "OrderId",
                principalTable: "Order",
                principalColumn: "Id");
        }
    }
}
