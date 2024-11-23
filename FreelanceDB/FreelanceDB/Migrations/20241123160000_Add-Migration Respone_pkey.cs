using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace FreelanceDB.Migrations
{
    /// <inheritdoc />
    public partial class AddMigrationRespone_pkey : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddPrimaryKey(
                name: "Responses_pkey",
                table: "Responses",
                column: "ID");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "Responses_pkey",
                table: "Responses");
        }
    }
}
