using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Authentication.Infrastructure.Migrations
{
    public partial class Update_DGCT : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_DonGiaChietTinh_DM_VatLieuChietTinh_IdVatLieuChietTinh",
                table: "DonGiaChietTinh");

            migrationBuilder.DropIndex(
                name: "IX_DonGiaChietTinh_IdVatLieuChietTinh",
                table: "DonGiaChietTinh");

            migrationBuilder.DropColumn(
                name: "DonGia",
                table: "DonGiaChietTinh");

            migrationBuilder.DropColumn(
                name: "IdPhanLoai",
                table: "DonGiaChietTinh");

            migrationBuilder.DropColumn(
                name: "TongGia",
                table: "DonGiaChietTinh");

            migrationBuilder.RenameColumn(
                name: "IdVatLieuChietTinh",
                table: "DonGiaChietTinh",
                newName: "IdCongViec");

            migrationBuilder.AddColumn<Guid>(
                name: "DM_VatLieuChietTinhId",
                table: "DonGiaChietTinh",
                type: "uniqueidentifier",
                nullable: true);

            migrationBuilder.AddColumn<decimal>(
                name: "DonGiaMTC",
                table: "DonGiaChietTinh",
                type: "numeric(18,1)",
                nullable: true);

            migrationBuilder.AddColumn<decimal>(
                name: "DonGiaNhanCong",
                table: "DonGiaChietTinh",
                type: "numeric(18,1)",
                nullable: true);

            migrationBuilder.AddColumn<decimal>(
                name: "DonGiaVatLieu",
                table: "DonGiaChietTinh",
                type: "numeric(18,1)",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_DonGiaChietTinh_DM_VatLieuChietTinhId",
                table: "DonGiaChietTinh",
                column: "DM_VatLieuChietTinhId");

            migrationBuilder.CreateIndex(
                name: "IX_DonGiaChietTinh_IdCongViec",
                table: "DonGiaChietTinh",
                column: "IdCongViec",
                unique: true,
                filter: "[IdCongViec] IS NOT NULL");

            migrationBuilder.AddForeignKey(
                name: "FK_DonGiaChietTinh_DM_CongViec_IdCongViec",
                table: "DonGiaChietTinh",
                column: "IdCongViec",
                principalTable: "DM_CongViec",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_DonGiaChietTinh_DM_VatLieuChietTinh_DM_VatLieuChietTinhId",
                table: "DonGiaChietTinh",
                column: "DM_VatLieuChietTinhId",
                principalTable: "DM_VatLieuChietTinh",
                principalColumn: "Id");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_DonGiaChietTinh_DM_CongViec_IdCongViec",
                table: "DonGiaChietTinh");

            migrationBuilder.DropForeignKey(
                name: "FK_DonGiaChietTinh_DM_VatLieuChietTinh_DM_VatLieuChietTinhId",
                table: "DonGiaChietTinh");

            migrationBuilder.DropIndex(
                name: "IX_DonGiaChietTinh_DM_VatLieuChietTinhId",
                table: "DonGiaChietTinh");

            migrationBuilder.DropIndex(
                name: "IX_DonGiaChietTinh_IdCongViec",
                table: "DonGiaChietTinh");

            migrationBuilder.DropColumn(
                name: "DM_VatLieuChietTinhId",
                table: "DonGiaChietTinh");

            migrationBuilder.DropColumn(
                name: "DonGiaMTC",
                table: "DonGiaChietTinh");

            migrationBuilder.DropColumn(
                name: "DonGiaNhanCong",
                table: "DonGiaChietTinh");

            migrationBuilder.DropColumn(
                name: "DonGiaVatLieu",
                table: "DonGiaChietTinh");

            migrationBuilder.RenameColumn(
                name: "IdCongViec",
                table: "DonGiaChietTinh",
                newName: "IdVatLieuChietTinh");

            migrationBuilder.AddColumn<decimal>(
                name: "DonGia",
                table: "DonGiaChietTinh",
                type: "numeric(18,1)",
                nullable: false,
                defaultValue: 0m);

            migrationBuilder.AddColumn<int>(
                name: "IdPhanLoai",
                table: "DonGiaChietTinh",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<decimal>(
                name: "TongGia",
                table: "DonGiaChietTinh",
                type: "decimal(18,2)",
                nullable: false,
                defaultValue: 0m);

            migrationBuilder.CreateIndex(
                name: "IX_DonGiaChietTinh_IdVatLieuChietTinh",
                table: "DonGiaChietTinh",
                column: "IdVatLieuChietTinh");

            migrationBuilder.AddForeignKey(
                name: "FK_DonGiaChietTinh_DM_VatLieuChietTinh_IdVatLieuChietTinh",
                table: "DonGiaChietTinh",
                column: "IdVatLieuChietTinh",
                principalTable: "DM_VatLieuChietTinh",
                principalColumn: "Id");
        }
    }
}
