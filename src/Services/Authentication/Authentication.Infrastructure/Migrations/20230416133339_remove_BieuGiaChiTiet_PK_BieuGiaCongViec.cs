using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Authentication.Infrastructure.Migrations
{
    public partial class remove_BieuGiaChiTiet_PK_BieuGiaCongViec : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ChiTietBieuGia_BieuGiaCongViec_IdBieuGiaCongViec",
                table: "ChiTietBieuGia");

            migrationBuilder.DropIndex(
                name: "IX_ChiTietBieuGia_IdBieuGiaCongViec",
                table: "ChiTietBieuGia");

            migrationBuilder.DropColumn(
                name: "IdBieuGiaCongViec",
                table: "ChiTietBieuGia");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<Guid>(
                name: "IdBieuGiaCongViec",
                table: "ChiTietBieuGia",
                type: "uniqueidentifier",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_ChiTietBieuGia_IdBieuGiaCongViec",
                table: "ChiTietBieuGia",
                column: "IdBieuGiaCongViec");

            migrationBuilder.AddForeignKey(
                name: "FK_ChiTietBieuGia_BieuGiaCongViec_IdBieuGiaCongViec",
                table: "ChiTietBieuGia",
                column: "IdBieuGiaCongViec",
                principalTable: "BieuGiaCongViec",
                principalColumn: "Id");
        }
    }
}
