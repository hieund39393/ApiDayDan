using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Authentication.Infrastructure.Migrations
{
    public partial class ThuTuHienThi_CTK : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "ThuTuHienThi",
                table: "DM_VatLieu_CapNgam",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "ThuTuHienThi",
                table: "DM_VatLieu",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "ThuTuHienThi",
                table: "DM_CongViec_CapNgam",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "ThuTuHienThi",
                table: "DM_CongViec",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ThuTuHienThi",
                table: "DM_VatLieu_CapNgam");

            migrationBuilder.DropColumn(
                name: "ThuTuHienThi",
                table: "DM_VatLieu");

            migrationBuilder.DropColumn(
                name: "ThuTuHienThi",
                table: "DM_CongViec_CapNgam");

            migrationBuilder.DropColumn(
                name: "ThuTuHienThi",
                table: "DM_CongViec");
        }
    }
}
