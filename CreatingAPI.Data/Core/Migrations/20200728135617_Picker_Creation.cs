using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace CreatingAPI.Data.Core.Migrations
{
    public partial class Picker_Creation : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_TicTacToe_User_UserId",
                table: "TicTacToe");

            migrationBuilder.AddColumn<int>(
                name: "PickerId",
                table: "Bookmark",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "Picker",
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
                    table.PrimaryKey("PK_Picker", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Picker_User_UserId",
                        column: x => x.UserId,
                        principalTable: "User",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "PickerTopic",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Description = table.Column<int>(maxLength: 150, nullable: false),
                    PickerId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PickerTopic", x => x.Id);
                    table.ForeignKey(
                        name: "FK_PickerTopic_Picker_PickerId",
                        column: x => x.PickerId,
                        principalTable: "Picker",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Bookmark_PickerId",
                table: "Bookmark",
                column: "PickerId");

            migrationBuilder.CreateIndex(
                name: "IX_Picker_UserId",
                table: "Picker",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_PickerTopic_PickerId",
                table: "PickerTopic",
                column: "PickerId");

            migrationBuilder.AddForeignKey(
                name: "FK_Bookmark_Picker_PickerId",
                table: "Bookmark",
                column: "PickerId",
                principalTable: "Picker",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_TicTacToe_User_UserId",
                table: "TicTacToe",
                column: "UserId",
                principalTable: "User",
                principalColumn: "Id");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Bookmark_Picker_PickerId",
                table: "Bookmark");

            migrationBuilder.DropForeignKey(
                name: "FK_TicTacToe_User_UserId",
                table: "TicTacToe");

            migrationBuilder.DropTable(
                name: "PickerTopic");

            migrationBuilder.DropTable(
                name: "Picker");

            migrationBuilder.DropIndex(
                name: "IX_Bookmark_PickerId",
                table: "Bookmark");

            migrationBuilder.DropColumn(
                name: "PickerId",
                table: "Bookmark");

            migrationBuilder.AddForeignKey(
                name: "FK_TicTacToe_User_UserId",
                table: "TicTacToe",
                column: "UserId",
                principalTable: "User",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
