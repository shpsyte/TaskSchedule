using Microsoft.EntityFrameworkCore.Migrations;

namespace TaskSchedule.Migrations
{
    public partial class UserPassTip : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "PasswordTip",
                table: "User",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "PasswordTip",
                table: "User");
        }
    }
}
