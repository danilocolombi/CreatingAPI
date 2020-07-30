using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace CreatingAPI.Data.Core.Migrations
{
    public partial class Quiz_Table_Creation : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "QuizId",
                table: "Bookmark",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "Quiz",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Title = table.Column<string>(maxLength: 150, nullable: false),
                    CreatedAt = table.Column<DateTime>(nullable: false),
                    IsPublic = table.Column<bool>(nullable: false),
                    UserId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Quiz", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Quiz_User_UserId",
                        column: x => x.UserId,
                        principalTable: "User",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "QuizQuestion",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Description = table.Column<string>(maxLength: 150, nullable: false),
                    AlternativeA = table.Column<string>(maxLength: 150, nullable: true),
                    IsAlternativeACorrect = table.Column<bool>(maxLength: 150, nullable: true),
                    AlternativeB = table.Column<string>(maxLength: 150, nullable: true),
                    IsAlternativeBCorrect = table.Column<bool>(nullable: true),
                    AlternativeC = table.Column<string>(maxLength: 150, nullable: true),
                    IsAlternativeCCorrect = table.Column<bool>(nullable: true),
                    AlternativeD = table.Column<string>(maxLength: 150, nullable: true),
                    IsAlternativeDCorrect = table.Column<bool>(nullable: true),
                    QuizId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_QuizQuestion", x => x.Id);
                    table.ForeignKey(
                        name: "FK_QuizQuestion_Quiz_QuizId",
                        column: x => x.QuizId,
                        principalTable: "Quiz",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Bookmark_QuizId",
                table: "Bookmark",
                column: "QuizId");

            migrationBuilder.CreateIndex(
                name: "IX_Quiz_UserId",
                table: "Quiz",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_QuizQuestion_QuizId",
                table: "QuizQuestion",
                column: "QuizId");

            migrationBuilder.AddForeignKey(
                name: "FK_Bookmark_Quiz_QuizId",
                table: "Bookmark",
                column: "QuizId",
                principalTable: "Quiz",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Bookmark_Quiz_QuizId",
                table: "Bookmark");

            migrationBuilder.DropTable(
                name: "QuizQuestion");

            migrationBuilder.DropTable(
                name: "Quiz");

            migrationBuilder.DropIndex(
                name: "IX_Bookmark_QuizId",
                table: "Bookmark");

            migrationBuilder.DropColumn(
                name: "QuizId",
                table: "Bookmark");
        }
    }
}
