using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Authentication.Infrastructure.Migrations
{
    public partial class newtablebieugiacongviec7 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ChiTietBieuGia_BieuGiaCongViec_BieuGiaCongViecID",
                table: "ChiTietBieuGia");

            migrationBuilder.RenameColumn(
                name: "BieuGiaCongViecID",
                table: "ChiTietBieuGia",
                newName: "DM_BieuGiaID");

            migrationBuilder.RenameIndex(
                name: "IX_ChiTietBieuGia_BieuGiaCongViecID",
                table: "ChiTietBieuGia",
                newName: "IX_ChiTietBieuGia_DM_BieuGiaID");

            migrationBuilder.AddForeignKey(
                name: "FK_ChiTietBieuGia_DM_BieuGia_DM_BieuGiaID",
                table: "ChiTietBieuGia",
                column: "DM_BieuGiaID",
                principalTable: "DM_BieuGia",
                principalColumn: "Id");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ChiTietBieuGia_DM_BieuGia_DM_BieuGiaID",
                table: "ChiTietBieuGia");

            migrationBuilder.RenameColumn(
                name: "DM_BieuGiaID",
                table: "ChiTietBieuGia",
                newName: "BieuGiaCongViecID");

            migrationBuilder.RenameIndex(
                name: "IX_ChiTietBieuGia_DM_BieuGiaID",
                table: "ChiTietBieuGia",
                newName: "IX_ChiTietBieuGia_BieuGiaCongViecID");

            migrationBuilder.AddForeignKey(
                name: "FK_ChiTietBieuGia_BieuGiaCongViec_BieuGiaCongViecID",
                table: "ChiTietBieuGia",
                column: "BieuGiaCongViecID",
                principalTable: "BieuGiaCongViec",
                principalColumn: "Id");
        }
    }
}
