using Microsoft.EntityFrameworkCore.Migrations;

namespace OnlineShop.ProductAPI.Migrations
{
    public partial class UpdateModifiedByColumn : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "ModifyAt",
                table: "Products",
                newName: "ModifiedBy");

            migrationBuilder.RenameColumn(
                name: "ModifyAt",
                table: "ProductImages",
                newName: "ModifiedBy");

            migrationBuilder.RenameColumn(
                name: "ModifyAt",
                table: "Categories",
                newName: "ModifiedBy");

            migrationBuilder.RenameColumn(
                name: "ModifyAt",
                table: "Brands",
                newName: "ModifiedBy");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "ModifiedBy",
                table: "Products",
                newName: "ModifyAt");

            migrationBuilder.RenameColumn(
                name: "ModifiedBy",
                table: "ProductImages",
                newName: "ModifyAt");

            migrationBuilder.RenameColumn(
                name: "ModifiedBy",
                table: "Categories",
                newName: "ModifyAt");

            migrationBuilder.RenameColumn(
                name: "ModifiedBy",
                table: "Brands",
                newName: "ModifyAt");
        }
    }
}
