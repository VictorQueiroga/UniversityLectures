using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace UniversityLectures.Migrations
{
    public partial class UpdateProfessor : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Birthday",
                table: "Professor");

            migrationBuilder.AddColumn<DateTime>(
                name: "Birthdate",
                table: "Professor",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Birthdate",
                table: "Professor");

            migrationBuilder.AddColumn<DateTime>(
                name: "Birthday",
                table: "Professor",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));
        }
    }
}
