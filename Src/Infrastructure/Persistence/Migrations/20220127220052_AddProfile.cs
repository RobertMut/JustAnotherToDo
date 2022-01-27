using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace JustAnotherToDo.Persistence.Migrations
{
    public partial class AddProfile : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<Guid>(
                name: "UserProfileUserId",
                table: "ToDos",
                type: "uniqueidentifier",
                nullable: true);

            migrationBuilder.AddColumn<Guid>(
                name: "UserProfileUserId",
                table: "Categories",
                type: "uniqueidentifier",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_ToDos_UserProfileUserId",
                table: "ToDos",
                column: "UserProfileUserId");

            migrationBuilder.CreateIndex(
                name: "IX_Categories_ProfileId",
                table: "Categories",
                column: "ProfileId");

            migrationBuilder.CreateIndex(
                name: "IX_Categories_UserProfileUserId",
                table: "Categories",
                column: "UserProfileUserId");

            migrationBuilder.AddForeignKey(
                name: "FK_Categories_Profiles_ProfileId",
                table: "Categories",
                column: "ProfileId",
                principalTable: "Profiles",
                principalColumn: "UserId",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Categories_Profiles_UserProfileUserId",
                table: "Categories",
                column: "UserProfileUserId",
                principalTable: "Profiles",
                principalColumn: "UserId");

            migrationBuilder.AddForeignKey(
                name: "FK_ToDos_Profiles_UserProfileUserId",
                table: "ToDos",
                column: "UserProfileUserId",
                principalTable: "Profiles",
                principalColumn: "UserId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Categories_Profiles_ProfileId",
                table: "Categories");

            migrationBuilder.DropForeignKey(
                name: "FK_Categories_Profiles_UserProfileUserId",
                table: "Categories");

            migrationBuilder.DropForeignKey(
                name: "FK_ToDos_Profiles_UserProfileUserId",
                table: "ToDos");

            migrationBuilder.DropIndex(
                name: "IX_ToDos_UserProfileUserId",
                table: "ToDos");

            migrationBuilder.DropIndex(
                name: "IX_Categories_ProfileId",
                table: "Categories");

            migrationBuilder.DropIndex(
                name: "IX_Categories_UserProfileUserId",
                table: "Categories");

            migrationBuilder.DropColumn(
                name: "UserProfileUserId",
                table: "ToDos");

            migrationBuilder.DropColumn(
                name: "UserProfileUserId",
                table: "Categories");
        }
    }
}
