using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Infrastructure.Migrations
{
    public partial class AddGameRequestToDb : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_GameSetups_AspNetUsers_UserId",
                table: "GameSetups");

            migrationBuilder.DropIndex(
                name: "IX_GameSetups_UserId",
                table: "GameSetups");

            migrationBuilder.DropColumn(
                name: "UserId",
                table: "GameSetups");

            migrationBuilder.DropColumn(
                name: "TotalAttempt",
                table: "AspNetUsers");

            migrationBuilder.RenameColumn(
                name: "AttemptNo",
                table: "GameSetups",
                newName: "GameRequestNo");

            migrationBuilder.AlterColumn<string>(
                name: "ThirdDoor",
                table: "GameSetups",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.AlterColumn<string>(
                name: "SecondDoor",
                table: "GameSetups",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.AlterColumn<string>(
                name: "FirstDoor",
                table: "GameSetups",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.AddColumn<int>(
                name: "GameRequestId",
                table: "GameSetups",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AlterColumn<int>(
                name: "SecondChoice",
                table: "GameResults",
                type: "int",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.AlterColumn<int>(
                name: "FirstChoice",
                table: "GameResults",
                type: "int",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.AlterColumn<string>(
                name: "LastName",
                table: "AspNetUsers",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.AlterColumn<string>(
                name: "FirstName",
                table: "AspNetUsers",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.CreateTable(
                name: "GameRequests",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    UserId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    TotalGameRequests = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_GameRequests", x => x.Id);
                    table.ForeignKey(
                        name: "FK_GameRequests_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_GameSetups_GameRequestId",
                table: "GameSetups",
                column: "GameRequestId");

            migrationBuilder.CreateIndex(
                name: "IX_GameRequests_UserId",
                table: "GameRequests",
                column: "UserId");

            migrationBuilder.AddForeignKey(
                name: "FK_GameSetups_GameRequests_GameRequestId",
                table: "GameSetups",
                column: "GameRequestId",
                principalTable: "GameRequests",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_GameSetups_GameRequests_GameRequestId",
                table: "GameSetups");

            migrationBuilder.DropTable(
                name: "GameRequests");

            migrationBuilder.DropIndex(
                name: "IX_GameSetups_GameRequestId",
                table: "GameSetups");

            migrationBuilder.DropColumn(
                name: "GameRequestId",
                table: "GameSetups");

            migrationBuilder.RenameColumn(
                name: "GameRequestNo",
                table: "GameSetups",
                newName: "AttemptNo");

            migrationBuilder.AlterColumn<string>(
                name: "ThirdDoor",
                table: "GameSetups",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "SecondDoor",
                table: "GameSetups",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "FirstDoor",
                table: "GameSetups",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.AddColumn<Guid>(
                name: "UserId",
                table: "GameSetups",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.AlterColumn<string>(
                name: "SecondChoice",
                table: "GameResults",
                type: "nvarchar(max)",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AlterColumn<string>(
                name: "FirstChoice",
                table: "GameResults",
                type: "nvarchar(max)",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AlterColumn<string>(
                name: "LastName",
                table: "AspNetUsers",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "FirstName",
                table: "AspNetUsers",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.AddColumn<int>(
                name: "TotalAttempt",
                table: "AspNetUsers",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_GameSetups_UserId",
                table: "GameSetups",
                column: "UserId");

            migrationBuilder.AddForeignKey(
                name: "FK_GameSetups_AspNetUsers_UserId",
                table: "GameSetups",
                column: "UserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
