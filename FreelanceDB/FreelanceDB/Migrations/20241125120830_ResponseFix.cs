using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace FreelanceDB.Migrations
{
    /// <inheritdoc />
    public partial class ResponseFix : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "UserId",
                table: "Responses",
                newName: "UserID");

            migrationBuilder.RenameColumn(
                name: "TaskId",
                table: "Responses",
                newName: "TaskID");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "UserID",
                table: "Responses",
                newName: "UserId");

            migrationBuilder.RenameColumn(
                name: "TaskID",
                table: "Responses",
                newName: "TaskId");
        }
    }
}
