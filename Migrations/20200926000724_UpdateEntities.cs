using Microsoft.EntityFrameworkCore.Migrations;

namespace UniversityLectures.Migrations
{
    public partial class UpdateEntities : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "ProfessorId",
                table: "Lecture",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Lecture_ProfessorId",
                table: "Lecture",
                column: "ProfessorId");

            migrationBuilder.AddForeignKey(
                name: "FK_Lecture_Professor_ProfessorId",
                table: "Lecture",
                column: "ProfessorId",
                principalTable: "Professor",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Lecture_Professor_ProfessorId",
                table: "Lecture");

            migrationBuilder.DropIndex(
                name: "IX_Lecture_ProfessorId",
                table: "Lecture");

            migrationBuilder.DropColumn(
                name: "ProfessorId",
                table: "Lecture");
        }
    }
}
