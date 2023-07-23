using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Authentication.Infrastructure.Migrations
{
    public partial class DinhMuc2_3 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<decimal>(
                name: "DinhMuc",
                table: "DonGiaChietTinh",
                type: "decimal(18,2)",
                nullable: true);

            migrationBuilder.AddColumn<decimal>(
                name: "DinhMucBa",
                table: "DonGiaChietTinh",
                type: "decimal(18,2)",
                nullable: true);

            migrationBuilder.AddColumn<decimal>(
                name: "DinhMucHai",
                table: "DonGiaChietTinh",
                type: "decimal(18,2)",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "DinhMuc",
                table: "DonGiaChietTinh");

            migrationBuilder.DropColumn(
                name: "DinhMucBa",
                table: "DonGiaChietTinh");

            migrationBuilder.DropColumn(
                name: "DinhMucHai",
                table: "DonGiaChietTinh");
        }
    }
}
