using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Authentication.Infrastructure.Migrations
{
    public partial class CHCT_CN : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_CauHinhChietTinh_CapNgam_DM_CongViec_IdCongViec",
                table: "CauHinhChietTinh_CapNgam");

            migrationBuilder.AddForeignKey(
                name: "FK_CauHinhChietTinh_CapNgam_DM_CongViec_CapNgam_IdCongViec",
                table: "CauHinhChietTinh_CapNgam",
                column: "IdCongViec",
                principalTable: "DM_CongViec_CapNgam",
                principalColumn: "Id");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_CauHinhChietTinh_CapNgam_DM_CongViec_CapNgam_IdCongViec",
                table: "CauHinhChietTinh_CapNgam");

            migrationBuilder.AddForeignKey(
                name: "FK_CauHinhChietTinh_CapNgam_DM_CongViec_IdCongViec",
                table: "CauHinhChietTinh_CapNgam",
                column: "IdCongViec",
                principalTable: "DM_CongViec",
                principalColumn: "Id");
        }
    }
}
