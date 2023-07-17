using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CraftBuddy.Data.Migrations
{
    public partial class FixingColumnsNaming : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Orders_AspNetUsers_UserId",
                table: "Orders");

            migrationBuilder.DropForeignKey(
                name: "FK_Orders_OrderStatuses_OrderStatusId",
                table: "Orders");

            migrationBuilder.DropForeignKey(
                name: "FK_Products_AspNetUsers_UserId",
                table: "Products");

            migrationBuilder.DropForeignKey(
                name: "FK_ProductsOrders_Orders_OrderId",
                table: "ProductsOrders");

            migrationBuilder.DropForeignKey(
                name: "FK_ProductsOrders_Products_ProductId",
                table: "ProductsOrders");

            migrationBuilder.DropForeignKey(
                name: "FK_WorkshopsParticipants_AspNetUsers_UserId",
                table: "WorkshopsParticipants");

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

            migrationBuilder.RenameColumn(
                name: "UserId",
                table: "Orders",
                newName: "ClientId");

            migrationBuilder.RenameColumn(
                name: "OrderStatusId",
                table: "Orders",
                newName: "StatusId");

            migrationBuilder.RenameIndex(
                name: "IX_Orders_UserId",
                table: "Orders",
                newName: "IX_Orders_ClientId");

            migrationBuilder.RenameIndex(
                name: "IX_Orders_OrderStatusId",
                table: "Orders",
                newName: "IX_Orders_StatusId");

            migrationBuilder.AlterColumn<decimal>(
                name: "Price",
                table: "Orders",
                type: "decimal(18,2)",
                nullable: true,
                oldClrType: typeof(decimal),
                oldType: "decimal(18,2)");

            migrationBuilder.AddForeignKey(
                name: "FK_Orders_AspNetUsers_ClientId",
                table: "Orders",
                column: "ClientId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Orders_OrderStatuses_StatusId",
                table: "Orders",
                column: "StatusId",
                principalTable: "OrderStatuses",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Products_AspNetUsers_CrafterId",
                table: "Products",
                column: "CrafterId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_ProductsOrders_Orders_OrderId",
                table: "ProductsOrders",
                column: "OrderId",
                principalTable: "Orders",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_ProductsOrders_Products_ProductId",
                table: "ProductsOrders",
                column: "ProductId",
                principalTable: "Products",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_WorkshopsParticipants_AspNetUsers_ParticipantId",
                table: "WorkshopsParticipants",
                column: "ParticipantId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Orders_AspNetUsers_ClientId",
                table: "Orders");

            migrationBuilder.DropForeignKey(
                name: "FK_Orders_OrderStatuses_StatusId",
                table: "Orders");

            migrationBuilder.DropForeignKey(
                name: "FK_Products_AspNetUsers_CrafterId",
                table: "Products");

            migrationBuilder.DropForeignKey(
                name: "FK_ProductsOrders_Orders_OrderId",
                table: "ProductsOrders");

            migrationBuilder.DropForeignKey(
                name: "FK_ProductsOrders_Products_ProductId",
                table: "ProductsOrders");

            migrationBuilder.DropForeignKey(
                name: "FK_WorkshopsParticipants_AspNetUsers_ParticipantId",
                table: "WorkshopsParticipants");

            migrationBuilder.RenameColumn(
                name: "ParticipantId",
                table: "WorkshopsParticipants",
                newName: "UserId");

            migrationBuilder.RenameIndex(
                name: "IX_WorkshopsParticipants_ParticipantId",
                table: "WorkshopsParticipants",
                newName: "IX_WorkshopsParticipants_UserId");

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

            migrationBuilder.RenameColumn(
                name: "StatusId",
                table: "Orders",
                newName: "OrderStatusId");

            migrationBuilder.RenameColumn(
                name: "ClientId",
                table: "Orders",
                newName: "UserId");

            migrationBuilder.RenameIndex(
                name: "IX_Orders_StatusId",
                table: "Orders",
                newName: "IX_Orders_OrderStatusId");

            migrationBuilder.RenameIndex(
                name: "IX_Orders_ClientId",
                table: "Orders",
                newName: "IX_Orders_UserId");

            migrationBuilder.AlterColumn<decimal>(
                name: "Price",
                table: "Orders",
                type: "decimal(18,2)",
                nullable: false,
                defaultValue: 0m,
                oldClrType: typeof(decimal),
                oldType: "decimal(18,2)",
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_Orders_AspNetUsers_UserId",
                table: "Orders",
                column: "UserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Orders_OrderStatuses_OrderStatusId",
                table: "Orders",
                column: "OrderStatusId",
                principalTable: "OrderStatuses",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Products_AspNetUsers_UserId",
                table: "Products",
                column: "UserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_ProductsOrders_Orders_OrderId",
                table: "ProductsOrders",
                column: "OrderId",
                principalTable: "Orders",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_ProductsOrders_Products_ProductId",
                table: "ProductsOrders",
                column: "ProductId",
                principalTable: "Products",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_WorkshopsParticipants_AspNetUsers_UserId",
                table: "WorkshopsParticipants",
                column: "UserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
