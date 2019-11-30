using Microsoft.EntityFrameworkCore.Migrations;

namespace OnlineShop.NotificationAPI.Migrations
{
    public partial class AllowNullForFields : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<int>(
                name: "AccountId",
                table: "Notifications",
                nullable: true,
                oldClrType: typeof(int));

            migrationBuilder.AddColumn<int>(
                name: "AccountId",
                table: "Devices",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "AccountId",
                table: "Devices");

            migrationBuilder.AlterColumn<int>(
                name: "AccountId",
                table: "Notifications",
                nullable: false,
                oldClrType: typeof(int),
                oldNullable: true);
        }
    }
}
