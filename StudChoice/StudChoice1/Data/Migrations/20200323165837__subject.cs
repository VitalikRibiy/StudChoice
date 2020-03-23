using Microsoft.EntityFrameworkCore.Migrations;

namespace StudChoice1.Data.Migrations
{
    public partial class _subject : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            //migrationBuilder.DropTable(
            //    name: "SubjectModel");

            migrationBuilder.CreateTable(
                name: "Subject",
                columns: table => new
                {
                    id = table.Column<long>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    name = table.Column<string>(nullable: false),
                    description = table.Column<string>(nullable: true),
                    type = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Subject", x => x.id);
                });

            migrationBuilder.InsertData(
                table: "Subject",
                columns: new[] { "id", "description", "name", "type" },
                values: new object[] { 1L, "Matan", "MATAN", "DVVS" });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Subject");

            migrationBuilder.CreateTable(
                name: "SubjectModel",
                columns: table => new
                {
                    id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    description = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    type = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SubjectModel", x => x.id);
                });

            migrationBuilder.InsertData(
                table: "SubjectModel",
                columns: new[] { "id", "description", "name", "type" },
                values: new object[] { 1L, "Matan", "MATAN", "DVVS" });
        }
    }
}
