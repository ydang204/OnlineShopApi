using Microsoft.EntityFrameworkCore.Migrations;

namespace OnlineShop.NotificationAPI.Migrations
{
    public partial class UpdateModifiedByColumn : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "ModifyAt",
                table: "Notifications",
                newName: "ModifiedBy");

            migrationBuilder.RenameColumn(
                name: "ModifyAt",
                table: "Devices",
                newName: "ModifiedBy");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "ModifiedBy",
                table: "Notifications",
                newName: "ModifyAt");

            migrationBuilder.RenameColumn(
                name: "ModifiedBy",
                table: "Devices",
                newName: "ModifyAt");
        }
    }
}
