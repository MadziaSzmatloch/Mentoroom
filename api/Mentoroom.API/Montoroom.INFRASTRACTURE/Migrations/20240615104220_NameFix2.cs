using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Mentoroom.INFRASTRACTURE.Migrations
{
    /// <inheritdoc />
    public partial class NameFix2 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_StudentAssignmentFiles_StudentAssignments_AssignnmentId",
                table: "StudentAssignmentFiles");

            migrationBuilder.DropColumn(
                name: "AssignmentId",
                table: "StudentAssignmentFiles");

            migrationBuilder.RenameColumn(
                name: "AssignnmentId",
                table: "StudentAssignmentFiles",
                newName: "StudentAssignmentId");

            migrationBuilder.RenameIndex(
                name: "IX_StudentAssignmentFiles_AssignnmentId",
                table: "StudentAssignmentFiles",
                newName: "IX_StudentAssignmentFiles_StudentAssignmentId");

            migrationBuilder.AddForeignKey(
                name: "FK_StudentAssignmentFiles_StudentAssignments_StudentAssignmentId",
                table: "StudentAssignmentFiles",
                column: "StudentAssignmentId",
                principalTable: "StudentAssignments",
                principalColumn: "Id",
                onDelete: ReferentialAction.NoAction);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_StudentAssignmentFiles_StudentAssignments_StudentAssignmentId",
                table: "StudentAssignmentFiles");

            migrationBuilder.RenameColumn(
                name: "StudentAssignmentId",
                table: "StudentAssignmentFiles",
                newName: "AssignnmentId");

            migrationBuilder.RenameIndex(
                name: "IX_StudentAssignmentFiles_StudentAssignmentId",
                table: "StudentAssignmentFiles",
                newName: "IX_StudentAssignmentFiles_AssignnmentId");

            migrationBuilder.AddColumn<Guid>(
                name: "AssignmentId",
                table: "StudentAssignmentFiles",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.AddForeignKey(
                name: "FK_StudentAssignmentFiles_StudentAssignments_AssignnmentId",
                table: "StudentAssignmentFiles",
                column: "AssignnmentId",
                principalTable: "StudentAssignments",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
