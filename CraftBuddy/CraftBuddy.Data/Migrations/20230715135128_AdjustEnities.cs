using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CraftBuddy.Data.Migrations
{
    public partial class AdjustEnities : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Products_AspNetUsers_CrafterId",
                table: "Products");

            migrationBuilder.DropForeignKey(
                name: "FK_Purchases_AspNetUsers_ClientId",
                table: "Purchases");

            migrationBuilder.DropForeignKey(
                name: "FK_Sets_AspNetUsers_MakerId",
                table: "Sets");

            migrationBuilder.DropForeignKey(
                name: "FK_WorkshopsParticipants_AspNetUsers_ParticipantId",
                table: "WorkshopsParticipants");

            migrationBuilder.DropTable(
                name: "CustomOrders");

            migrationBuilder.RenameColumn(
                name: "ParticipantId",
                table: "WorkshopsParticipants",
                newName: "UserId");

            migrationBuilder.RenameIndex(
                name: "IX_WorkshopsParticipants_ParticipantId",
                table: "WorkshopsParticipants",
                newName: "IX_WorkshopsParticipants_UserId");

            migrationBuilder.RenameColumn(
                name: "MakerId",
                table: "Sets",
                newName: "UserId");

            migrationBuilder.RenameIndex(
                name: "IX_Sets_MakerId",
                table: "Sets",
                newName: "IX_Sets_UserId");

            migrationBuilder.RenameColumn(
                name: "ClientId",
                table: "Purchases",
                newName: "UserId");

            migrationBuilder.RenameColumn(
                name: "Amount",
                table: "Purchases",
                newName: "Price");

            migrationBuilder.RenameIndex(
                name: "IX_Purchases_ClientId",
                table: "Purchases",
                newName: "IX_Purchases_UserId");

            migrationBuilder.RenameColumn(
                name: "CreatedOn",
                table: "Products",
                newName: "CreatedAt");

            migrationBuilder.RenameColumn(
                name: "CrafterId",
                table: "Products",
                newName: "UserId");

            migrationBuilder.RenameIndex(
                name: "IX_Products_CrafterId",
                table: "Products",
                newName: "IX_Products_UserId");

            migrationBuilder.AddColumn<int>(
                name: "OrderStatusId",
                table: "Purchases",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AlterColumn<decimal>(
                name: "Price",
                table: "Products",
                type: "decimal(18,2)",
                nullable: true,
                oldClrType: typeof(decimal),
                oldType: "decimal(18,2)");

            migrationBuilder.AddColumn<bool>(
                name: "IsCustom",
                table: "Products",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.CreateIndex(
                name: "IX_Purchases_OrderStatusId",
                table: "Purchases",
                column: "OrderStatusId");

            migrationBuilder.AddForeignKey(
                name: "FK_Products_AspNetUsers_UserId",
                table: "Products",
                column: "UserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Purchases_AspNetUsers_UserId",
                table: "Purchases",
                column: "UserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Purchases_OrderStatuses_OrderStatusId",
                table: "Purchases",
                column: "OrderStatusId",
                principalTable: "OrderStatuses",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Sets_AspNetUsers_UserId",
                table: "Sets",
                column: "UserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_WorkshopsParticipants_AspNetUsers_UserId",
                table: "WorkshopsParticipants",
                column: "UserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Products_AspNetUsers_UserId",
                table: "Products");

            migrationBuilder.DropForeignKey(
                name: "FK_Purchases_AspNetUsers_UserId",
                table: "Purchases");

            migrationBuilder.DropForeignKey(
                name: "FK_Purchases_OrderStatuses_OrderStatusId",
                table: "Purchases");

            migrationBuilder.DropForeignKey(
                name: "FK_Sets_AspNetUsers_UserId",
                table: "Sets");

            migrationBuilder.DropForeignKey(
                name: "FK_WorkshopsParticipants_AspNetUsers_UserId",
                table: "WorkshopsParticipants");

            migrationBuilder.DropIndex(
                name: "IX_Purchases_OrderStatusId",
                table: "Purchases");

            migrationBuilder.DropColumn(
                name: "OrderStatusId",
                table: "Purchases");

            migrationBuilder.DropColumn(
                name: "IsCustom",
                table: "Products");

            migrationBuilder.RenameColumn(
                name: "UserId",
                table: "WorkshopsParticipants",
                newName: "ParticipantId");

            migrationBuilder.RenameIndex(
                name: "IX_WorkshopsParticipants_UserId",
                table: "WorkshopsParticipants",
                newName: "IX_WorkshopsParticipants_ParticipantId");

            migrationBuilder.RenameColumn(
                name: "UserId",
                table: "Sets",
                newName: "MakerId");

            migrationBuilder.RenameIndex(
                name: "IX_Sets_UserId",
                table: "Sets",
                newName: "IX_Sets_MakerId");

            migrationBuilder.RenameColumn(
                name: "UserId",
                table: "Purchases",
                newName: "ClientId");

            migrationBuilder.RenameColumn(
                name: "Price",
                table: "Purchases",
                newName: "Amount");

            migrationBuilder.RenameIndex(
                name: "IX_Purchases_UserId",
                table: "Purchases",
                newName: "IX_Purchases_ClientId");

            migrationBuilder.RenameColumn(
                name: "UserId",
                table: "Products",
                newName: "CrafterId");

            migrationBuilder.RenameColumn(
                name: "CreatedAt",
                table: "Products",
                newName: "CreatedOn");

            migrationBuilder.RenameIndex(
                name: "IX_Products_UserId",
                table: "Products",
                newName: "IX_Products_CrafterId");

            migrationBuilder.AlterColumn<decimal>(
                name: "Price",
                table: "Products",
                type: "decimal(18,2)",
                nullable: false,
                defaultValue: 0m,
                oldClrType: typeof(decimal),
                oldType: "decimal(18,2)",
                oldNullable: true);

            migrationBuilder.CreateTable(
                name: "CustomOrders",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ClientId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    ProductTypeId = table.Column<int>(type: "int", nullable: false),
                    StatusId = table.Column<int>(type: "int", nullable: false),
                    CreatedOn = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Description = table.Column<string>(type: "nvarchar(80)", maxLength: 80, nullable: false),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CustomOrders", x => x.Id);
                    table.ForeignKey(
                        name: "FK_CustomOrders_AspNetUsers_ClientId",
                        column: x => x.ClientId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_CustomOrders_OrderStatuses_StatusId",
                        column: x => x.StatusId,
                        principalTable: "OrderStatuses",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_CustomOrders_ProductTypes_ProductTypeId",
                        column: x => x.ProductTypeId,
                        principalTable: "ProductTypes",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_CustomOrders_ClientId",
                table: "CustomOrders",
                column: "ClientId");

            migrationBuilder.CreateIndex(
                name: "IX_CustomOrders_ProductTypeId",
                table: "CustomOrders",
                column: "ProductTypeId");

            migrationBuilder.CreateIndex(
                name: "IX_CustomOrders_StatusId",
                table: "CustomOrders",
                column: "StatusId");

            migrationBuilder.AddForeignKey(
                name: "FK_Products_AspNetUsers_CrafterId",
                table: "Products",
                column: "CrafterId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Purchases_AspNetUsers_ClientId",
                table: "Purchases",
                column: "ClientId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Sets_AspNetUsers_MakerId",
                table: "Sets",
                column: "MakerId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_WorkshopsParticipants_AspNetUsers_ParticipantId",
                table: "WorkshopsParticipants",
                column: "ParticipantId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
