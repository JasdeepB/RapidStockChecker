using Microsoft.EntityFrameworkCore.Migrations;

namespace RSC.DataAccess.Migrations
{
    public partial class AddSKUToRestockHistory : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "SKU",
                table: "RestockHistory",
                type: "nvarchar(max)",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "SKU",
                table: "RestockHistory");
        }
    }
}
