using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace InterrogateMe.Data.Migrations
{
    public partial class Renamed_DateAsked : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "DateAsked",
                table: "Links",
                newName: "DateCreated");

            migrationBuilder.AddColumn<DateTime>(
                name: "DateAsked",
                table: "Questions",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "DateAsked",
                table: "Questions");

            migrationBuilder.RenameColumn(
                name: "DateCreated",
                table: "Links",
                newName: "DateAsked");
        }
    }
}
