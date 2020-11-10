using Microsoft.EntityFrameworkCore.Migrations;

namespace MovieDatabase.Data.Migrations
{
    public partial class second : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Votes_Movies_MovieId",
                table: "Votes");

            migrationBuilder.DropIndex(
                name: "IX_Votes_MovieId",
                table: "Votes");

            migrationBuilder.DropColumn(
                name: "MovieId",
                table: "Votes");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "MovieId",
                table: "Votes",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Votes_MovieId",
                table: "Votes",
                column: "MovieId");

            migrationBuilder.AddForeignKey(
                name: "FK_Votes_Movies_MovieId",
                table: "Votes",
                column: "MovieId",
                principalTable: "Movies",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
