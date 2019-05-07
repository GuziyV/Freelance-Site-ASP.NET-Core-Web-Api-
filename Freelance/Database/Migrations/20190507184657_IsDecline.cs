using Microsoft.EntityFrameworkCore.Migrations;

namespace Database.Migrations
{
    public partial class IsDecline : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "IsDeclined",
                table: "TeamUsers",
                nullable: false,
                defaultValue: false);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "IsDeclined",
                table: "TeamUsers");
        }
    }
}
