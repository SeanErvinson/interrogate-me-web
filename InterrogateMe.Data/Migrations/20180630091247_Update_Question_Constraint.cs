using Microsoft.EntityFrameworkCore.Migrations;

namespace InterrogateMe.Data.Migrations
{
    public partial class Update_Question_Constraint : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "Content",
                table: "Questions",
                maxLength: 2147483647,
                nullable: false,
                oldClrType: typeof(string),
                oldMaxLength: 60);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "Content",
                table: "Questions",
                maxLength: 60,
                nullable: false,
                oldClrType: typeof(string),
                oldMaxLength: 2147483647);
        }
    }
}
