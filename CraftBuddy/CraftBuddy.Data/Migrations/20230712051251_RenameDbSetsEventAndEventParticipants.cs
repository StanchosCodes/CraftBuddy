using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CraftBuddy.Data.Migrations
{
    public partial class RenameDbSetsEventAndEventParticipants : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Events_AspNetUsers_OrganiserId",
                table: "Events");

            migrationBuilder.DropForeignKey(
                name: "FK_EventsParticipants_AspNetUsers_OrganiserId",
                table: "EventsParticipants");

            migrationBuilder.DropForeignKey(
                name: "FK_EventsParticipants_Events_WorkshopId",
                table: "EventsParticipants");

            migrationBuilder.DropPrimaryKey(
                name: "PK_EventsParticipants",
                table: "EventsParticipants");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Events",
                table: "Events");

            migrationBuilder.RenameTable(
                name: "EventsParticipants",
                newName: "WorkshopsParticipants");

            migrationBuilder.RenameTable(
                name: "Events",
                newName: "Workshops");

            migrationBuilder.RenameIndex(
                name: "IX_EventsParticipants_OrganiserId",
                table: "WorkshopsParticipants",
                newName: "IX_WorkshopsParticipants_OrganiserId");

            migrationBuilder.RenameIndex(
                name: "IX_Events_OrganiserId",
                table: "Workshops",
                newName: "IX_Workshops_OrganiserId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_WorkshopsParticipants",
                table: "WorkshopsParticipants",
                columns: new[] { "WorkshopId", "OrganiserId" });

            migrationBuilder.AddPrimaryKey(
                name: "PK_Workshops",
                table: "Workshops",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Workshops_AspNetUsers_OrganiserId",
                table: "Workshops",
                column: "OrganiserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_WorkshopsParticipants_AspNetUsers_OrganiserId",
                table: "WorkshopsParticipants",
                column: "OrganiserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_WorkshopsParticipants_Workshops_WorkshopId",
                table: "WorkshopsParticipants",
                column: "WorkshopId",
                principalTable: "Workshops",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Workshops_AspNetUsers_OrganiserId",
                table: "Workshops");

            migrationBuilder.DropForeignKey(
                name: "FK_WorkshopsParticipants_AspNetUsers_OrganiserId",
                table: "WorkshopsParticipants");

            migrationBuilder.DropForeignKey(
                name: "FK_WorkshopsParticipants_Workshops_WorkshopId",
                table: "WorkshopsParticipants");

            migrationBuilder.DropPrimaryKey(
                name: "PK_WorkshopsParticipants",
                table: "WorkshopsParticipants");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Workshops",
                table: "Workshops");

            migrationBuilder.RenameTable(
                name: "WorkshopsParticipants",
                newName: "EventsParticipants");

            migrationBuilder.RenameTable(
                name: "Workshops",
                newName: "Events");

            migrationBuilder.RenameIndex(
                name: "IX_WorkshopsParticipants_OrganiserId",
                table: "EventsParticipants",
                newName: "IX_EventsParticipants_OrganiserId");

            migrationBuilder.RenameIndex(
                name: "IX_Workshops_OrganiserId",
                table: "Events",
                newName: "IX_Events_OrganiserId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_EventsParticipants",
                table: "EventsParticipants",
                columns: new[] { "WorkshopId", "OrganiserId" });

            migrationBuilder.AddPrimaryKey(
                name: "PK_Events",
                table: "Events",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Events_AspNetUsers_OrganiserId",
                table: "Events",
                column: "OrganiserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_EventsParticipants_AspNetUsers_OrganiserId",
                table: "EventsParticipants",
                column: "OrganiserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_EventsParticipants_Events_WorkshopId",
                table: "EventsParticipants",
                column: "WorkshopId",
                principalTable: "Events",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
