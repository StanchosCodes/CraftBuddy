using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CraftBuddy.Data.Migrations
{
    public partial class AddColumnsAndForeignKeyToTableOrders : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<Guid>(
                name: "CrafterId",
                table: "Orders",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.CreateIndex(
                name: "IX_Orders_CrafterId",
                table: "Orders",
                column: "CrafterId");

            migrationBuilder.AddForeignKey(
                name: "FK_Orders_AspNetUsers_CrafterId",
                table: "Orders",
                column: "CrafterId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Orders_AspNetUsers_CrafterId",
                table: "Orders");

            migrationBuilder.DropIndex(
                name: "IX_Orders_CrafterId",
                table: "Orders");

            migrationBuilder.DropColumn(
                name: "CrafterId",
                table: "Orders");
        }
    }
}
