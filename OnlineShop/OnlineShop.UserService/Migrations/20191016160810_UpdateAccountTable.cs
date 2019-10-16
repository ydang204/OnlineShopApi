using Microsoft.EntityFrameworkCore.Migrations;

namespace OnlineShop.UserService.Migrations
{
    public partial class UpdateAccountTable : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Status",
                table: "Accounts");

            migrationBuilder.RenameColumn(
                name: "Password",
                table: "Accounts",
                newName: "PasswordHash");

            migrationBuilder.AddColumn<int>(
                name: "ObjectStatus",
                table: "Accounts",
                nullable: false,
                defaultValue: 0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ObjectStatus",
                table: "Accounts");

            migrationBuilder.RenameColumn(
                name: "PasswordHash",
                table: "Accounts",
                newName: "Password");

            migrationBuilder.AddColumn<int>(
                name: "Status",
                table: "Accounts",
                nullable: false,
                defaultValue: 0);
        }
    }
}
