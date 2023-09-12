using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Authentication.Infrastructure.Migrations
{
    public partial class add_ThuTu_BieuGia : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "ThuTuHienThi",
                table: "DM_BieuGia_CapNgam",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "ThuTuHienThi",
                table: "DM_BieuGia",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ThuTuHienThi",
                table: "DM_BieuGia_CapNgam");

            migrationBuilder.DropColumn(
                name: "ThuTuHienThi",
                table: "DM_BieuGia");
        }
    }
}
