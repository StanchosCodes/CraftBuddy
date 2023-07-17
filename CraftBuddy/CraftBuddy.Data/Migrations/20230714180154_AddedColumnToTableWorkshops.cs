using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CraftBuddy.Data.Migrations
{
    public partial class AddedColumnToTableWorkshops : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "ImagePath",
                table: "Workshops",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ImagePath",
                table: "Workshops");
        }
    }
}
