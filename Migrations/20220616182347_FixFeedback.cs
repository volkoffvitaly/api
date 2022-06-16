using Microsoft.EntityFrameworkCore.Migrations;

namespace TinkoffWatcher_Api.Migrations
{
    public partial class FixFeedback : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "InterwiewId",
                table: "Feedbacks");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {

        }
    }
}
