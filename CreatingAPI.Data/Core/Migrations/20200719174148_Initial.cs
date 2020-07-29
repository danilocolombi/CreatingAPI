using Microsoft.EntityFrameworkCore.Migrations;
using System;

namespace CreatingAPI.Data.Core.Migrations
{
    public partial class Initial : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "User",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(maxLength: 150, nullable: false),
                    Email = table.Column<string>(maxLength: 150, nullable: true),
                    Password = table.Column<string>(maxLength: 150, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_User", x => x.Id);
                });

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
                name: "Unscramble",
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
                    table.PrimaryKey("PK_Unscramble", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Unscramble_User_UserId",
                        column: x => x.UserId,
                        principalTable: "User",
                        principalColumn: "Id");
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

            migrationBuilder.CreateTable(
                name: "Bookmark",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    UserId = table.Column<int>(nullable: false),
                    UnscrambleId = table.Column<int>(nullable: true),
                    TicTacToeId = table.Column<int>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Bookmark", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Bookmark_TicTacToe_TicTacToeId",
                        column: x => x.TicTacToeId,
                        principalTable: "TicTacToe",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Bookmark_Unscramble_UnscrambleId",
                        column: x => x.UnscrambleId,
                        principalTable: "Unscramble",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Bookmark_User_UserId",
                        column: x => x.UserId,
                        principalTable: "User",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "Exercise",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Description = table.Column<string>(maxLength: 150, nullable: false),
                    Position = table.Column<byte>(type: "TINYINT", nullable: false),
                    UnscrambleId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Exercise", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Exercise_Unscramble_UnscrambleId",
                        column: x => x.UnscrambleId,
                        principalTable: "Unscramble",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Game",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    StartedAt = table.Column<DateTime>(nullable: false),
                    EndedAt = table.Column<DateTime>(nullable: false),
                    NumberOfCorrectAnswers = table.Column<byte>(type: "TINYINT", nullable: false),
                    NumberOfWrongAnswers = table.Column<byte>(type: "TINYINT", nullable: false),
                    UserId = table.Column<int>(nullable: false),
                    UnscrambleId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Game", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Game_Unscramble_UnscrambleId",
                        column: x => x.UnscrambleId,
                        principalTable: "Unscramble",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Game_User_UserId",
                        column: x => x.UserId,
                        principalTable: "User",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateIndex(
                name: "IX_Bookmark_TicTacToeId",
                table: "Bookmark",
                column: "TicTacToeId");

            migrationBuilder.CreateIndex(
                name: "IX_Bookmark_UnscrambleId",
                table: "Bookmark",
                column: "UnscrambleId");

            migrationBuilder.CreateIndex(
                name: "IX_Bookmark_UserId",
                table: "Bookmark",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_Exercise_UnscrambleId",
                table: "Exercise",
                column: "UnscrambleId");

            migrationBuilder.CreateIndex(
                name: "IX_Game_UnscrambleId",
                table: "Game",
                column: "UnscrambleId");

            migrationBuilder.CreateIndex(
                name: "IX_Game_UserId",
                table: "Game",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_TicTacToe_UserId",
                table: "TicTacToe",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_TicTacToeSquare_TicTacToeId",
                table: "TicTacToeSquare",
                column: "TicTacToeId");

            migrationBuilder.CreateIndex(
                name: "IX_Unscramble_UserId",
                table: "Unscramble",
                column: "UserId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Bookmark");

            migrationBuilder.DropTable(
                name: "Exercise");

            migrationBuilder.DropTable(
                name: "Game");

            migrationBuilder.DropTable(
                name: "TicTacToeSquare");

            migrationBuilder.DropTable(
                name: "Unscramble");

            migrationBuilder.DropTable(
                name: "TicTacToe");

            migrationBuilder.DropTable(
                name: "User");
        }
    }
}
