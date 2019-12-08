using Microsoft.EntityFrameworkCore.Migrations;

namespace OnlineShop.OrderAPI.Migrations
{
    public partial class UpdateOrderEntity : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "ProductCount",
                table: "OrderDetails",
                newName: "Count");

            migrationBuilder.AddColumn<int>(
                name: "Status",
                table: "Orders",
                nullable: false,
                defaultValue: 0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Status",
                table: "Orders");

            migrationBuilder.RenameColumn(
                name: "Count",
                table: "OrderDetails",
                newName: "ProductCount");
        }
    }
}
