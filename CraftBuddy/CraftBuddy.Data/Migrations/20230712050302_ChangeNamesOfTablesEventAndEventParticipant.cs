using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CraftBuddy.Data.Migrations
{
    public partial class ChangeNamesOfTablesEventAndEventParticipant : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_EventsParticipants_Events_EventId",
                table: "EventsParticipants");

            migrationBuilder.RenameColumn(
                name: "EventId",
                table: "EventsParticipants",
                newName: "WorkshopId");

            migrationBuilder.AddForeignKey(
                name: "FK_EventsParticipants_Events_WorkshopId",
                table: "EventsParticipants",
                column: "WorkshopId",
                principalTable: "Events",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_EventsParticipants_Events_WorkshopId",
                table: "EventsParticipants");

            migrationBuilder.RenameColumn(
                name: "WorkshopId",
                table: "EventsParticipants",
                newName: "EventId");

            migrationBuilder.AddForeignKey(
                name: "FK_EventsParticipants_Events_EventId",
                table: "EventsParticipants",
                column: "EventId",
                principalTable: "Events",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
