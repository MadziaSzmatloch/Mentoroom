using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Mentoroom.INFRASTRACTURE.Migrations
{
    /// <inheritdoc />
    public partial class studentassignmentfiles : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "StudentAssignmentFiles",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    IsSended = table.Column<bool>(type: "bit", nullable: false),
                    AssignmentId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    AssigmentFileId = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_StudentAssignmentFiles", x => x.Id);
                    table.ForeignKey(
                        name: "FK_StudentAssignmentFiles_AssignmentFiles_AssigmentFileId",
                        column: x => x.AssigmentFileId,
                        principalTable: "AssignmentFiles",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_StudentAssignmentFiles_StudentAssignments_AssignmentId",
                        column: x => x.AssignmentId,
                        principalTable: "StudentAssignments",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.NoAction);
                });

            migrationBuilder.CreateIndex(
                name: "IX_StudentAssignmentFiles_AssigmentFileId",
                table: "StudentAssignmentFiles",
                column: "AssigmentFileId");

            migrationBuilder.CreateIndex(
                name: "IX_StudentAssignmentFiles_AssignmentId",
                table: "StudentAssignmentFiles",
                column: "AssignmentId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "StudentAssignmentFiles");
        }
    }
}
