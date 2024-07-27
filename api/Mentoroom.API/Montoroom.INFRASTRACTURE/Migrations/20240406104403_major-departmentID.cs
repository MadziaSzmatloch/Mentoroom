using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Mentoroom.INFRASTRACTURE.Migrations
{
    /// <inheritdoc />
    public partial class majordepartmentID : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<Guid>(
                name: "DepartmentId",
                table: "Majors",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.CreateIndex(
                name: "IX_Majors_DepartmentId",
                table: "Majors",
                column: "DepartmentId");

            migrationBuilder.AddForeignKey(
                name: "FK_Majors_Departments_DepartmentId",
                table: "Majors",
                column: "DepartmentId",
                principalTable: "Departments",
                principalColumn: "Id",
                onDelete: ReferentialAction.NoAction);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Majors_Departments_DepartmentId",
                table: "Majors");

            migrationBuilder.DropIndex(
                name: "IX_Majors_DepartmentId",
                table: "Majors");

            migrationBuilder.DropColumn(
                name: "DepartmentId",
                table: "Majors");
        }
    }
}
