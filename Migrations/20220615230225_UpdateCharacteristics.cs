using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace TinkoffWatcher_Api.Migrations
{
    public partial class UpdateCharacteristics : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Characteristics_CharacteristicTypes_CharacteristicTypeId",
                table: "Characteristics");

            migrationBuilder.DropForeignKey(
                name: "FK_Characteristics_CharacteristicValues_CharacteristicValueId",
                table: "Characteristics");

            migrationBuilder.DropTable(
                name: "CharacteristicValues");

            migrationBuilder.DropTable(
                name: "CharacteristicTypes");

            migrationBuilder.DropIndex(
                name: "IX_Characteristics_CharacteristicTypeId",
                table: "Characteristics");

            migrationBuilder.DropColumn(
                name: "CharacteristicTypeId",
                table: "Characteristics");

            migrationBuilder.RenameColumn(
                name: "CharacteristicValueId",
                table: "Characteristics",
                newName: "CharacteristicQuestionId");

            migrationBuilder.RenameIndex(
                name: "IX_Characteristics_CharacteristicValueId",
                table: "Characteristics",
                newName: "IX_Characteristics_CharacteristicQuestionId");

            migrationBuilder.CreateTable(
                name: "CharacteristicQuestions",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    IsMultipleValues = table.Column<bool>(type: "bit", nullable: false),
                    CreatedDate = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: false),
                    EditedDate = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: true),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false),
                    Version = table.Column<int>(type: "int", nullable: false),
                    IsCurrent = table.Column<bool>(type: "bit", nullable: false),
                    VersionDate = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CharacteristicQuestions", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "CharacteristicAnswers",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CharacteristicQuestionId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    CreatedDate = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: false),
                    EditedDate = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: true),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false),
                    Version = table.Column<int>(type: "int", nullable: false),
                    IsCurrent = table.Column<bool>(type: "bit", nullable: false),
                    VersionDate = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CharacteristicAnswers", x => x.Id);
                    table.ForeignKey(
                        name: "FK_CharacteristicAnswers_CharacteristicQuestions_CharacteristicQuestionId",
                        column: x => x.CharacteristicQuestionId,
                        principalTable: "CharacteristicQuestions",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "CharacteristicCharacteristicAnswer",
                columns: table => new
                {
                    CharacteristicAnswersId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    CharacteristicsId = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CharacteristicCharacteristicAnswer", x => new { x.CharacteristicAnswersId, x.CharacteristicsId });
                    table.ForeignKey(
                        name: "FK_CharacteristicCharacteristicAnswer_CharacteristicAnswers_CharacteristicAnswersId",
                        column: x => x.CharacteristicAnswersId,
                        principalTable: "CharacteristicAnswers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_CharacteristicCharacteristicAnswer_Characteristics_CharacteristicsId",
                        column: x => x.CharacteristicsId,
                        principalTable: "Characteristics",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_CharacteristicAnswers_CharacteristicQuestionId",
                table: "CharacteristicAnswers",
                column: "CharacteristicQuestionId");

            migrationBuilder.CreateIndex(
                name: "IX_CharacteristicCharacteristicAnswer_CharacteristicsId",
                table: "CharacteristicCharacteristicAnswer",
                column: "CharacteristicsId");

            migrationBuilder.AddForeignKey(
                name: "FK_Characteristics_CharacteristicQuestions_CharacteristicQuestionId",
                table: "Characteristics",
                column: "CharacteristicQuestionId",
                principalTable: "CharacteristicQuestions",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Characteristics_CharacteristicQuestions_CharacteristicQuestionId",
                table: "Characteristics");

            migrationBuilder.DropTable(
                name: "CharacteristicCharacteristicAnswer");

            migrationBuilder.DropTable(
                name: "CharacteristicAnswers");

            migrationBuilder.DropTable(
                name: "CharacteristicQuestions");

            migrationBuilder.RenameColumn(
                name: "CharacteristicQuestionId",
                table: "Characteristics",
                newName: "CharacteristicValueId");

            migrationBuilder.RenameIndex(
                name: "IX_Characteristics_CharacteristicQuestionId",
                table: "Characteristics",
                newName: "IX_Characteristics_CharacteristicValueId");

            migrationBuilder.AddColumn<Guid>(
                name: "CharacteristicTypeId",
                table: "Characteristics",
                type: "uniqueidentifier",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "CharacteristicTypes",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    CreatedDate = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: false),
                    EditedDate = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: true),
                    IsCurrent = table.Column<bool>(type: "bit", nullable: false),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Version = table.Column<int>(type: "int", nullable: false),
                    VersionDate = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CharacteristicTypes", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "CharacteristicValues",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    CharacteristicTypeId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    CreatedDate = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: false),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Discriminator = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    EditedDate = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: true),
                    IsCurrent = table.Column<bool>(type: "bit", nullable: false),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false),
                    Version = table.Column<int>(type: "int", nullable: false),
                    VersionDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    BoolValue = table.Column<bool>(type: "bit", nullable: true),
                    IntValue = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CharacteristicValues", x => x.Id);
                    table.ForeignKey(
                        name: "FK_CharacteristicValues_CharacteristicTypes_CharacteristicTypeId",
                        column: x => x.CharacteristicTypeId,
                        principalTable: "CharacteristicTypes",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Characteristics_CharacteristicTypeId",
                table: "Characteristics",
                column: "CharacteristicTypeId");

            migrationBuilder.CreateIndex(
                name: "IX_CharacteristicValues_CharacteristicTypeId",
                table: "CharacteristicValues",
                column: "CharacteristicTypeId");

            migrationBuilder.AddForeignKey(
                name: "FK_Characteristics_CharacteristicTypes_CharacteristicTypeId",
                table: "Characteristics",
                column: "CharacteristicTypeId",
                principalTable: "CharacteristicTypes",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Characteristics_CharacteristicValues_CharacteristicValueId",
                table: "Characteristics",
                column: "CharacteristicValueId",
                principalTable: "CharacteristicValues",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
