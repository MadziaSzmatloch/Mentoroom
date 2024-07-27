using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Mentoroom.INFRASTRACTURE.Migrations
{
    /// <inheritdoc />
    public partial class CourseCoAuthorKeyAdded : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_CourseCoAuthors_CourseId",
                table: "CourseCoAuthors");

            migrationBuilder.AddPrimaryKey(
                name: "PK_CourseCoAuthors",
                table: "CourseCoAuthors",
                columns: new[] { "CourseId", "CoAuthorId" });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_CourseCoAuthors",
                table: "CourseCoAuthors");

            migrationBuilder.CreateIndex(
                name: "IX_CourseCoAuthors_CourseId",
                table: "CourseCoAuthors",
                column: "CourseId");
        }
    }
}
