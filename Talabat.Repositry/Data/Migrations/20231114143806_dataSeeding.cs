using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Talabat.Repositry.Data.Migrations
{
    public partial class dataSeeding : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "DeilveryTime",
                table: "DeliveryMethods",
                newName: "DeliveryTime");

            migrationBuilder.AlterColumn<int>(
                name: "DeliveryMethodId",
                table: "Orders",
                type: "int",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "int?");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "DeliveryTime",
                table: "DeliveryMethods",
                newName: "DeilveryTime");

            migrationBuilder.AlterColumn<int>(
                name: "DeliveryMethodId",
                table: "Orders",
                type: "int?",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "int");
        }
    }
}
