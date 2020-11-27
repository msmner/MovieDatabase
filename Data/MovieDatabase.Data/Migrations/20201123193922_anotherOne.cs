using Microsoft.EntityFrameworkCore.Migrations;

namespace MovieDatabase.Data.Migrations
{
    public partial class anotherOne : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "FirstQuote",
                table: "Reviews");

            migrationBuilder.DropColumn(
                name: "SecondQuote",
                table: "Reviews");

            migrationBuilder.DropColumn(
                name: "ThirdQuote",
                table: "Reviews");

            migrationBuilder.AddColumn<string>(
                name: "Quote",
                table: "Movies",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Quote",
                table: "Movies");

            migrationBuilder.AddColumn<string>(
                name: "FirstQuote",
                table: "Reviews",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "SecondQuote",
                table: "Reviews",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "ThirdQuote",
                table: "Reviews",
                type: "nvarchar(max)",
                nullable: true);
        }
    }
}
