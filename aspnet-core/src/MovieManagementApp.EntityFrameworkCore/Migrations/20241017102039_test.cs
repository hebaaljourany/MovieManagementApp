using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MovieManagementApp.Migrations
{
    public partial class test : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_AppMovieActors_AppActors_ActorId1",
                table: "AppMovieActors");

            migrationBuilder.DropForeignKey(
                name: "FK_AppMovieActors_AppMovies_MovieId1",
                table: "AppMovieActors");

            migrationBuilder.DropIndex(
                name: "IX_AppMovieActors_ActorId1",
                table: "AppMovieActors");

            migrationBuilder.DropIndex(
                name: "IX_AppMovieActors_MovieId1",
                table: "AppMovieActors");

            migrationBuilder.DropColumn(
                name: "ActorId1",
                table: "AppMovieActors");

            migrationBuilder.DropColumn(
                name: "MovieId1",
                table: "AppMovieActors");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<Guid>(
                name: "ActorId1",
                table: "AppMovieActors",
                type: "uniqueidentifier",
                nullable: true);

            migrationBuilder.AddColumn<Guid>(
                name: "MovieId1",
                table: "AppMovieActors",
                type: "uniqueidentifier",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_AppMovieActors_ActorId1",
                table: "AppMovieActors",
                column: "ActorId1");

            migrationBuilder.CreateIndex(
                name: "IX_AppMovieActors_MovieId1",
                table: "AppMovieActors",
                column: "MovieId1");

            migrationBuilder.AddForeignKey(
                name: "FK_AppMovieActors_AppActors_ActorId1",
                table: "AppMovieActors",
                column: "ActorId1",
                principalTable: "AppActors",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_AppMovieActors_AppMovies_MovieId1",
                table: "AppMovieActors",
                column: "MovieId1",
                principalTable: "AppMovies",
                principalColumn: "Id");
        }
    }
}
