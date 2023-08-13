using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Infrastructure.Migrations
{
    public partial class AddEntityRelationship : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_GameResults_GameSetupId",
                table: "GameResults");

            migrationBuilder.CreateIndex(
                name: "IX_GameResults_GameSetupId",
                table: "GameResults",
                column: "GameSetupId",
                unique: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_GameResults_GameSetupId",
                table: "GameResults");

            migrationBuilder.CreateIndex(
                name: "IX_GameResults_GameSetupId",
                table: "GameResults",
                column: "GameSetupId");
        }
    }
}
