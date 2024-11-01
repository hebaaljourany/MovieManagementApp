using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MovieManagementApp.Migrations
{
    public partial class AddActorImage : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "ActorImageBlob",
                table: "AppActors",
                type: "nvarchar(max)",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ActorImageBlob",
                table: "AppActors");
        }
    }
}
