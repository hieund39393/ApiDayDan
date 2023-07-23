using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Authentication.Infrastructure.Migrations
{
    public partial class Db_Add_DGNCHai : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<decimal>(
                name: "DonGiaNhanCongBa",
                table: "DonGiaChietTinh",
                type: "numeric(18,1)",
                nullable: true);

            migrationBuilder.AddColumn<decimal>(
                name: "DonGiaNhanCongHai",
                table: "DonGiaChietTinh",
                type: "numeric(18,1)",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "DonGiaNhanCongBa",
                table: "DonGiaChietTinh");

            migrationBuilder.DropColumn(
                name: "DonGiaNhanCongHai",
                table: "DonGiaChietTinh");
        }
    }
}
