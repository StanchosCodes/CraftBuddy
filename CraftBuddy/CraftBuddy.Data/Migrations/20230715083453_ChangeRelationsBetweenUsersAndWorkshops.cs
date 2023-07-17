using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CraftBuddy.Data.Migrations
{
    public partial class ChangeRelationsBetweenUsersAndWorkshops : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_WorkshopsParticipants_AspNetUsers_OrganiserId",
                table: "WorkshopsParticipants");

            migrationBuilder.RenameColumn(
                name: "OrganiserId",
                table: "WorkshopsParticipants",
                newName: "ParticipantId");

            migrationBuilder.RenameIndex(
                name: "IX_WorkshopsParticipants_OrganiserId",
                table: "WorkshopsParticipants",
                newName: "IX_WorkshopsParticipants_ParticipantId");

            migrationBuilder.AddForeignKey(
                name: "FK_WorkshopsParticipants_AspNetUsers_ParticipantId",
                table: "WorkshopsParticipants",
                column: "ParticipantId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_WorkshopsParticipants_AspNetUsers_ParticipantId",
                table: "WorkshopsParticipants");

            migrationBuilder.RenameColumn(
                name: "ParticipantId",
                table: "WorkshopsParticipants",
                newName: "OrganiserId");

            migrationBuilder.RenameIndex(
                name: "IX_WorkshopsParticipants_ParticipantId",
                table: "WorkshopsParticipants",
                newName: "IX_WorkshopsParticipants_OrganiserId");

            migrationBuilder.AddForeignKey(
                name: "FK_WorkshopsParticipants_AspNetUsers_OrganiserId",
                table: "WorkshopsParticipants",
                column: "OrganiserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
