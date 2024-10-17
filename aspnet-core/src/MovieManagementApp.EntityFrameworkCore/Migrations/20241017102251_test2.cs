using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MovieManagementApp.Migrations
{
    public partial class test2 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_AppMovieCategories_AppCategories_CategoryId1",
                table: "AppMovieCategories");

            migrationBuilder.DropForeignKey(
                name: "FK_AppMovieCategories_AppMovies_MovieId1",
                table: "AppMovieCategories");

            migrationBuilder.DropIndex(
                name: "IX_AppMovieCategories_CategoryId1",
                table: "AppMovieCategories");

            migrationBuilder.DropIndex(
                name: "IX_AppMovieCategories_MovieId1",
                table: "AppMovieCategories");

            migrationBuilder.DropColumn(
                name: "CategoryId1",
                table: "AppMovieCategories");

            migrationBuilder.DropColumn(
                name: "MovieId1",
                table: "AppMovieCategories");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<Guid>(
                name: "CategoryId1",
                table: "AppMovieCategories",
                type: "uniqueidentifier",
                nullable: true);

            migrationBuilder.AddColumn<Guid>(
                name: "MovieId1",
                table: "AppMovieCategories",
                type: "uniqueidentifier",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_AppMovieCategories_CategoryId1",
                table: "AppMovieCategories",
                column: "CategoryId1");

            migrationBuilder.CreateIndex(
                name: "IX_AppMovieCategories_MovieId1",
                table: "AppMovieCategories",
                column: "MovieId1");

            migrationBuilder.AddForeignKey(
                name: "FK_AppMovieCategories_AppCategories_CategoryId1",
                table: "AppMovieCategories",
                column: "CategoryId1",
                principalTable: "AppCategories",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_AppMovieCategories_AppMovies_MovieId1",
                table: "AppMovieCategories",
                column: "MovieId1",
                principalTable: "AppMovies",
                principalColumn: "Id");
        }
    }
}
