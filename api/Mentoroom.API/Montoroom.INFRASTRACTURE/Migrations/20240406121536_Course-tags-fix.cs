using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Mentoroom.INFRASTRACTURE.Migrations
{
    /// <inheritdoc />
    public partial class Coursetagsfix : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Courses_Degree_DegreeId",
                table: "Courses");

            migrationBuilder.DropForeignKey(
                name: "FK_Courses_Semester_SemesterId",
                table: "Courses");

            migrationBuilder.DropForeignKey(
                name: "FK_Courses_Year_YearId",
                table: "Courses");

            migrationBuilder.DropForeignKey(
                name: "FK_Semester_Year_YearId",
                table: "Semester");

            migrationBuilder.DropForeignKey(
                name: "FK_Year_Degree_DegreeId",
                table: "Year");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Year",
                table: "Year");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Semester",
                table: "Semester");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Degree",
                table: "Degree");

            migrationBuilder.RenameTable(
                name: "Year",
                newName: "Years");

            migrationBuilder.RenameTable(
                name: "Semester",
                newName: "Semesters");

            migrationBuilder.RenameTable(
                name: "Degree",
                newName: "Degrees");

            migrationBuilder.RenameIndex(
                name: "IX_Year_DegreeId",
                table: "Years",
                newName: "IX_Years_DegreeId");

            migrationBuilder.RenameIndex(
                name: "IX_Semester_YearId",
                table: "Semesters",
                newName: "IX_Semesters_YearId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Years",
                table: "Years",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Semesters",
                table: "Semesters",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Degrees",
                table: "Degrees",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Courses_Degrees_DegreeId",
                table: "Courses",
                column: "DegreeId",
                principalTable: "Degrees",
                principalColumn: "Id",
                onDelete: ReferentialAction.NoAction);

            migrationBuilder.AddForeignKey(
                name: "FK_Courses_Semesters_SemesterId",
                table: "Courses",
                column: "SemesterId",
                principalTable: "Semesters",
                principalColumn: "Id",
                onDelete: ReferentialAction.NoAction);

            migrationBuilder.AddForeignKey(
                name: "FK_Courses_Years_YearId",
                table: "Courses",
                column: "YearId",
                principalTable: "Years",
                principalColumn: "Id",
                onDelete: ReferentialAction.NoAction);

            migrationBuilder.AddForeignKey(
                name: "FK_Semesters_Years_YearId",
                table: "Semesters",
                column: "YearId",
                principalTable: "Years",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Years_Degrees_DegreeId",
                table: "Years",
                column: "DegreeId",
                principalTable: "Degrees",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Courses_Degrees_DegreeId",
                table: "Courses");

            migrationBuilder.DropForeignKey(
                name: "FK_Courses_Semesters_SemesterId",
                table: "Courses");

            migrationBuilder.DropForeignKey(
                name: "FK_Courses_Years_YearId",
                table: "Courses");

            migrationBuilder.DropForeignKey(
                name: "FK_Semesters_Years_YearId",
                table: "Semesters");

            migrationBuilder.DropForeignKey(
                name: "FK_Years_Degrees_DegreeId",
                table: "Years");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Years",
                table: "Years");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Semesters",
                table: "Semesters");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Degrees",
                table: "Degrees");

            migrationBuilder.RenameTable(
                name: "Years",
                newName: "Year");

            migrationBuilder.RenameTable(
                name: "Semesters",
                newName: "Semester");

            migrationBuilder.RenameTable(
                name: "Degrees",
                newName: "Degree");

            migrationBuilder.RenameIndex(
                name: "IX_Years_DegreeId",
                table: "Year",
                newName: "IX_Year_DegreeId");

            migrationBuilder.RenameIndex(
                name: "IX_Semesters_YearId",
                table: "Semester",
                newName: "IX_Semester_YearId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Year",
                table: "Year",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Semester",
                table: "Semester",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Degree",
                table: "Degree",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Courses_Degree_DegreeId",
                table: "Courses",
                column: "DegreeId",
                principalTable: "Degree",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Courses_Semester_SemesterId",
                table: "Courses",
                column: "SemesterId",
                principalTable: "Semester",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Courses_Year_YearId",
                table: "Courses",
                column: "YearId",
                principalTable: "Year",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Semester_Year_YearId",
                table: "Semester",
                column: "YearId",
                principalTable: "Year",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Year_Degree_DegreeId",
                table: "Year",
                column: "DegreeId",
                principalTable: "Degree",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
