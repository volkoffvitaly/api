using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace TinkoffWatcher_Api.Migrations
{
    public partial class addCharacteristicType : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<Guid>(
                name: "CharacteristicTypeId",
                table: "CharacteristicValues",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.CreateTable(
                name: "CharacteristicType",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CreatedDate = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: false),
                    EditedDate = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: true),
                    Version = table.Column<int>(type: "int", nullable: false),
                    IsCurrent = table.Column<bool>(type: "bit", nullable: false),
                    VersionDate = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CharacteristicType", x => x.Id);
                });

            migrationBuilder.CreateIndex(
                name: "IX_CharacteristicValues_CharacteristicTypeId",
                table: "CharacteristicValues",
                column: "CharacteristicTypeId");

            migrationBuilder.AddForeignKey(
                name: "FK_CharacteristicValues_CharacteristicType_CharacteristicTypeId",
                table: "CharacteristicValues",
                column: "CharacteristicTypeId",
                principalTable: "CharacteristicType",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_CharacteristicValues_CharacteristicType_CharacteristicTypeId",
                table: "CharacteristicValues");

            migrationBuilder.DropTable(
                name: "CharacteristicType");

            migrationBuilder.DropIndex(
                name: "IX_CharacteristicValues_CharacteristicTypeId",
                table: "CharacteristicValues");

            migrationBuilder.DropColumn(
                name: "CharacteristicTypeId",
                table: "CharacteristicValues");
        }
    }
}
