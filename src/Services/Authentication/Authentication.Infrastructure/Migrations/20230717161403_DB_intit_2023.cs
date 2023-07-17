using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Authentication.Infrastructure.Migrations
{
    public partial class DB_intit_2023 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_DM_NhanCong_DM_KhuVuc_KhuVucId",
                table: "DM_NhanCong");

            migrationBuilder.DropForeignKey(
                name: "FK_DM_NhanCong_CapNgam_DM_KhuVuc_KhuVucId",
                table: "DM_NhanCong_CapNgam");

            migrationBuilder.DropIndex(
                name: "IX_DM_NhanCong_CapNgam_KhuVucId",
                table: "DM_NhanCong_CapNgam");

            migrationBuilder.DropIndex(
                name: "IX_DM_NhanCong_KhuVucId",
                table: "DM_NhanCong");

            migrationBuilder.DropColumn(
                name: "KhuVucId",
                table: "DM_NhanCong_CapNgam");

            migrationBuilder.DropColumn(
                name: "KhuVucId",
                table: "DM_NhanCong");

            migrationBuilder.AlterColumn<bool>(
                name: "IsDeleted",
                table: "DM_NhanCong_CapNgam",
                type: "bit",
                nullable: false,
                defaultValue: false,
                comment: "Cờ xóa",
                oldClrType: typeof(bool),
                oldType: "bit",
                oldComment: "Cờ xóa");

            migrationBuilder.AlterColumn<string>(
                name: "HeSo",
                table: "DM_NhanCong_CapNgam",
                type: "nvarchar(10)",
                maxLength: 10,
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "CapBac",
                table: "DM_NhanCong_CapNgam",
                type: "nvarchar(10)",
                maxLength: 10,
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.AlterColumn<bool>(
                name: "IsDeleted",
                table: "DM_NhanCong",
                type: "bit",
                nullable: false,
                defaultValue: false,
                comment: "Cờ xóa",
                oldClrType: typeof(bool),
                oldType: "bit",
                oldComment: "Cờ xóa");

            migrationBuilder.AlterColumn<string>(
                name: "HeSo",
                table: "DM_NhanCong",
                type: "nvarchar(10)",
                maxLength: 10,
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "CapBac",
                table: "DM_NhanCong",
                type: "nvarchar(10)",
                maxLength: 10,
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_DM_NhanCong_CapNgam_IdKhuVuc",
                table: "DM_NhanCong_CapNgam",
                column: "IdKhuVuc");

            migrationBuilder.CreateIndex(
                name: "IX_DM_NhanCong_IdKhuVuc",
                table: "DM_NhanCong",
                column: "IdKhuVuc");

            migrationBuilder.AddForeignKey(
                name: "FK_DM_NhanCong_DM_KhuVuc_IdKhuVuc",
                table: "DM_NhanCong",
                column: "IdKhuVuc",
                principalTable: "DM_KhuVuc",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_DM_NhanCong_CapNgam_DM_KhuVuc_IdKhuVuc",
                table: "DM_NhanCong_CapNgam",
                column: "IdKhuVuc",
                principalTable: "DM_KhuVuc",
                principalColumn: "Id");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_DM_NhanCong_DM_KhuVuc_IdKhuVuc",
                table: "DM_NhanCong");

            migrationBuilder.DropForeignKey(
                name: "FK_DM_NhanCong_CapNgam_DM_KhuVuc_IdKhuVuc",
                table: "DM_NhanCong_CapNgam");

            migrationBuilder.DropIndex(
                name: "IX_DM_NhanCong_CapNgam_IdKhuVuc",
                table: "DM_NhanCong_CapNgam");

            migrationBuilder.DropIndex(
                name: "IX_DM_NhanCong_IdKhuVuc",
                table: "DM_NhanCong");

            migrationBuilder.AlterColumn<bool>(
                name: "IsDeleted",
                table: "DM_NhanCong_CapNgam",
                type: "bit",
                nullable: false,
                comment: "Cờ xóa",
                oldClrType: typeof(bool),
                oldType: "bit",
                oldDefaultValue: false,
                oldComment: "Cờ xóa");

            migrationBuilder.AlterColumn<string>(
                name: "HeSo",
                table: "DM_NhanCong_CapNgam",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(10)",
                oldMaxLength: 10,
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "CapBac",
                table: "DM_NhanCong_CapNgam",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(10)",
                oldMaxLength: 10,
                oldNullable: true);

            migrationBuilder.AddColumn<Guid>(
                name: "KhuVucId",
                table: "DM_NhanCong_CapNgam",
                type: "uniqueidentifier",
                nullable: true);

            migrationBuilder.AlterColumn<bool>(
                name: "IsDeleted",
                table: "DM_NhanCong",
                type: "bit",
                nullable: false,
                comment: "Cờ xóa",
                oldClrType: typeof(bool),
                oldType: "bit",
                oldDefaultValue: false,
                oldComment: "Cờ xóa");

            migrationBuilder.AlterColumn<string>(
                name: "HeSo",
                table: "DM_NhanCong",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(10)",
                oldMaxLength: 10,
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "CapBac",
                table: "DM_NhanCong",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(10)",
                oldMaxLength: 10,
                oldNullable: true);

            migrationBuilder.AddColumn<Guid>(
                name: "KhuVucId",
                table: "DM_NhanCong",
                type: "uniqueidentifier",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_DM_NhanCong_CapNgam_KhuVucId",
                table: "DM_NhanCong_CapNgam",
                column: "KhuVucId");

            migrationBuilder.CreateIndex(
                name: "IX_DM_NhanCong_KhuVucId",
                table: "DM_NhanCong",
                column: "KhuVucId");

            migrationBuilder.AddForeignKey(
                name: "FK_DM_NhanCong_DM_KhuVuc_KhuVucId",
                table: "DM_NhanCong",
                column: "KhuVucId",
                principalTable: "DM_KhuVuc",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_DM_NhanCong_CapNgam_DM_KhuVuc_KhuVucId",
                table: "DM_NhanCong_CapNgam",
                column: "KhuVucId",
                principalTable: "DM_KhuVuc",
                principalColumn: "Id");
        }
    }
}
