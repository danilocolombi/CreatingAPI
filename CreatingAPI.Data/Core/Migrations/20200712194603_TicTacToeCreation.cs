using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace CreatingAPI.Data.Core.Migrations
{
    public partial class TicTacToeCreation : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "TicTacToeId",
                table: "Bookmark",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateTable(
                name: "TicTacToe",
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
                    table.PrimaryKey("PK_TicTacToe", x => x.Id);
                    table.ForeignKey(
                        name: "FK_TicTacToe_User_UserId",
                        column: x => x.UserId,
                        principalTable: "User",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "TicTacToeSquare",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Description = table.Column<string>(maxLength: 150, nullable: false),
                    Position = table.Column<byte>(type: "TINYINT", nullable: false),
                    TicTacToeId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TicTacToeSquare", x => x.Id);
                    table.ForeignKey(
                        name: "FK_TicTacToeSquare_TicTacToe_TicTacToeId",
                        column: x => x.TicTacToeId,
                        principalTable: "TicTacToe",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Bookmark_TicTacToeId",
                table: "Bookmark",
                column: "TicTacToeId");

            migrationBuilder.CreateIndex(
                name: "IX_TicTacToe_UserId",
                table: "TicTacToe",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_TicTacToeSquare_TicTacToeId",
                table: "TicTacToeSquare",
                column: "TicTacToeId");

            migrationBuilder.AddForeignKey(
                name: "FK_Bookmark_TicTacToe_TicTacToeId",
                table: "Bookmark",
                column: "TicTacToeId",
                principalTable: "TicTacToe",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Bookmark_TicTacToe_TicTacToeId",
                table: "Bookmark");

            migrationBuilder.DropTable(
                name: "TicTacToeSquare");

            migrationBuilder.DropTable(
                name: "TicTacToe");

            migrationBuilder.DropIndex(
                name: "IX_Bookmark_TicTacToeId",
                table: "Bookmark");

            migrationBuilder.DropColumn(
                name: "TicTacToeId",
                table: "Bookmark");
        }
    }
}
