using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace TinkoffWatcher_Api.Migrations
{
    public partial class UpdateMark : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Review",
                table: "Marks",
                newName: "OverallMark");

            migrationBuilder.AddColumn<string>(
                name: "AdditionalComment",
                table: "Marks",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "CharacteristicTypes",
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
                    table.PrimaryKey("PK_CharacteristicTypes", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "SubscriberToCompanies",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    SubscriberId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    CompanyId = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SubscriberToCompanies", x => x.Id);
                    table.ForeignKey(
                        name: "FK_SubscriberToCompanies_AspNetUsers_SubscriberId",
                        column: x => x.SubscriberId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_SubscriberToCompanies_Companies_CompanyId",
                        column: x => x.CompanyId,
                        principalTable: "Companies",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "CharacteristicValues",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CharacteristicTypeId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Discriminator = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    BoolValue = table.Column<bool>(type: "bit", nullable: true),
                    IntValue = table.Column<int>(type: "int", nullable: true),
                    CreatedDate = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: false),
                    EditedDate = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: true),
                    Version = table.Column<int>(type: "int", nullable: false),
                    IsCurrent = table.Column<bool>(type: "bit", nullable: false),
                    VersionDate = table.Column<DateTime>(type: "datetime2", nullable: false)
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

            migrationBuilder.CreateIndex(
                name: "IX_CharacteristicValues_CharacteristicTypeId",
                table: "CharacteristicValues",
                column: "CharacteristicTypeId");

            migrationBuilder.CreateIndex(
                name: "IX_SubscriberToCompanies_CompanyId",
                table: "SubscriberToCompanies",
                column: "CompanyId");

            migrationBuilder.CreateIndex(
                name: "IX_SubscriberToCompanies_SubscriberId",
                table: "SubscriberToCompanies",
                column: "SubscriberId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Characteristics");

            migrationBuilder.DropTable(
                name: "SubscriberToCompanies");

            migrationBuilder.DropTable(
                name: "CharacteristicValues");

            migrationBuilder.DropTable(
                name: "CharacteristicTypes");

            migrationBuilder.DropColumn(
                name: "AdditionalComment",
                table: "Marks");

            migrationBuilder.RenameColumn(
                name: "OverallMark",
                table: "Marks",
                newName: "Review");
        }
    }
}
