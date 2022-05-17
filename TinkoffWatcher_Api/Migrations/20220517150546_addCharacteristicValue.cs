using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace TinkoffWatcher_Api.Migrations
{
    public partial class addCharacteristicValue : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_SubscriberToCompany_AspNetUsers_SubscriberId",
                table: "SubscriberToCompany");

            migrationBuilder.DropForeignKey(
                name: "FK_SubscriberToCompany_Companies_CompanyId",
                table: "SubscriberToCompany");

            migrationBuilder.DropPrimaryKey(
                name: "PK_SubscriberToCompany",
                table: "SubscriberToCompany");

            migrationBuilder.RenameTable(
                name: "SubscriberToCompany",
                newName: "SubscriberToCompanies");

            migrationBuilder.RenameIndex(
                name: "IX_SubscriberToCompany_SubscriberId",
                table: "SubscriberToCompanies",
                newName: "IX_SubscriberToCompanies_SubscriberId");

            migrationBuilder.RenameIndex(
                name: "IX_SubscriberToCompany_CompanyId",
                table: "SubscriberToCompanies",
                newName: "IX_SubscriberToCompanies_CompanyId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_SubscriberToCompanies",
                table: "SubscriberToCompanies",
                column: "Id");

            migrationBuilder.CreateTable(
                name: "CharacteristicValues",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: true),
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
                });

            migrationBuilder.AddForeignKey(
                name: "FK_SubscriberToCompanies_AspNetUsers_SubscriberId",
                table: "SubscriberToCompanies",
                column: "SubscriberId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_SubscriberToCompanies_Companies_CompanyId",
                table: "SubscriberToCompanies",
                column: "CompanyId",
                principalTable: "Companies",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_SubscriberToCompanies_AspNetUsers_SubscriberId",
                table: "SubscriberToCompanies");

            migrationBuilder.DropForeignKey(
                name: "FK_SubscriberToCompanies_Companies_CompanyId",
                table: "SubscriberToCompanies");

            migrationBuilder.DropTable(
                name: "CharacteristicValues");

            migrationBuilder.DropPrimaryKey(
                name: "PK_SubscriberToCompanies",
                table: "SubscriberToCompanies");

            migrationBuilder.RenameTable(
                name: "SubscriberToCompanies",
                newName: "SubscriberToCompany");

            migrationBuilder.RenameIndex(
                name: "IX_SubscriberToCompanies_SubscriberId",
                table: "SubscriberToCompany",
                newName: "IX_SubscriberToCompany_SubscriberId");

            migrationBuilder.RenameIndex(
                name: "IX_SubscriberToCompanies_CompanyId",
                table: "SubscriberToCompany",
                newName: "IX_SubscriberToCompany_CompanyId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_SubscriberToCompany",
                table: "SubscriberToCompany",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_SubscriberToCompany_AspNetUsers_SubscriberId",
                table: "SubscriberToCompany",
                column: "SubscriberId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_SubscriberToCompany_Companies_CompanyId",
                table: "SubscriberToCompany",
                column: "CompanyId",
                principalTable: "Companies",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
