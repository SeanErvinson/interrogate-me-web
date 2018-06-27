using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace InterrogateMe.Data.Migrations
{
    public partial class Added_DateTime : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<DateTime>(
                name: "DateAsked",
                table: "Links",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "DateAsked",
                table: "Links");
        }
    }
}
