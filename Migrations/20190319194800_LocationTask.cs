using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

namespace TaskSchedule.Migrations
{
    public partial class LocationTask : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "LocationId",
                table: "TaskUser",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "Location",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    FundationName = table.Column<string>(nullable: false),
                    Address = table.Column<string>(nullable: false),
                    Number = table.Column<string>(nullable: false),
                    Neighborhood = table.Column<string>(nullable: false),
                    PostalCode = table.Column<string>(nullable: false),
                    Phone = table.Column<string>(nullable: false),
                    Responsible = table.Column<string>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Location", x => x.Id);
                });

            migrationBuilder.CreateIndex(
                name: "IX_TaskUser_LocationId",
                table: "TaskUser",
                column: "LocationId");

            migrationBuilder.AddForeignKey(
                name: "FK_TaskUser_Location_LocationId",
                table: "TaskUser",
                column: "LocationId",
                principalTable: "Location",
                principalColumn: "Id",
                onDelete: ReferentialAction.SetNull);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_TaskUser_Location_LocationId",
                table: "TaskUser");

            migrationBuilder.DropTable(
                name: "Location");

            migrationBuilder.DropIndex(
                name: "IX_TaskUser_LocationId",
                table: "TaskUser");

            migrationBuilder.DropColumn(
                name: "LocationId",
                table: "TaskUser");
        }
    }
}
