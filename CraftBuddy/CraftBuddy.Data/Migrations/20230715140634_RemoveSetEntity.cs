using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CraftBuddy.Data.Migrations
{
    public partial class RemoveSetEntity : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Products_Sets_SetId",
                table: "Products");

            migrationBuilder.DropTable(
                name: "Sets");

            migrationBuilder.DropIndex(
                name: "IX_Products_SetId",
                table: "Products");

            migrationBuilder.DropColumn(
                name: "SetId",
                table: "Products");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "SetId",
                table: "Products",
                type: "int",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "Sets",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    UserId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    CreatedOn = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Description = table.Column<string>(type: "nvarchar(80)", maxLength: 80, nullable: false),
                    ImagePath = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(30)", maxLength: 30, nullable: false),
                    Price = table.Column<decimal>(type: "decimal(18,2)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Sets", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Sets_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Products_SetId",
                table: "Products",
                column: "SetId");

            migrationBuilder.CreateIndex(
                name: "IX_Sets_UserId",
                table: "Sets",
                column: "UserId");

            migrationBuilder.AddForeignKey(
                name: "FK_Products_Sets_SetId",
                table: "Products",
                column: "SetId",
                principalTable: "Sets",
                principalColumn: "Id");
        }
    }
}
