using Microsoft.EntityFrameworkCore.Migrations;

namespace OnlineShop.OrderAPI.Migrations
{
    public partial class AddReceiverOfOrder : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Receiver",
                table: "Orders",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Receiver",
                table: "Orders");
        }
    }
}
