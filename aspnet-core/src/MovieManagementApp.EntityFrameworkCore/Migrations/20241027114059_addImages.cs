using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MovieManagementApp.Migrations
{
    public partial class addImages : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "CoverUrl",
                table: "AppMovies",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "ActorImage",
                table: "AppActors",
                type: "nvarchar(max)",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "CoverUrl",
                table: "AppMovies");

            migrationBuilder.DropColumn(
                name: "ActorImage",
                table: "AppActors");
        }
    }
}
