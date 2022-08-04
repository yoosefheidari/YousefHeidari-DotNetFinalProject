using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace App.Infrastructure.DataBase.SqlServer.Migrations
{
    public partial class addiswritebycustomerfieldtocommenttable : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "IsWriteByCustomer",
                table: "Comments",
                type: "bit",
                nullable: false,
                defaultValue: false);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "IsWriteByCustomer",
                table: "Comments");
        }
    }
}
