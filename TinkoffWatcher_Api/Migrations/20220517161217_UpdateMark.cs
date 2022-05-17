using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace TinkoffWatcher_Api.Migrations
{
    public partial class UpdateMark : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_CharacteristicValues_CharacteristicType_CharacteristicTypeId",
                table: "CharacteristicValues");

            migrationBuilder.DropPrimaryKey(
                name: "PK_CharacteristicType",
                table: "CharacteristicType");

            migrationBuilder.RenameTable(
                name: "CharacteristicType",
                newName: "CharacteristicTypes");

            migrationBuilder.RenameColumn(
                name: "Review",
                table: "Marks",
                newName: "OverallMark");

            migrationBuilder.AddColumn<string>(
                name: "AdditionalComment",
                table: "Marks",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddPrimaryKey(
                name: "PK_CharacteristicTypes",
                table: "CharacteristicTypes",
                column: "Id");

            migrationBuilder.CreateTable(
                name: "Characteristics",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Other = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CharacteristicTypeId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    CharacteristicValueId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    MarkId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    CreatedDate = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: false),
                    EditedDate = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Characteristics", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Characteristics_CharacteristicTypes_CharacteristicTypeId",
                        column: x => x.CharacteristicTypeId,
                        principalTable: "CharacteristicTypes",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Characteristics_CharacteristicValues_CharacteristicValueId",
                        column: x => x.CharacteristicValueId,
                        principalTable: "CharacteristicValues",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Characteristics_Marks_MarkId",
                        column: x => x.MarkId,
                        principalTable: "Marks",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Characteristics_CharacteristicTypeId",
                table: "Characteristics",
                column: "CharacteristicTypeId");

            migrationBuilder.CreateIndex(
                name: "IX_Characteristics_CharacteristicValueId",
                table: "Characteristics",
                column: "CharacteristicValueId");

            migrationBuilder.CreateIndex(
                name: "IX_Characteristics_MarkId",
                table: "Characteristics",
                column: "MarkId");

            migrationBuilder.AddForeignKey(
                name: "FK_CharacteristicValues_CharacteristicTypes_CharacteristicTypeId",
                table: "CharacteristicValues",
                column: "CharacteristicTypeId",
                principalTable: "CharacteristicTypes",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_CharacteristicValues_CharacteristicTypes_CharacteristicTypeId",
                table: "CharacteristicValues");

            migrationBuilder.DropTable(
                name: "Characteristics");

            migrationBuilder.DropPrimaryKey(
                name: "PK_CharacteristicTypes",
                table: "CharacteristicTypes");

            migrationBuilder.DropColumn(
                name: "AdditionalComment",
                table: "Marks");

            migrationBuilder.RenameTable(
                name: "CharacteristicTypes",
                newName: "CharacteristicType");

            migrationBuilder.RenameColumn(
                name: "OverallMark",
                table: "Marks",
                newName: "Review");

            migrationBuilder.AddPrimaryKey(
                name: "PK_CharacteristicType",
                table: "CharacteristicType",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_CharacteristicValues_CharacteristicType_CharacteristicTypeId",
                table: "CharacteristicValues",
                column: "CharacteristicTypeId",
                principalTable: "CharacteristicType",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
