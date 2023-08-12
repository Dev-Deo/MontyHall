using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Infrastructure.Migrations
{
    public partial class AddGameRelatedTablesToDb : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "GameSetups",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    UserId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    AttemptNo = table.Column<int>(type: "int", nullable: false),
                    FirstDoor = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    SecondDoor = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ThirdDoor = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_GameSetups", x => x.Id);
                    table.ForeignKey(
                        name: "FK_GameSetups_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "GameResults",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    GameSetupId = table.Column<int>(type: "int", nullable: false),
                    FirstChoice = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    SecondChoice = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    IsWin = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_GameResults", x => x.Id);
                    table.ForeignKey(
                        name: "FK_GameResults_GameSetups_GameSetupId",
                        column: x => x.GameSetupId,
                        principalTable: "GameSetups",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_GameResults_GameSetupId",
                table: "GameResults",
                column: "GameSetupId");

            migrationBuilder.CreateIndex(
                name: "IX_GameSetups_UserId",
                table: "GameSetups",
                column: "UserId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "GameResults");

            migrationBuilder.DropTable(
                name: "GameSetups");
        }
    }
}
