using Microsoft.EntityFrameworkCore.Migrations;

namespace InterrogateMe.Data.Migrations
{
    public partial class RemovedIdentifierandBrowserCookie : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Identifiers_Topics_TopicId",
                table: "Identifiers");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Identifiers",
                table: "Identifiers");

            migrationBuilder.DropColumn(
                name: "Discriminator",
                table: "Identifiers");

            migrationBuilder.DropColumn(
                name: "Cookie",
                table: "Identifiers");

            migrationBuilder.RenameTable(
                name: "Identifiers",
                newName: "IpAddresses");

            migrationBuilder.RenameIndex(
                name: "IX_Identifiers_TopicId",
                table: "IpAddresses",
                newName: "IX_IpAddresses_TopicId");

            migrationBuilder.AlterColumn<string>(
                name: "Address",
                table: "IpAddresses",
                nullable: false,
                oldClrType: typeof(string),
                oldNullable: true);

            migrationBuilder.AddPrimaryKey(
                name: "PK_IpAddresses",
                table: "IpAddresses",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_IpAddresses_Topics_TopicId",
                table: "IpAddresses",
                column: "TopicId",
                principalTable: "Topics",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_IpAddresses_Topics_TopicId",
                table: "IpAddresses");

            migrationBuilder.DropPrimaryKey(
                name: "PK_IpAddresses",
                table: "IpAddresses");

            migrationBuilder.RenameTable(
                name: "IpAddresses",
                newName: "Identifiers");

            migrationBuilder.RenameIndex(
                name: "IX_IpAddresses_TopicId",
                table: "Identifiers",
                newName: "IX_Identifiers_TopicId");

            migrationBuilder.AlterColumn<string>(
                name: "Address",
                table: "Identifiers",
                nullable: true,
                oldClrType: typeof(string));

            migrationBuilder.AddColumn<string>(
                name: "Discriminator",
                table: "Identifiers",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "Cookie",
                table: "Identifiers",
                nullable: true);

            migrationBuilder.AddPrimaryKey(
                name: "PK_Identifiers",
                table: "Identifiers",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Identifiers_Topics_TopicId",
                table: "Identifiers",
                column: "TopicId",
                principalTable: "Topics",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
