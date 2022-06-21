using Microsoft.EntityFrameworkCore.Migrations;

namespace TinkoffWatcher_Api.Migrations
{
    public partial class FixUserWithMarkDelete : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_AspNetUsers_Companies_CompanyId",
                table: "AspNetUsers");

            migrationBuilder.DropForeignKey(
                name: "FK_CharacteristicAnswers_CharacteristicQuestions_CharacteristicQuestionId",
                table: "CharacteristicAnswers");

            migrationBuilder.DropForeignKey(
                name: "FK_Marks_AspNetUsers_AgentId",
                table: "Marks");

            migrationBuilder.DropForeignKey(
                name: "FK_Marks_AspNetUsers_StudentId",
                table: "Marks");

            migrationBuilder.AddForeignKey(
                name: "FK_AspNetUsers_Companies_CompanyId",
                table: "AspNetUsers",
                column: "CompanyId",
                principalTable: "Companies",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_CharacteristicAnswers_CharacteristicQuestions_CharacteristicQuestionId",
                table: "CharacteristicAnswers",
                column: "CharacteristicQuestionId",
                principalTable: "CharacteristicQuestions",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Marks_AspNetUsers_AgentId",
                table: "Marks",
                column: "AgentId",
                principalTable: "AspNetUsers",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Marks_AspNetUsers_StudentId",
                table: "Marks",
                column: "StudentId",
                principalTable: "AspNetUsers",
                principalColumn: "Id");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_AspNetUsers_Companies_CompanyId",
                table: "AspNetUsers");

            migrationBuilder.DropForeignKey(
                name: "FK_CharacteristicAnswers_CharacteristicQuestions_CharacteristicQuestionId",
                table: "CharacteristicAnswers");

            migrationBuilder.DropForeignKey(
                name: "FK_Marks_AspNetUsers_AgentId",
                table: "Marks");

            migrationBuilder.DropForeignKey(
                name: "FK_Marks_AspNetUsers_StudentId",
                table: "Marks");

            migrationBuilder.AddForeignKey(
                name: "FK_AspNetUsers_Companies_CompanyId",
                table: "AspNetUsers",
                column: "CompanyId",
                principalTable: "Companies",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_CharacteristicAnswers_CharacteristicQuestions_CharacteristicQuestionId",
                table: "CharacteristicAnswers",
                column: "CharacteristicQuestionId",
                principalTable: "CharacteristicQuestions",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Marks_AspNetUsers_AgentId",
                table: "Marks",
                column: "AgentId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Marks_AspNetUsers_StudentId",
                table: "Marks",
                column: "StudentId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
