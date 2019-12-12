using Microsoft.EntityFrameworkCore.Migrations;

namespace OnlineShop.ProductAPI.Migrations
{
    public partial class AddPublicIdForProductImage : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Slug",
                table: "Brands",
                newName: "SlugName");

            migrationBuilder.AddColumn<string>(
                name: "PublicId",
                table: "ProductImages",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "PublicId",
                table: "ProductImages");

            migrationBuilder.RenameColumn(
                name: "SlugName",
                table: "Brands",
                newName: "Slug");
        }
    }
}
