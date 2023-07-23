using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Authentication.Infrastructure.Migrations
{
    public partial class ThuTuHienThi_CHCT : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "ThuTuHienThi",
                table: "CauHinhChietTinh_CapNgam",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "ThuTuHienThi",
                table: "CauHinhChietTinh",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ThuTuHienThi",
                table: "CauHinhChietTinh_CapNgam");

            migrationBuilder.DropColumn(
                name: "ThuTuHienThi",
                table: "CauHinhChietTinh");
        }
    }
}
