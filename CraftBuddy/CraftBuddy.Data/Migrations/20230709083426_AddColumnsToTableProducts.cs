using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CraftBuddy.Data.Migrations
{
    public partial class AddColumnsToTableProducts : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<Guid>(
                name: "CrafterId",
                table: "Products",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.CreateIndex(
                name: "IX_Products_CrafterId",
                table: "Products",
                column: "CrafterId");

            migrationBuilder.AddForeignKey(
                name: "FK_Products_AspNetUsers_CrafterId",
                table: "Products",
                column: "CrafterId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Products_AspNetUsers_CrafterId",
                table: "Products");

            migrationBuilder.DropIndex(
                name: "IX_Products_CrafterId",
                table: "Products");

            migrationBuilder.DropColumn(
                name: "CrafterId",
                table: "Products");
        }
    }
}
