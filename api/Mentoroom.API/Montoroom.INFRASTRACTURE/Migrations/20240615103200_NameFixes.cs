using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Mentoroom.INFRASTRACTURE.Migrations
{
    /// <inheritdoc />
    public partial class NameFixes : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_StudentAssignmentFiles_AssignmentFiles_AssigmentFileId",
                table: "StudentAssignmentFiles");

            migrationBuilder.DropForeignKey(
                name: "FK_StudentAssignmentFiles_StudentAssignments_AssignmentId",
                table: "StudentAssignmentFiles");

            migrationBuilder.DropIndex(
                name: "IX_StudentAssignmentFiles_AssignmentId",
                table: "StudentAssignmentFiles");

            migrationBuilder.RenameColumn(
                name: "AssigmentFileId",
                table: "StudentAssignmentFiles",
                newName: "AssignnmentId");

            migrationBuilder.RenameIndex(
                name: "IX_StudentAssignmentFiles_AssigmentFileId",
                table: "StudentAssignmentFiles",
                newName: "IX_StudentAssignmentFiles_AssignnmentId");

            migrationBuilder.AddColumn<Guid>(
                name: "AssignmentFileId",
                table: "StudentAssignmentFiles",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.CreateIndex(
                name: "IX_StudentAssignmentFiles_AssignmentFileId",
                table: "StudentAssignmentFiles",
                column: "AssignmentFileId");

            migrationBuilder.AddForeignKey(
                name: "FK_StudentAssignmentFiles_AssignmentFiles_AssignmentFileId",
                table: "StudentAssignmentFiles",
                column: "AssignmentFileId",
                principalTable: "AssignmentFiles",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_StudentAssignmentFiles_StudentAssignments_AssignnmentId",
                table: "StudentAssignmentFiles",
                column: "AssignnmentId",
                principalTable: "StudentAssignments",
                principalColumn: "Id",
                onDelete: ReferentialAction.NoAction);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_StudentAssignmentFiles_AssignmentFiles_AssignmentFileId",
                table: "StudentAssignmentFiles");

            migrationBuilder.DropForeignKey(
                name: "FK_StudentAssignmentFiles_StudentAssignments_AssignnmentId",
                table: "StudentAssignmentFiles");

            migrationBuilder.DropIndex(
                name: "IX_StudentAssignmentFiles_AssignmentFileId",
                table: "StudentAssignmentFiles");

            migrationBuilder.DropColumn(
                name: "AssignmentFileId",
                table: "StudentAssignmentFiles");

            migrationBuilder.RenameColumn(
                name: "AssignnmentId",
                table: "StudentAssignmentFiles",
                newName: "AssigmentFileId");

            migrationBuilder.RenameIndex(
                name: "IX_StudentAssignmentFiles_AssignnmentId",
                table: "StudentAssignmentFiles",
                newName: "IX_StudentAssignmentFiles_AssigmentFileId");

            migrationBuilder.CreateIndex(
                name: "IX_StudentAssignmentFiles_AssignmentId",
                table: "StudentAssignmentFiles",
                column: "AssignmentId");

            migrationBuilder.AddForeignKey(
                name: "FK_StudentAssignmentFiles_AssignmentFiles_AssigmentFileId",
                table: "StudentAssignmentFiles",
                column: "AssigmentFileId",
                principalTable: "AssignmentFiles",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_StudentAssignmentFiles_StudentAssignments_AssignmentId",
                table: "StudentAssignmentFiles",
                column: "AssignmentId",
                principalTable: "StudentAssignments",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
