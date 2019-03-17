using Microsoft.EntityFrameworkCore.Migrations;

namespace TaskSchedule.Migrations
{
    public partial class Tasks : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_TaskUser_User_UserId",
                table: "TaskUser");

            migrationBuilder.AddForeignKey(
                name: "FK_TaskUser_User_UserId",
                table: "TaskUser",
                column: "UserId",
                principalTable: "User",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_TaskUser_User_UserId",
                table: "TaskUser");

            migrationBuilder.AddForeignKey(
                name: "FK_TaskUser_User_UserId",
                table: "TaskUser",
                column: "UserId",
                principalTable: "User",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
