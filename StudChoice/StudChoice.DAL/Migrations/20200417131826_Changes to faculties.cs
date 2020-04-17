using Microsoft.EntityFrameworkCore.Migrations;

namespace StudChoice.DAL.Migrations
{
    public partial class Changestofaculties : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Cathedra_Faculty_FacultyId",
                table: "Cathedra");

            migrationBuilder.DropForeignKey(
                name: "FK_Subjects_Cathedra_CathedraId",
                table: "Subjects");

            migrationBuilder.DropForeignKey(
                name: "FK_Subjects_Professor_ProfessorId",
                table: "Subjects");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Professor",
                table: "Professor");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Faculty",
                table: "Faculty");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Cathedra",
                table: "Cathedra");

            migrationBuilder.DropIndex(
                name: "IX_Cathedra_FacultyId",
                table: "Cathedra");

            migrationBuilder.RenameTable(
                name: "Professor",
                newName: "Professors");

            migrationBuilder.RenameTable(
                name: "Faculty",
                newName: "Faculties");

            migrationBuilder.RenameTable(
                name: "Cathedra",
                newName: "Cathedras");

            migrationBuilder.AlterColumn<int>(
                name: "FacultyId",
                table: "Cathedras",
                nullable: false,
                oldClrType: typeof(long),
                oldType: "bigint",
                oldNullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Description",
                table: "Cathedras",
                nullable: true);

            migrationBuilder.AddPrimaryKey(
                name: "PK_Professors",
                table: "Professors",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Faculties",
                table: "Faculties",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Cathedras",
                table: "Cathedras",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Subjects_Cathedras_CathedraId",
                table: "Subjects",
                column: "CathedraId",
                principalTable: "Cathedras",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Subjects_Professors_ProfessorId",
                table: "Subjects",
                column: "ProfessorId",
                principalTable: "Professors",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Subjects_Cathedras_CathedraId",
                table: "Subjects");

            migrationBuilder.DropForeignKey(
                name: "FK_Subjects_Professors_ProfessorId",
                table: "Subjects");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Professors",
                table: "Professors");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Faculties",
                table: "Faculties");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Cathedras",
                table: "Cathedras");

            migrationBuilder.DropColumn(
                name: "Description",
                table: "Cathedras");

            migrationBuilder.RenameTable(
                name: "Professors",
                newName: "Professor");

            migrationBuilder.RenameTable(
                name: "Faculties",
                newName: "Faculty");

            migrationBuilder.RenameTable(
                name: "Cathedras",
                newName: "Cathedra");

            migrationBuilder.AlterColumn<long>(
                name: "FacultyId",
                table: "Cathedra",
                type: "bigint",
                nullable: true,
                oldClrType: typeof(int));

            migrationBuilder.AddPrimaryKey(
                name: "PK_Professor",
                table: "Professor",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Faculty",
                table: "Faculty",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Cathedra",
                table: "Cathedra",
                column: "Id");

            migrationBuilder.CreateIndex(
                name: "IX_Cathedra_FacultyId",
                table: "Cathedra",
                column: "FacultyId");

            migrationBuilder.AddForeignKey(
                name: "FK_Cathedra_Faculty_FacultyId",
                table: "Cathedra",
                column: "FacultyId",
                principalTable: "Faculty",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Subjects_Cathedra_CathedraId",
                table: "Subjects",
                column: "CathedraId",
                principalTable: "Cathedra",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Subjects_Professor_ProfessorId",
                table: "Subjects",
                column: "ProfessorId",
                principalTable: "Professor",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
