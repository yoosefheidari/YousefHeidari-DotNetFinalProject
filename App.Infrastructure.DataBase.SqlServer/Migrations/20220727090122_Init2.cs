using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace App.Infrastructure.DataBase.SqlServer.Migrations
{
    public partial class Init2 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Suggests_Orders_OrderId",
                table: "Suggests");

            migrationBuilder.AddForeignKey(
                name: "FK_Suggests_Orders_OrderId",
                table: "Suggests",
                column: "OrderId",
                principalTable: "Orders",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Suggests_Orders_OrderId",
                table: "Suggests");

            migrationBuilder.AddForeignKey(
                name: "FK_Suggests_Orders_OrderId",
                table: "Suggests",
                column: "OrderId",
                principalTable: "Orders",
                principalColumn: "Id");
        }
    }
}
