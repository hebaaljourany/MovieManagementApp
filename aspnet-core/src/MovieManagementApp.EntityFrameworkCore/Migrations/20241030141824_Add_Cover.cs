using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MovieManagementApp.Migrations
{
    public partial class Add_Cover : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "CoverBlob",
                table: "AppMovies",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "CoverBlob",
                table: "AppMovies");
        }
    }
}
