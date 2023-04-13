using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Authentication.Infrastructure.Migrations
{
    public partial class updatecolumechitietbieugia : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ChiTietBieuGia_DM_BieuGia_DM_BieuGiaID",
                table: "ChiTietBieuGia");

            migrationBuilder.DropIndex(
                name: "IX_ChiTietBieuGia_DM_BieuGiaID",
                table: "ChiTietBieuGia");

            migrationBuilder.DropColumn(
                name: "CongViecID",
                table: "ChiTietBieuGia");

            migrationBuilder.DropColumn(
                name: "DM_BieuGiaID",
                table: "ChiTietBieuGia");

            migrationBuilder.RenameColumn(
                name: "VungID",
                table: "ChiTietBieuGia",
                newName: "IDCongViec");

            migrationBuilder.RenameColumn(
                name: "KhuVucID",
                table: "ChiTietBieuGia",
                newName: "IDBieuGia");

            migrationBuilder.CreateIndex(
                name: "IX_ChiTietBieuGia_IDBieuGia",
                table: "ChiTietBieuGia",
                column: "IDBieuGia");

            migrationBuilder.CreateIndex(
                name: "IX_ChiTietBieuGia_IDCongViec",
                table: "ChiTietBieuGia",
                column: "IDCongViec");

            migrationBuilder.AddForeignKey(
                name: "FK_ChiTietBieuGia_DM_BieuGia_IDBieuGia",
                table: "ChiTietBieuGia",
                column: "IDBieuGia",
                principalTable: "DM_BieuGia",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_ChiTietBieuGia_DM_CongViec_IDCongViec",
                table: "ChiTietBieuGia",
                column: "IDCongViec",
                principalTable: "DM_CongViec",
                principalColumn: "Id");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ChiTietBieuGia_DM_BieuGia_IDBieuGia",
                table: "ChiTietBieuGia");

            migrationBuilder.DropForeignKey(
                name: "FK_ChiTietBieuGia_DM_CongViec_IDCongViec",
                table: "ChiTietBieuGia");

            migrationBuilder.DropIndex(
                name: "IX_ChiTietBieuGia_IDBieuGia",
                table: "ChiTietBieuGia");

            migrationBuilder.DropIndex(
                name: "IX_ChiTietBieuGia_IDCongViec",
                table: "ChiTietBieuGia");

            migrationBuilder.RenameColumn(
                name: "IDCongViec",
                table: "ChiTietBieuGia",
                newName: "VungID");

            migrationBuilder.RenameColumn(
                name: "IDBieuGia",
                table: "ChiTietBieuGia",
                newName: "KhuVucID");

            migrationBuilder.AddColumn<Guid>(
                name: "CongViecID",
                table: "ChiTietBieuGia",
                type: "uniqueidentifier",
                nullable: true);

            migrationBuilder.AddColumn<Guid>(
                name: "DM_BieuGiaID",
                table: "ChiTietBieuGia",
                type: "uniqueidentifier",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_ChiTietBieuGia_DM_BieuGiaID",
                table: "ChiTietBieuGia",
                column: "DM_BieuGiaID");

            migrationBuilder.AddForeignKey(
                name: "FK_ChiTietBieuGia_DM_BieuGia_DM_BieuGiaID",
                table: "ChiTietBieuGia",
                column: "DM_BieuGiaID",
                principalTable: "DM_BieuGia",
                principalColumn: "Id");
        }
    }
}
