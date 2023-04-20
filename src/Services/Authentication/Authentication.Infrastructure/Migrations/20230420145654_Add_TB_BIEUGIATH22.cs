using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Authentication.Infrastructure.Migrations
{
    public partial class Add_TB_BIEUGIATH22 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_BieuGiaTongHop_DM_BieuGia_DM_BieuGiaId",
                table: "BieuGiaTongHop");

            migrationBuilder.DropIndex(
                name: "IX_BieuGiaTongHop_DM_BieuGiaId",
                table: "BieuGiaTongHop");

            migrationBuilder.DropColumn(
                name: "DM_BieuGiaId",
                table: "BieuGiaTongHop");

            migrationBuilder.AlterColumn<bool>(
                name: "IsDeleted",
                table: "BieuGiaTongHop",
                type: "bit",
                nullable: false,
                defaultValue: false,
                comment: "Cờ xóa",
                oldClrType: typeof(bool),
                oldType: "bit",
                oldComment: "Cờ xóa");

            migrationBuilder.AlterColumn<decimal>(
                name: "DonGia",
                table: "BieuGiaTongHop",
                type: "numeric(18,2)",
                nullable: false,
                oldClrType: typeof(decimal),
                oldType: "decimal(18,2)");

            migrationBuilder.CreateIndex(
                name: "IX_BieuGiaTongHop_IdBieuGia",
                table: "BieuGiaTongHop",
                column: "IdBieuGia");

            migrationBuilder.AddForeignKey(
                name: "FK_BieuGiaTongHop_DM_BieuGia_IdBieuGia",
                table: "BieuGiaTongHop",
                column: "IdBieuGia",
                principalTable: "DM_BieuGia",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_BieuGiaTongHop_DM_BieuGia_IdBieuGia",
                table: "BieuGiaTongHop");

            migrationBuilder.DropIndex(
                name: "IX_BieuGiaTongHop_IdBieuGia",
                table: "BieuGiaTongHop");

            migrationBuilder.AlterColumn<bool>(
                name: "IsDeleted",
                table: "BieuGiaTongHop",
                type: "bit",
                nullable: false,
                comment: "Cờ xóa",
                oldClrType: typeof(bool),
                oldType: "bit",
                oldDefaultValue: false,
                oldComment: "Cờ xóa");

            migrationBuilder.AlterColumn<decimal>(
                name: "DonGia",
                table: "BieuGiaTongHop",
                type: "decimal(18,2)",
                nullable: false,
                oldClrType: typeof(decimal),
                oldType: "numeric(18,2)");

            migrationBuilder.AddColumn<Guid>(
                name: "DM_BieuGiaId",
                table: "BieuGiaTongHop",
                type: "uniqueidentifier",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_BieuGiaTongHop_DM_BieuGiaId",
                table: "BieuGiaTongHop",
                column: "DM_BieuGiaId");

            migrationBuilder.AddForeignKey(
                name: "FK_BieuGiaTongHop_DM_BieuGia_DM_BieuGiaId",
                table: "BieuGiaTongHop",
                column: "DM_BieuGiaId",
                principalTable: "DM_BieuGia",
                principalColumn: "Id");
        }
    }
}
