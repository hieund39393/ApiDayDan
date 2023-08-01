using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Authentication.Infrastructure.Migrations
{
    public partial class Db_Add_ChieTTinhCapNgam : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ChietTinhChiTiet_CapNgam_DonGiaChietTinh_CapNgam_DonGiaChietTinh_CapNgamId",
                table: "ChietTinhChiTiet_CapNgam");

            migrationBuilder.DropIndex(
                name: "IX_ChietTinhChiTiet_CapNgam_DonGiaChietTinh_CapNgamId",
                table: "ChietTinhChiTiet_CapNgam");

            migrationBuilder.DropColumn(
                name: "DonGiaChietTinh_CapNgamId",
                table: "ChietTinhChiTiet_CapNgam");

            migrationBuilder.AlterColumn<bool>(
                name: "IsDeleted",
                table: "ChietTinhChiTiet_CapNgam",
                type: "bit",
                nullable: false,
                defaultValue: false,
                comment: "Cờ xóa",
                oldClrType: typeof(bool),
                oldType: "bit",
                oldComment: "Cờ xóa");

            migrationBuilder.AlterColumn<decimal>(
                name: "DinhMuc",
                table: "ChietTinhChiTiet_CapNgam",
                type: "numeric(18,2)",
                nullable: true,
                oldClrType: typeof(decimal),
                oldType: "decimal(18,2)",
                oldNullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_ChietTinhChiTiet_CapNgam_IdDonGiaChietTinh",
                table: "ChietTinhChiTiet_CapNgam",
                column: "IdDonGiaChietTinh");

            migrationBuilder.AddForeignKey(
                name: "FK_ChietTinhChiTiet_CapNgam_DonGiaChietTinh_CapNgam_IdDonGiaChietTinh",
                table: "ChietTinhChiTiet_CapNgam",
                column: "IdDonGiaChietTinh",
                principalTable: "DonGiaChietTinh_CapNgam",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ChietTinhChiTiet_CapNgam_DonGiaChietTinh_CapNgam_IdDonGiaChietTinh",
                table: "ChietTinhChiTiet_CapNgam");

            migrationBuilder.DropIndex(
                name: "IX_ChietTinhChiTiet_CapNgam_IdDonGiaChietTinh",
                table: "ChietTinhChiTiet_CapNgam");

            migrationBuilder.AlterColumn<bool>(
                name: "IsDeleted",
                table: "ChietTinhChiTiet_CapNgam",
                type: "bit",
                nullable: false,
                comment: "Cờ xóa",
                oldClrType: typeof(bool),
                oldType: "bit",
                oldDefaultValue: false,
                oldComment: "Cờ xóa");

            migrationBuilder.AlterColumn<decimal>(
                name: "DinhMuc",
                table: "ChietTinhChiTiet_CapNgam",
                type: "decimal(18,2)",
                nullable: true,
                oldClrType: typeof(decimal),
                oldType: "numeric(18,2)",
                oldNullable: true);

            migrationBuilder.AddColumn<Guid>(
                name: "DonGiaChietTinh_CapNgamId",
                table: "ChietTinhChiTiet_CapNgam",
                type: "uniqueidentifier",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_ChietTinhChiTiet_CapNgam_DonGiaChietTinh_CapNgamId",
                table: "ChietTinhChiTiet_CapNgam",
                column: "DonGiaChietTinh_CapNgamId");

            migrationBuilder.AddForeignKey(
                name: "FK_ChietTinhChiTiet_CapNgam_DonGiaChietTinh_CapNgam_DonGiaChietTinh_CapNgamId",
                table: "ChietTinhChiTiet_CapNgam",
                column: "DonGiaChietTinh_CapNgamId",
                principalTable: "DonGiaChietTinh_CapNgam",
                principalColumn: "Id");
        }
    }
}
