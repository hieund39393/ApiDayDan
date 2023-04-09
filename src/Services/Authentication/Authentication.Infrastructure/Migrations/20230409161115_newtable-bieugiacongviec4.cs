using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Authentication.Infrastructure.Migrations
{
    public partial class newtablebieugiacongviec4 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
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

            migrationBuilder.DropIndex(
                name: "IX_ChiTietBieuGia_DM_CongViecId",
                table: "ChiTietBieuGia");

            migrationBuilder.DropIndex(
                name: "IX_ChiTietBieuGia_DM_KhuVucId",
                table: "ChiTietBieuGia");

            migrationBuilder.DropIndex(
                name: "IX_ChiTietBieuGia_DM_VungId",
                table: "ChiTietBieuGia");

            migrationBuilder.DropColumn(
                name: "DM_CongViecId",
                table: "ChiTietBieuGia");

            migrationBuilder.DropColumn(
                name: "DM_KhuVucId",
                table: "ChiTietBieuGia");

            migrationBuilder.DropColumn(
                name: "DM_VungId",
                table: "ChiTietBieuGia");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<Guid>(
                name: "DM_CongViecId",
                table: "ChiTietBieuGia",
                type: "uniqueidentifier",
                nullable: true);

            migrationBuilder.AddColumn<Guid>(
                name: "DM_KhuVucId",
                table: "ChiTietBieuGia",
                type: "uniqueidentifier",
                nullable: true);

            migrationBuilder.AddColumn<Guid>(
                name: "DM_VungId",
                table: "ChiTietBieuGia",
                type: "uniqueidentifier",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_ChiTietBieuGia_DM_CongViecId",
                table: "ChiTietBieuGia",
                column: "DM_CongViecId");

            migrationBuilder.CreateIndex(
                name: "IX_ChiTietBieuGia_DM_KhuVucId",
                table: "ChiTietBieuGia",
                column: "DM_KhuVucId");

            migrationBuilder.CreateIndex(
                name: "IX_ChiTietBieuGia_DM_VungId",
                table: "ChiTietBieuGia",
                column: "DM_VungId");

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
    }
}
