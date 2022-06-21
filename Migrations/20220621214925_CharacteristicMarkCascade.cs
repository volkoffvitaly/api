using Microsoft.EntityFrameworkCore.Migrations;

namespace TinkoffWatcher_Api.Migrations
{
    public partial class CharacteristicMarkCascade : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Characteristics_Marks_MarkId",
                table: "Characteristics");

            migrationBuilder.AddForeignKey(
                name: "FK_Characteristics_Marks_MarkId",
                table: "Characteristics",
                column: "MarkId",
                principalTable: "Marks",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Characteristics_Marks_MarkId",
                table: "Characteristics");

            migrationBuilder.AddForeignKey(
                name: "FK_Characteristics_Marks_MarkId",
                table: "Characteristics",
                column: "MarkId",
                principalTable: "Marks",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
