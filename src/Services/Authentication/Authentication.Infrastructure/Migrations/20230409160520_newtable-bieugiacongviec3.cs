using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Authentication.Infrastructure.Migrations
{
    public partial class newtablebieugiacongviec3 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ChiTietBieuGia_DM_CongViec_CongViecID",
                table: "ChiTietBieuGia");

            migrationBuilder.DropForeignKey(
                name: "FK_ChiTietBieuGia_DM_KhuVuc_KhuVucID",
                table: "ChiTietBieuGia");

            migrationBuilder.DropForeignKey(
                name: "FK_ChiTietBieuGia_DM_Vung_VungID",
                table: "ChiTietBieuGia");

            migrationBuilder.RenameColumn(
                name: "VungID",
                table: "ChiTietBieuGia",
                newName: "DM_VungId");

            migrationBuilder.RenameColumn(
                name: "KhuVucID",
                table: "ChiTietBieuGia",
                newName: "DM_KhuVucId");

            migrationBuilder.RenameColumn(
                name: "CongViecID",
                table: "ChiTietBieuGia",
                newName: "DM_CongViecId");

            migrationBuilder.RenameIndex(
                name: "IX_ChiTietBieuGia_VungID",
                table: "ChiTietBieuGia",
                newName: "IX_ChiTietBieuGia_DM_VungId");

            migrationBuilder.RenameIndex(
                name: "IX_ChiTietBieuGia_KhuVucID",
                table: "ChiTietBieuGia",
                newName: "IX_ChiTietBieuGia_DM_KhuVucId");

            migrationBuilder.RenameIndex(
                name: "IX_ChiTietBieuGia_CongViecID",
                table: "ChiTietBieuGia",
                newName: "IX_ChiTietBieuGia_DM_CongViecId");

            migrationBuilder.AddForeignKey(
                name: "FK_ChiTietBieuGia_DM_CongViec_DM_CongViecId",
                table: "ChiTietBieuGia",
                column: "DM_CongViecId",
                principalTable: "DM_CongViec",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_ChiTietBieuGia_DM_KhuVuc_DM_KhuVucId",
                table: "ChiTietBieuGia",
                column: "DM_KhuVucId",
                principalTable: "DM_KhuVuc",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_ChiTietBieuGia_DM_Vung_DM_VungId",
                table: "ChiTietBieuGia",
                column: "DM_VungId",
                principalTable: "DM_Vung",
                principalColumn: "Id");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ChiTietBieuGia_DM_CongViec_DM_CongViecId",
                table: "ChiTietBieuGia");

            migrationBuilder.DropForeignKey(
                name: "FK_ChiTietBieuGia_DM_KhuVuc_DM_KhuVucId",
                table: "ChiTietBieuGia");

            migrationBuilder.DropForeignKey(
                name: "FK_ChiTietBieuGia_DM_Vung_DM_VungId",
                table: "ChiTietBieuGia");

            migrationBuilder.RenameColumn(
                name: "DM_VungId",
                table: "ChiTietBieuGia",
                newName: "VungID");

            migrationBuilder.RenameColumn(
                name: "DM_KhuVucId",
                table: "ChiTietBieuGia",
                newName: "KhuVucID");

            migrationBuilder.RenameColumn(
                name: "DM_CongViecId",
                table: "ChiTietBieuGia",
                newName: "CongViecID");

            migrationBuilder.RenameIndex(
                name: "IX_ChiTietBieuGia_DM_VungId",
                table: "ChiTietBieuGia",
                newName: "IX_ChiTietBieuGia_VungID");

            migrationBuilder.RenameIndex(
                name: "IX_ChiTietBieuGia_DM_KhuVucId",
                table: "ChiTietBieuGia",
                newName: "IX_ChiTietBieuGia_KhuVucID");

            migrationBuilder.RenameIndex(
                name: "IX_ChiTietBieuGia_DM_CongViecId",
                table: "ChiTietBieuGia",
                newName: "IX_ChiTietBieuGia_CongViecID");

            migrationBuilder.AddForeignKey(
                name: "FK_ChiTietBieuGia_DM_CongViec_CongViecID",
                table: "ChiTietBieuGia",
                column: "CongViecID",
                principalTable: "DM_CongViec",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_ChiTietBieuGia_DM_KhuVuc_KhuVucID",
                table: "ChiTietBieuGia",
                column: "KhuVucID",
                principalTable: "DM_KhuVuc",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_ChiTietBieuGia_DM_Vung_VungID",
                table: "ChiTietBieuGia",
                column: "VungID",
                principalTable: "DM_Vung",
                principalColumn: "Id");
        }
    }
}
