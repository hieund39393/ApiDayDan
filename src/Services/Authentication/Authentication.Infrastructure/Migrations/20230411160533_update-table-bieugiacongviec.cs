using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Authentication.Infrastructure.Migrations
{
    public partial class updatetablebieugiacongviec : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_BieuGiaCongViec_DM_BieuGia_BieuGiaID",
                table: "BieuGiaCongViec");

            migrationBuilder.DropForeignKey(
                name: "FK_BieuGiaCongViec_DM_CongViec_CongViecID",
                table: "BieuGiaCongViec");

            migrationBuilder.DropForeignKey(
                name: "FK_BieuGiaCongViec_DM_KhuVuc_KhuVucID",
                table: "BieuGiaCongViec");

            migrationBuilder.DropForeignKey(
                name: "FK_BieuGiaCongViec_DM_Vung_VungID",
                table: "BieuGiaCongViec");

            migrationBuilder.DropIndex(
                name: "IX_BieuGiaCongViec_BieuGiaID",
                table: "BieuGiaCongViec");

            migrationBuilder.DropIndex(
                name: "IX_BieuGiaCongViec_CongViecID",
                table: "BieuGiaCongViec");

            migrationBuilder.DropColumn(
                name: "BieuGiaID",
                table: "BieuGiaCongViec");

            migrationBuilder.DropColumn(
                name: "CongViecID",
                table: "BieuGiaCongViec");

            migrationBuilder.RenameColumn(
                name: "VungID",
                table: "BieuGiaCongViec",
                newName: "IdCongViec");

            migrationBuilder.RenameColumn(
                name: "KhuVucID",
                table: "BieuGiaCongViec",
                newName: "IdBieuGia");

            migrationBuilder.RenameIndex(
                name: "IX_BieuGiaCongViec_VungID",
                table: "BieuGiaCongViec",
                newName: "IX_BieuGiaCongViec_IdCongViec");

            migrationBuilder.RenameIndex(
                name: "IX_BieuGiaCongViec_KhuVucID",
                table: "BieuGiaCongViec",
                newName: "IX_BieuGiaCongViec_IdBieuGia");

            migrationBuilder.AddForeignKey(
                name: "FK_BieuGiaCongViec_DM_BieuGia_IdBieuGia",
                table: "BieuGiaCongViec",
                column: "IdBieuGia",
                principalTable: "DM_BieuGia",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_BieuGiaCongViec_DM_CongViec_IdCongViec",
                table: "BieuGiaCongViec",
                column: "IdCongViec",
                principalTable: "DM_CongViec",
                principalColumn: "Id");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_BieuGiaCongViec_DM_BieuGia_IdBieuGia",
                table: "BieuGiaCongViec");

            migrationBuilder.DropForeignKey(
                name: "FK_BieuGiaCongViec_DM_CongViec_IdCongViec",
                table: "BieuGiaCongViec");

            migrationBuilder.RenameColumn(
                name: "IdCongViec",
                table: "BieuGiaCongViec",
                newName: "VungID");

            migrationBuilder.RenameColumn(
                name: "IdBieuGia",
                table: "BieuGiaCongViec",
                newName: "KhuVucID");

            migrationBuilder.RenameIndex(
                name: "IX_BieuGiaCongViec_IdCongViec",
                table: "BieuGiaCongViec",
                newName: "IX_BieuGiaCongViec_VungID");

            migrationBuilder.RenameIndex(
                name: "IX_BieuGiaCongViec_IdBieuGia",
                table: "BieuGiaCongViec",
                newName: "IX_BieuGiaCongViec_KhuVucID");

            migrationBuilder.AddColumn<Guid>(
                name: "BieuGiaID",
                table: "BieuGiaCongViec",
                type: "uniqueidentifier",
                nullable: true);

            migrationBuilder.AddColumn<Guid>(
                name: "CongViecID",
                table: "BieuGiaCongViec",
                type: "uniqueidentifier",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_BieuGiaCongViec_BieuGiaID",
                table: "BieuGiaCongViec",
                column: "BieuGiaID");

            migrationBuilder.CreateIndex(
                name: "IX_BieuGiaCongViec_CongViecID",
                table: "BieuGiaCongViec",
                column: "CongViecID");

            migrationBuilder.AddForeignKey(
                name: "FK_BieuGiaCongViec_DM_BieuGia_BieuGiaID",
                table: "BieuGiaCongViec",
                column: "BieuGiaID",
                principalTable: "DM_BieuGia",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_BieuGiaCongViec_DM_CongViec_CongViecID",
                table: "BieuGiaCongViec",
                column: "CongViecID",
                principalTable: "DM_CongViec",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_BieuGiaCongViec_DM_KhuVuc_KhuVucID",
                table: "BieuGiaCongViec",
                column: "KhuVucID",
                principalTable: "DM_KhuVuc",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_BieuGiaCongViec_DM_Vung_VungID",
                table: "BieuGiaCongViec",
                column: "VungID",
                principalTable: "DM_Vung",
                principalColumn: "Id");
        }
    }
}
