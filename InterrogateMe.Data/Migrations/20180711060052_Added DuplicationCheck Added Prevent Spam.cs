using Microsoft.EntityFrameworkCore.Migrations;

namespace InterrogateMe.Data.Migrations
{
    public partial class AddedDuplicationCheckAddedPreventSpam : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "PreventIpDuplication",
                table: "Topics",
                newName: "PreventSpam");

            migrationBuilder.AddColumn<int>(
                name: "DuplicationCheck",
                table: "Topics",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<string>(
                name: "RequestScheme",
                table: "IpAddresses",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "DuplicationCheck",
                table: "Topics");

            migrationBuilder.DropColumn(
                name: "RequestScheme",
                table: "IpAddresses");

            migrationBuilder.RenameColumn(
                name: "PreventSpam",
                table: "Topics",
                newName: "PreventIpDuplication");
        }
    }
}
