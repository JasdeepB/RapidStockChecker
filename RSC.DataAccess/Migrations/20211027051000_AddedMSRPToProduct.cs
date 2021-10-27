using Microsoft.EntityFrameworkCore.Migrations;

namespace RSC.DataAccess.Migrations
{
    public partial class AddedMSRPToProduct : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<double>(
                name: "MSRP",
                table: "Products",
                type: "float",
                nullable: false,
                defaultValue: 0.0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "MSRP",
                table: "Products");
        }
    }
}
