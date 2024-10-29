using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MovieManagementApp.Migrations
{
    public partial class Remove_URLS_Video_Poster : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "PosterUrl",
                table: "AppMovies");

            migrationBuilder.RenameColumn(
                name: "VideoUrl",
                table: "AppMovies",
                newName: "PosterBlob");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "PosterBlob",
                table: "AppMovies",
                newName: "VideoUrl");

            migrationBuilder.AddColumn<string>(
                name: "PosterUrl",
                table: "AppMovies",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }
    }
}
