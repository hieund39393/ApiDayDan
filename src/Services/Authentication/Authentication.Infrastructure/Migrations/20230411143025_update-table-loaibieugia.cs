using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Authentication.Infrastructure.Migrations
{
    public partial class updatetableloaibieugia : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "TenBieuGia",
                table: "DM_LoaiBieuGia",
                newName: "TenLoaiBieuGia");

            migrationBuilder.RenameColumn(
                name: "MaBieuGia",
                table: "DM_LoaiBieuGia",
                newName: "MaLoaiBieuGia");

            migrationBuilder.AddColumn<Guid>(
                name: "KhuVucID",
                table: "DM_LoaiBieuGia",
                type: "uniqueidentifier",
                nullable: true);

            migrationBuilder.AddColumn<Guid>(
                name: "VungID",
                table: "DM_LoaiBieuGia",
                type: "uniqueidentifier",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_DM_LoaiBieuGia_KhuVucID",
                table: "DM_LoaiBieuGia",
                column: "KhuVucID");

            migrationBuilder.CreateIndex(
                name: "IX_DM_LoaiBieuGia_VungID",
                table: "DM_LoaiBieuGia",
                column: "VungID");

            migrationBuilder.AddForeignKey(
                name: "FK_DM_LoaiBieuGia_DM_KhuVuc_KhuVucID",
                table: "DM_LoaiBieuGia",
                column: "KhuVucID",
                principalTable: "DM_KhuVuc",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_DM_LoaiBieuGia_DM_Vung_VungID",
                table: "DM_LoaiBieuGia",
                column: "VungID",
                principalTable: "DM_Vung",
                principalColumn: "Id");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_DM_LoaiBieuGia_DM_KhuVuc_KhuVucID",
                table: "DM_LoaiBieuGia");

            migrationBuilder.DropForeignKey(
                name: "FK_DM_LoaiBieuGia_DM_Vung_VungID",
                table: "DM_LoaiBieuGia");

            migrationBuilder.DropIndex(
                name: "IX_DM_LoaiBieuGia_KhuVucID",
                table: "DM_LoaiBieuGia");

            migrationBuilder.DropIndex(
                name: "IX_DM_LoaiBieuGia_VungID",
                table: "DM_LoaiBieuGia");

            migrationBuilder.DropColumn(
                name: "KhuVucID",
                table: "DM_LoaiBieuGia");

            migrationBuilder.DropColumn(
                name: "VungID",
                table: "DM_LoaiBieuGia");

            migrationBuilder.RenameColumn(
                name: "TenLoaiBieuGia",
                table: "DM_LoaiBieuGia",
                newName: "TenBieuGia");

            migrationBuilder.RenameColumn(
                name: "MaLoaiBieuGia",
                table: "DM_LoaiBieuGia",
                newName: "MaBieuGia");
        }
    }
}
