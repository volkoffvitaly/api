using Microsoft.EntityFrameworkCore.Migrations;

namespace TinkoffWatcher_Api.Migrations
{
    public partial class SlotsDatesFormatEdited : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Day",
                table: "Slots");

            migrationBuilder.DropColumn(
                name: "Hour",
                table: "Slots");

            migrationBuilder.DropColumn(
                name: "Minute",
                table: "Slots");

            migrationBuilder.DropColumn(
                name: "Month",
                table: "Slots");

            migrationBuilder.DropColumn(
                name: "Year",
                table: "Slots");

            migrationBuilder.RenameColumn(
                name: "SlotDate",
                table: "Slots",
                newName: "DateTime");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "DateTime",
                table: "Slots",
                newName: "SlotDate");

            migrationBuilder.AddColumn<int>(
                name: "Day",
                table: "Slots",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "Hour",
                table: "Slots",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "Minute",
                table: "Slots",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "Month",
                table: "Slots",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "Year",
                table: "Slots",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }
    }
}
