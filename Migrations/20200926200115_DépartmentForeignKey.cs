using Microsoft.EntityFrameworkCore.Migrations;

namespace UniversityLectures.Migrations
{
    public partial class DépartmentForeignKey : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Professor_Department_DepartmentId",
                table: "Professor");

            migrationBuilder.AlterColumn<int>(
                name: "DepartmentId",
                table: "Professor",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_Professor_Department_DepartmentId",
                table: "Professor",
                column: "DepartmentId",
                principalTable: "Department",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Professor_Department_DepartmentId",
                table: "Professor");

            migrationBuilder.AlterColumn<int>(
                name: "DepartmentId",
                table: "Professor",
                type: "int",
                nullable: true,
                oldClrType: typeof(int));

            migrationBuilder.AddForeignKey(
                name: "FK_Professor_Department_DepartmentId",
                table: "Professor",
                column: "DepartmentId",
                principalTable: "Department",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
