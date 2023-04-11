using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Authentication.Infrastructure.Migrations
{
    public partial class newtablebieugiacongviec5 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ChiTietBieuGia_DM_BieuGia_DM_BieuGiaId",
                table: "ChiTietBieuGia");

            migrationBuilder.DropIndex(
                name: "IX_ChiTietBieuGia_DM_BieuGiaId",
                table: "ChiTietBieuGia");

            migrationBuilder.DropColumn(
                name: "DM_BieuGiaId",
                table: "ChiTietBieuGia");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<Guid>(
                name: "DM_BieuGiaId",
                table: "ChiTietBieuGia",
                type: "uniqueidentifier",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_ChiTietBieuGia_DM_BieuGiaId",
                table: "ChiTietBieuGia",
                column: "DM_BieuGiaId");

            migrationBuilder.AddForeignKey(
                name: "FK_ChiTietBieuGia_DM_BieuGia_DM_BieuGiaId",
                table: "ChiTietBieuGia",
                column: "DM_BieuGiaId",
                principalTable: "DM_BieuGia",
                principalColumn: "Id");
        }
    }
}
