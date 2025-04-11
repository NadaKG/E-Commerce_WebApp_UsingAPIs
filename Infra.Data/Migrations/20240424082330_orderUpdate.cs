using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Infra.Data.Migrations
{
    /// <inheritdoc />
    public partial class orderUpdate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Order_Details_Order_Items_OrderId",
                table: "Order_Details");

            migrationBuilder.DropForeignKey(
                name: "FK_Order_Details_Users_UserId",
                table: "Order_Details");

            migrationBuilder.DropIndex(
                name: "IX_Order_Details_UserId",
                table: "Order_Details");

            migrationBuilder.RenameColumn(
                name: "UserId",
                table: "Order_Details",
                newName: "Quantity");

            migrationBuilder.RenameColumn(
                name: "Total",
                table: "Order_Details",
                newName: "UnitPrice");

            migrationBuilder.RenameColumn(
                name: "TotalAmount",
                table: "Order",
                newName: "TotalPrice");

            migrationBuilder.AddColumn<int>(
                name: "ProductId",
                table: "Order_Details",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<DateTime>(
                name: "OrderDate",
                table: "Order",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<int>(
                name: "UserId1",
                table: "Order",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_Order_Details_ProductId",
                table: "Order_Details",
                column: "ProductId");

            migrationBuilder.CreateIndex(
                name: "IX_Order_UserId1",
                table: "Order",
                column: "UserId1");

            migrationBuilder.AddForeignKey(
                name: "FK_Order_Users_UserId1",
                table: "Order",
                column: "UserId1",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Order_Details_Order_OrderId",
                table: "Order_Details",
                column: "OrderId",
                principalTable: "Order",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Order_Details_Products_ProductId",
                table: "Order_Details",
                column: "ProductId",
                principalTable: "Products",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Order_Users_UserId1",
                table: "Order");

            migrationBuilder.DropForeignKey(
                name: "FK_Order_Details_Order_OrderId",
                table: "Order_Details");

            migrationBuilder.DropForeignKey(
                name: "FK_Order_Details_Products_ProductId",
                table: "Order_Details");

            migrationBuilder.DropIndex(
                name: "IX_Order_Details_ProductId",
                table: "Order_Details");

            migrationBuilder.DropIndex(
                name: "IX_Order_UserId1",
                table: "Order");

            migrationBuilder.DropColumn(
                name: "ProductId",
                table: "Order_Details");

            migrationBuilder.DropColumn(
                name: "OrderDate",
                table: "Order");

            migrationBuilder.DropColumn(
                name: "UserId1",
                table: "Order");

            migrationBuilder.RenameColumn(
                name: "UnitPrice",
                table: "Order_Details",
                newName: "Total");

            migrationBuilder.RenameColumn(
                name: "Quantity",
                table: "Order_Details",
                newName: "UserId");

            migrationBuilder.RenameColumn(
                name: "TotalPrice",
                table: "Order",
                newName: "TotalAmount");

            migrationBuilder.CreateIndex(
                name: "IX_Order_Details_UserId",
                table: "Order_Details",
                column: "UserId");

            migrationBuilder.AddForeignKey(
                name: "FK_Order_Details_Order_Items_OrderId",
                table: "Order_Details",
                column: "OrderId",
                principalTable: "Order_Items",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Order_Details_Users_UserId",
                table: "Order_Details",
                column: "UserId",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
