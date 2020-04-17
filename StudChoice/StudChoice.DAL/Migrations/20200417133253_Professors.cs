using Microsoft.EntityFrameworkCore.Migrations;

namespace StudChoice.DAL.Migrations
{
    public partial class Professors : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "CathedraId",
                table: "Professors",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "FacultyId",
                table: "Professors",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<string>(
                name: "FirstName",
                table: "Professors",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "LastName",
                table: "Professors",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "MiddleName",
                table: "Professors",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "CathedraId",
                table: "Professors");

            migrationBuilder.DropColumn(
                name: "FacultyId",
                table: "Professors");

            migrationBuilder.DropColumn(
                name: "FirstName",
                table: "Professors");

            migrationBuilder.DropColumn(
                name: "LastName",
                table: "Professors");

            migrationBuilder.DropColumn(
                name: "MiddleName",
                table: "Professors");
        }
    }
}
