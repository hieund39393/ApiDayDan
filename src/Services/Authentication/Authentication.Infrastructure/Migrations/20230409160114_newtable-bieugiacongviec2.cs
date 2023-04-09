using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Authentication.Infrastructure.Migrations
{
    public partial class newtablebieugiacongviec2 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ChiTietBieuGia_DM_BieuGia_BieugiaID",
                table: "ChiTietBieuGia");

            migrationBuilder.RenameColumn(
                name: "BieugiaID",
                table: "ChiTietBieuGia",
                newName: "DM_BieuGiaId");

            migrationBuilder.RenameIndex(
                name: "IX_ChiTietBieuGia_BieugiaID",
                table: "ChiTietBieuGia",
                newName: "IX_ChiTietBieuGia_DM_BieuGiaId");

            migrationBuilder.AlterColumn<decimal>(
                name: "HeSoDieuChinh_K2nc",
                table: "ChiTietBieuGia",
                type: "numeric(18,1)",
                nullable: false,
                oldClrType: typeof(double),
                oldType: "float");

            migrationBuilder.AlterColumn<decimal>(
                name: "HeSoDieuChinh_K2mnc",
                table: "ChiTietBieuGia",
                type: "numeric(18,1)",
                nullable: false,
                oldClrType: typeof(double),
                oldType: "float");

            migrationBuilder.AlterColumn<decimal>(
                name: "HeSoDieuChinh_K1nc",
                table: "ChiTietBieuGia",
                type: "numeric(18,1)",
                nullable: false,
                oldClrType: typeof(double),
                oldType: "float");

            migrationBuilder.AlterColumn<decimal>(
                name: "DonGia_VL",
                table: "ChiTietBieuGia",
                type: "numeric(18,1)",
                nullable: false,
                oldClrType: typeof(double),
                oldType: "float");

            migrationBuilder.AlterColumn<decimal>(
                name: "DonGia_NC",
                table: "ChiTietBieuGia",
                type: "numeric(18,1)",
                nullable: false,
                oldClrType: typeof(double),
                oldType: "float");

            migrationBuilder.AlterColumn<decimal>(
                name: "DonGia_MTC",
                table: "ChiTietBieuGia",
                type: "numeric(18,1)",
                nullable: false,
                oldClrType: typeof(double),
                oldType: "float");

            migrationBuilder.AddColumn<Guid>(
                name: "BieuGiaCongViecID",
                table: "ChiTietBieuGia",
                type: "uniqueidentifier",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_ChiTietBieuGia_BieuGiaCongViecID",
                table: "ChiTietBieuGia",
                column: "BieuGiaCongViecID");

            migrationBuilder.AddForeignKey(
                name: "FK_ChiTietBieuGia_BieuGiaCongViec_BieuGiaCongViecID",
                table: "ChiTietBieuGia",
                column: "BieuGiaCongViecID",
                principalTable: "BieuGiaCongViec",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_ChiTietBieuGia_DM_BieuGia_DM_BieuGiaId",
                table: "ChiTietBieuGia",
                column: "DM_BieuGiaId",
                principalTable: "DM_BieuGia",
                principalColumn: "Id");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ChiTietBieuGia_BieuGiaCongViec_BieuGiaCongViecID",
                table: "ChiTietBieuGia");

            migrationBuilder.DropForeignKey(
                name: "FK_ChiTietBieuGia_DM_BieuGia_DM_BieuGiaId",
                table: "ChiTietBieuGia");

            migrationBuilder.DropIndex(
                name: "IX_ChiTietBieuGia_BieuGiaCongViecID",
                table: "ChiTietBieuGia");

            migrationBuilder.DropColumn(
                name: "BieuGiaCongViecID",
                table: "ChiTietBieuGia");

            migrationBuilder.RenameColumn(
                name: "DM_BieuGiaId",
                table: "ChiTietBieuGia",
                newName: "BieugiaID");

            migrationBuilder.RenameIndex(
                name: "IX_ChiTietBieuGia_DM_BieuGiaId",
                table: "ChiTietBieuGia",
                newName: "IX_ChiTietBieuGia_BieugiaID");

            migrationBuilder.AlterColumn<double>(
                name: "HeSoDieuChinh_K2nc",
                table: "ChiTietBieuGia",
                type: "float",
                nullable: false,
                oldClrType: typeof(decimal),
                oldType: "numeric(18,1)");

            migrationBuilder.AlterColumn<double>(
                name: "HeSoDieuChinh_K2mnc",
                table: "ChiTietBieuGia",
                type: "float",
                nullable: false,
                oldClrType: typeof(decimal),
                oldType: "numeric(18,1)");

            migrationBuilder.AlterColumn<double>(
                name: "HeSoDieuChinh_K1nc",
                table: "ChiTietBieuGia",
                type: "float",
                nullable: false,
                oldClrType: typeof(decimal),
                oldType: "numeric(18,1)");

            migrationBuilder.AlterColumn<double>(
                name: "DonGia_VL",
                table: "ChiTietBieuGia",
                type: "float",
                nullable: false,
                oldClrType: typeof(decimal),
                oldType: "numeric(18,1)");

            migrationBuilder.AlterColumn<double>(
                name: "DonGia_NC",
                table: "ChiTietBieuGia",
                type: "float",
                nullable: false,
                oldClrType: typeof(decimal),
                oldType: "numeric(18,1)");

            migrationBuilder.AlterColumn<double>(
                name: "DonGia_MTC",
                table: "ChiTietBieuGia",
                type: "float",
                nullable: false,
                oldClrType: typeof(decimal),
                oldType: "numeric(18,1)");

            migrationBuilder.AddForeignKey(
                name: "FK_ChiTietBieuGia_DM_BieuGia_BieugiaID",
                table: "ChiTietBieuGia",
                column: "BieugiaID",
                principalTable: "DM_BieuGia",
                principalColumn: "Id");
        }
    }
}
