using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace TaskSchedule.Migrations {
  public partial class UserSetting : Migration {
    protected override void Up (MigrationBuilder migrationBuilder) {

      migrationBuilder.CreateTable (
        name: "UserSetting",
        columns : table => new {
          id = table.Column<string> (nullable: false),
            Name = table.Column<string> (nullable: true),
            PhotoProfile = table.Column<byte[]> (nullable: true),
            UserId = table.Column<string> (nullable: true)
        },
        constraints : table => {
          table.PrimaryKey ("PK_UserSetting", x => x.id);
          table.ForeignKey (
            name: "FK_UserSetting_User_UserId",
            column : x => x.UserId,
            principalTable: "User",
            principalColumn: "Id",
            onDelete : ReferentialAction.Restrict);
        });

      migrationBuilder.CreateIndex (
        name: "IX_UserSetting_UserId",
        table: "UserSetting",
        column: "UserId");
    }

    protected override void Down (MigrationBuilder migrationBuilder) {
      migrationBuilder.DropTable (
        name: "UserSetting");

    }
  }
}
