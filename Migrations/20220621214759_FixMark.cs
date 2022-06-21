using Microsoft.EntityFrameworkCore.Migrations;

namespace TinkoffWatcher_Api.Migrations
{
    public partial class FixMark : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Marks_AspNetUsers_StudentId",
                table: "Marks");

            migrationBuilder.AddForeignKey(
                name: "FK_Marks_AspNetUsers_StudentId",
                table: "Marks",
                column: "StudentId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Marks_AspNetUsers_StudentId",
                table: "Marks");

            migrationBuilder.AddForeignKey(
                name: "FK_Marks_AspNetUsers_StudentId",
                table: "Marks",
                column: "StudentId",
                principalTable: "AspNetUsers",
                principalColumn: "Id");
        }
    }
}
