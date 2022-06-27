using Microsoft.EntityFrameworkCore.Migrations;

namespace TinkoffWatcher_Api.Migrations
{
    public partial class StudentGroupsAdded : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Grade",
                table: "AspNetUsers");

            migrationBuilder.AddColumn<int>(
                name: "Group",
                table: "AspNetUsers",
                type: "int",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Group",
                table: "AspNetUsers");

            migrationBuilder.AddColumn<int>(
                name: "Grade",
                table: "AspNetUsers",
                type: "int",
                nullable: true);
        }
    }
}
