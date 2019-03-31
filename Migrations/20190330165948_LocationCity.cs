using Microsoft.EntityFrameworkCore.Migrations;

namespace TaskSchedule.Migrations
{
    public partial class LocationCity : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "FundationName",
                table: "TaskUser",
                nullable: true,
                oldClrType: typeof(string));

            migrationBuilder.AddColumn<string>(
                name: "CityAndState",
                table: "Location",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "CityAndState",
                table: "Location");

            migrationBuilder.AlterColumn<string>(
                name: "FundationName",
                table: "TaskUser",
                nullable: false,
                oldClrType: typeof(string),
                oldNullable: true);
        }
    }
}
