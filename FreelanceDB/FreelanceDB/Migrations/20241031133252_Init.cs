using System;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace FreelanceDB.Migrations
{
    /// <inheritdoc />
    public partial class Init : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Resume",
                columns: table => new
                {
                    ID = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Head = table.Column<string>(type: "text", nullable: false),
                    WorkExp = table.Column<string>(type: "text", nullable: true),
                    Skills = table.Column<string>(type: "text", nullable: true),
                    Education = table.Column<string>(type: "text", nullable: true),
                    AboutMe = table.Column<string>(type: "text", nullable: true),
                    Contacts = table.Column<string>(type: "text", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("Resume_pkey", x => x.ID);
                });

            migrationBuilder.CreateTable(
                name: "Status",
                columns: table => new
                {
                    ID = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    StatusName = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("Status_pkey", x => x.ID);
                });

            migrationBuilder.CreateTable(
                name: "User",
                columns: table => new
                {
                    ID = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Login = table.Column<string>(type: "text", nullable: false),
                    PasswordHash = table.Column<string>(type: "text", nullable: false),
                    Nickname = table.Column<string>(type: "text", nullable: false),
                    aToken = table.Column<string>(type: "text", nullable: true),
                    rToken = table.Column<string>(type: "text", nullable: true),
                    Balance = table.Column<int>(type: "integer", nullable: false, defaultValue: 0),
                    FreezeBalance = table.Column<int>(type: "integer", nullable: false, defaultValue: 0)
                },
                constraints: table =>
                {
                    table.PrimaryKey("User_pkey", x => x.ID);
                });

            migrationBuilder.CreateTable(
                name: "Review",
                columns: table => new
                {
                    ID = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Description = table.Column<string>(type: "text", nullable: false),
                    Rate = table.Column<int>(type: "integer", nullable: false),
                    AuthorID = table.Column<long>(type: "bigint", nullable: false),
                    RecipientID = table.Column<long>(type: "bigint", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("Review_pkey", x => x.ID);
                    table.ForeignKey(
                        name: "Review_AuthorID_fkey",
                        column: x => x.AuthorID,
                        principalTable: "User",
                        principalColumn: "ID");
                    table.ForeignKey(
                        name: "Review_RecipientID_fkey",
                        column: x => x.RecipientID,
                        principalTable: "User",
                        principalColumn: "ID");
                });

            migrationBuilder.CreateTable(
                name: "Task",
                columns: table => new
                {
                    ID = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Head = table.Column<string>(type: "text", nullable: false),
                    Deadline = table.Column<DateOnly>(type: "date", nullable: false),
                    EndDate = table.Column<DateOnly>(type: "date", nullable: true),
                    Price = table.Column<int>(type: "integer", nullable: false),
                    Description = table.Column<string>(type: "text", nullable: false),
                    Tag = table.Column<string>(type: "text", nullable: false),
                    AuthorID = table.Column<long>(type: "bigint", nullable: false),
                    ExecutorID = table.Column<long>(type: "bigint", nullable: true),
                    StatusID = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("Task_pkey", x => x.ID);
                    table.ForeignKey(
                        name: "Task_AuthorID_fkey",
                        column: x => x.AuthorID,
                        principalTable: "User",
                        principalColumn: "ID");
                    table.ForeignKey(
                        name: "Task_ExecutorID_fkey",
                        column: x => x.ExecutorID,
                        principalTable: "User",
                        principalColumn: "ID");
                    table.ForeignKey(
                        name: "Task_StatusID_fkey",
                        column: x => x.StatusID,
                        principalTable: "Status",
                        principalColumn: "ID");
                });

            migrationBuilder.CreateTable(
                name: "UserResume",
                columns: table => new
                {
                    ID = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    UserID = table.Column<long>(type: "bigint", nullable: false),
                    ResumeID = table.Column<long>(type: "bigint", nullable: false)
                },
                constraints: table =>
                {
                    table.ForeignKey(
                        name: "UserResume_ResumeID_fkey",
                        column: x => x.ResumeID,
                        principalTable: "Resume",
                        principalColumn: "ID");
                    table.ForeignKey(
                        name: "UserResume_UserID_fkey",
                        column: x => x.UserID,
                        principalTable: "User",
                        principalColumn: "ID");
                });

            migrationBuilder.CreateTable(
                name: "Responses",
                columns: table => new
                {
                    ID = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    TaskID = table.Column<long>(type: "bigint", nullable: false),
                    UserID = table.Column<long>(type: "bigint", nullable: false)
                },
                constraints: table =>
                {
                    table.ForeignKey(
                        name: "Responses_TaskID_fkey",
                        column: x => x.TaskID,
                        principalTable: "Task",
                        principalColumn: "ID");
                    table.ForeignKey(
                        name: "Responses_UserID_fkey",
                        column: x => x.UserID,
                        principalTable: "User",
                        principalColumn: "ID");
                });

            migrationBuilder.CreateIndex(
                name: "IX_Responses_TaskID",
                table: "Responses",
                column: "TaskID");

            migrationBuilder.CreateIndex(
                name: "IX_Responses_UserID",
                table: "Responses",
                column: "UserID");

            migrationBuilder.CreateIndex(
                name: "IX_Review_AuthorID",
                table: "Review",
                column: "AuthorID");

            migrationBuilder.CreateIndex(
                name: "IX_Review_RecipientID",
                table: "Review",
                column: "RecipientID");

            migrationBuilder.CreateIndex(
                name: "IX_Task_AuthorID",
                table: "Task",
                column: "AuthorID");

            migrationBuilder.CreateIndex(
                name: "IX_Task_ExecutorID",
                table: "Task",
                column: "ExecutorID");

            migrationBuilder.CreateIndex(
                name: "IX_Task_StatusID",
                table: "Task",
                column: "StatusID");

            migrationBuilder.CreateIndex(
                name: "IX_UserResume_ResumeID",
                table: "UserResume",
                column: "ResumeID");

            migrationBuilder.CreateIndex(
                name: "IX_UserResume_UserID",
                table: "UserResume",
                column: "UserID");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Responses");

            migrationBuilder.DropTable(
                name: "Review");

            migrationBuilder.DropTable(
                name: "UserResume");

            migrationBuilder.DropTable(
                name: "Task");

            migrationBuilder.DropTable(
                name: "Resume");

            migrationBuilder.DropTable(
                name: "User");

            migrationBuilder.DropTable(
                name: "Status");
        }
    }
}
