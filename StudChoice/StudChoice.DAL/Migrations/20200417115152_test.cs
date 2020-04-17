using Microsoft.EntityFrameworkCore.Migrations;

namespace StudChoice.DAL.Migrations
{
    public partial class test : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "type",
                table: "Subjects",
                newName: "Type");

            migrationBuilder.RenameColumn(
                name: "name",
                table: "Subjects",
                newName: "Name");

            migrationBuilder.RenameColumn(
                name: "description",
                table: "Subjects",
                newName: "Description");

            migrationBuilder.AddColumn<long>(
                name: "CathedraId",
                table: "Subjects",
                nullable: true);

            migrationBuilder.AddColumn<long>(
                name: "ProfessorId",
                table: "Subjects",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "Faculty",
                columns: table => new
                {
                    Id = table.Column<long>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    DisplayName = table.Column<string>(nullable: true),
                    Description = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Faculty", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Professor",
                columns: table => new
                {
                    Id = table.Column<long>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Professor", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Cathedra",
                columns: table => new
                {
                    Id = table.Column<long>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    DisplayName = table.Column<string>(nullable: true),
                    FacultyId = table.Column<long>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Cathedra", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Cathedra_Faculty_FacultyId",
                        column: x => x.FacultyId,
                        principalTable: "Faculty",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Subjects_CathedraId",
                table: "Subjects",
                column: "CathedraId");

            migrationBuilder.CreateIndex(
                name: "IX_Subjects_ProfessorId",
                table: "Subjects",
                column: "ProfessorId");

            migrationBuilder.CreateIndex(
                name: "IX_Cathedra_FacultyId",
                table: "Cathedra",
                column: "FacultyId");

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

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Subjects_Cathedra_CathedraId",
                table: "Subjects");

            migrationBuilder.DropForeignKey(
                name: "FK_Subjects_Professor_ProfessorId",
                table: "Subjects");

            migrationBuilder.DropTable(
                name: "Cathedra");

            migrationBuilder.DropTable(
                name: "Professor");

            migrationBuilder.DropTable(
                name: "Faculty");

            migrationBuilder.DropIndex(
                name: "IX_Subjects_CathedraId",
                table: "Subjects");

            migrationBuilder.DropIndex(
                name: "IX_Subjects_ProfessorId",
                table: "Subjects");

            migrationBuilder.DropColumn(
                name: "CathedraId",
                table: "Subjects");

            migrationBuilder.DropColumn(
                name: "ProfessorId",
                table: "Subjects");

            migrationBuilder.RenameColumn(
                name: "Type",
                table: "Subjects",
                newName: "type");

            migrationBuilder.RenameColumn(
                name: "Name",
                table: "Subjects",
                newName: "name");

            migrationBuilder.RenameColumn(
                name: "Description",
                table: "Subjects",
                newName: "description");
        }
    }
}
