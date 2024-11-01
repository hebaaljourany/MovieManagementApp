using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MovieManagementApp.Migrations
{
    public partial class Actor_Thumbnail : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Thumbnail",
                table: "AppActors",
                type: "nvarchar(max)",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Thumbnail",
                table: "AppActors");
        }
    }
}
