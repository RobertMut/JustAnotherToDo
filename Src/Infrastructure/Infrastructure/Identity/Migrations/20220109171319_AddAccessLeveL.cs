using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace JustAnotherToDo.Infrastructure.Identity.Migrations
{
    public partial class AddAccessLeveL : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "AccessLevel",
                table: "Profiles",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "AccessLevel",
                table: "Profiles");
        }
    }
}
