using Microsoft.EntityFrameworkCore.Migrations;

namespace OnlineShop.UserAPI.Migrations
{
    public partial class UpdateModifiedByColumn : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "ModifyAt",
                table: "Roles",
                newName: "ModifiedBy");

            migrationBuilder.RenameColumn(
                name: "ModifyAt",
                table: "Permission",
                newName: "ModifiedBy");

            migrationBuilder.RenameColumn(
                name: "ModifyAt",
                table: "Accounts",
                newName: "ModifiedBy");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "ModifiedBy",
                table: "Roles",
                newName: "ModifyAt");

            migrationBuilder.RenameColumn(
                name: "ModifiedBy",
                table: "Permission",
                newName: "ModifyAt");

            migrationBuilder.RenameColumn(
                name: "ModifiedBy",
                table: "Accounts",
                newName: "ModifyAt");
        }
    }
}
