using Microsoft.EntityFrameworkCore.Migrations;

namespace TaskSchedule.Migrations
{
    public partial class ControladoPorHorari : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "IsHourControl",
                table: "TaskUser",
                nullable: false,
                defaultValue: false);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "IsHourControl",
                table: "TaskUser");
        }
    }
}
