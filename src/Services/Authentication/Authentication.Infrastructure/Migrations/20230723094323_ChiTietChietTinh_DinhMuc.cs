using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Authentication.Infrastructure.Migrations
{
    public partial class ChiTietChietTinh_DinhMuc : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ChietTinhChiTiet_DonGiaChietTinh_DonGiaChietTinhId",
                table: "ChietTinhChiTiet");

            migrationBuilder.DropIndex(
                name: "IX_ChietTinhChiTiet_DonGiaChietTinhId",
                table: "ChietTinhChiTiet");

            migrationBuilder.DropColumn(
                name: "DonGiaChietTinhId",
                table: "ChietTinhChiTiet");

            migrationBuilder.AlterColumn<bool>(
                name: "IsDeleted",
                table: "ChietTinhChiTiet",
                type: "bit",
                nullable: false,
                defaultValue: false,
                comment: "Cờ xóa",
                oldClrType: typeof(bool),
                oldType: "bit",
                oldComment: "Cờ xóa");

            migrationBuilder.AlterColumn<decimal>(
                name: "DinhMuc",
                table: "ChietTinhChiTiet",
                type: "numeric(18,1)",
                nullable: true,
                oldClrType: typeof(decimal),
                oldType: "decimal(18,2)",
                oldNullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_ChietTinhChiTiet_IdDonGiaChietTinh",
                table: "ChietTinhChiTiet",
                column: "IdDonGiaChietTinh");

            migrationBuilder.AddForeignKey(
                name: "FK_ChietTinhChiTiet_DonGiaChietTinh_IdDonGiaChietTinh",
                table: "ChietTinhChiTiet",
                column: "IdDonGiaChietTinh",
                principalTable: "DonGiaChietTinh",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ChietTinhChiTiet_DonGiaChietTinh_IdDonGiaChietTinh",
                table: "ChietTinhChiTiet");

            migrationBuilder.DropIndex(
                name: "IX_ChietTinhChiTiet_IdDonGiaChietTinh",
                table: "ChietTinhChiTiet");

            migrationBuilder.AlterColumn<bool>(
                name: "IsDeleted",
                table: "ChietTinhChiTiet",
                type: "bit",
                nullable: false,
                comment: "Cờ xóa",
                oldClrType: typeof(bool),
                oldType: "bit",
                oldDefaultValue: false,
                oldComment: "Cờ xóa");

            migrationBuilder.AlterColumn<decimal>(
                name: "DinhMuc",
                table: "ChietTinhChiTiet",
                type: "decimal(18,2)",
                nullable: true,
                oldClrType: typeof(decimal),
                oldType: "numeric(18,1)",
                oldNullable: true);

            migrationBuilder.AddColumn<Guid>(
                name: "DonGiaChietTinhId",
                table: "ChietTinhChiTiet",
                type: "uniqueidentifier",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_ChietTinhChiTiet_DonGiaChietTinhId",
                table: "ChietTinhChiTiet",
                column: "DonGiaChietTinhId");

            migrationBuilder.AddForeignKey(
                name: "FK_ChietTinhChiTiet_DonGiaChietTinh_DonGiaChietTinhId",
                table: "ChietTinhChiTiet",
                column: "DonGiaChietTinhId",
                principalTable: "DonGiaChietTinh",
                principalColumn: "Id");
        }
    }
}
