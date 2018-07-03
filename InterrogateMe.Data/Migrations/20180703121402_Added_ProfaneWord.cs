using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace InterrogateMe.Data.Migrations
{
    public partial class Added_ProfaneWord : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "ProfaneWords",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    Word = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ProfaneWords", x => x.Id);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ProfaneWords");
        }
    }
}
