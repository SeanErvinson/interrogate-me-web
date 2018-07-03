using Microsoft.EntityFrameworkCore.Migrations;

namespace InterrogateMe.Data.Migrations
{
    public partial class Updated_ErrorMEssage : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "Title",
                table: "Topics",
                maxLength: 2147483647,
                nullable: false,
                oldClrType: typeof(string),
                oldMaxLength: 60);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "Title",
                table: "Topics",
                maxLength: 60,
                nullable: false,
                oldClrType: typeof(string),
                oldMaxLength: 2147483647);
        }
    }
}
