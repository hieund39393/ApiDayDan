using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Authentication.Infrastructure.Migrations
{
    public partial class Update_Kmtc : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "HeSoDieuChinh_K2mnc",
                table: "ChiTietBieuGia",
                newName: "HeSoDieuChinh_Kmtc");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "HeSoDieuChinh_Kmtc",
                table: "ChiTietBieuGia",
                newName: "HeSoDieuChinh_K2mnc");
        }
    }
}
