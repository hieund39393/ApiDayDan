using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Authentication.Infrastructure.Migrations
{
    public partial class UP_config : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_DonGiaMTC_DM_MTC_IdMTC",
                table: "DonGiaMTC");

            migrationBuilder.DropForeignKey(
                name: "FK_DonGiaMTC_CapNgam_DM_MTC_CapNgam_IdMTC",
                table: "DonGiaMTC_CapNgam");

            migrationBuilder.DropIndex(
                name: "IX_DonGiaMTC_CapNgam_IdMTC",
                table: "DonGiaMTC_CapNgam");

            migrationBuilder.DropIndex(
                name: "IX_DonGiaMTC_IdMTC",
                table: "DonGiaMTC");

            migrationBuilder.AlterColumn<string>(
                name: "VanBan",
                table: "DonGiaMTC_CapNgam",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(50)",
                oldMaxLength: 50,
                oldNullable: true);

            migrationBuilder.AlterColumn<bool>(
                name: "IsDeleted",
                table: "DonGiaMTC_CapNgam",
                type: "bit",
                nullable: false,
                comment: "Cờ xóa",
                oldClrType: typeof(bool),
                oldType: "bit",
                oldDefaultValue: false,
                oldComment: "Cờ xóa");

            migrationBuilder.AlterColumn<decimal>(
                name: "DonGia",
                table: "DonGiaMTC_CapNgam",
                type: "decimal(18,2)",
                nullable: false,
                oldClrType: typeof(decimal),
                oldType: "numeric(18,1)");

            migrationBuilder.AddColumn<Guid>(
                name: "DM_MTC_CapNgamId",
                table: "DonGiaMTC_CapNgam",
                type: "uniqueidentifier",
                nullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "VanBan",
                table: "DonGiaMTC",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(50)",
                oldMaxLength: 50,
                oldNullable: true);

            migrationBuilder.AlterColumn<bool>(
                name: "IsDeleted",
                table: "DonGiaMTC",
                type: "bit",
                nullable: false,
                comment: "Cờ xóa",
                oldClrType: typeof(bool),
                oldType: "bit",
                oldDefaultValue: false,
                oldComment: "Cờ xóa");

            migrationBuilder.AlterColumn<decimal>(
                name: "DonGia",
                table: "DonGiaMTC",
                type: "decimal(18,2)",
                nullable: false,
                oldClrType: typeof(decimal),
                oldType: "numeric(18,1)");

            migrationBuilder.AddColumn<Guid>(
                name: "DM_MTCId",
                table: "DonGiaMTC",
                type: "uniqueidentifier",
                nullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "TenMTC",
                table: "DM_MTC_CapNgam",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(200)",
                oldMaxLength: 200,
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "MaMTC",
                table: "DM_MTC_CapNgam",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(50)",
                oldMaxLength: 50,
                oldNullable: true);

            migrationBuilder.AlterColumn<bool>(
                name: "IsDeleted",
                table: "DM_MTC_CapNgam",
                type: "bit",
                nullable: false,
                comment: "Cờ xóa",
                oldClrType: typeof(bool),
                oldType: "bit",
                oldDefaultValue: false,
                oldComment: "Cờ xóa");

            migrationBuilder.AlterColumn<string>(
                name: "DonViTinh",
                table: "DM_MTC_CapNgam",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(50)",
                oldMaxLength: 50,
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "TenMayThiCong",
                table: "DM_MTC",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(200)",
                oldMaxLength: 200,
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "MaMTC",
                table: "DM_MTC",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(50)",
                oldMaxLength: 50,
                oldNullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_DonGiaMTC_CapNgam_DM_MTC_CapNgamId",
                table: "DonGiaMTC_CapNgam",
                column: "DM_MTC_CapNgamId");

            migrationBuilder.CreateIndex(
                name: "IX_DonGiaMTC_DM_MTCId",
                table: "DonGiaMTC",
                column: "DM_MTCId");

            migrationBuilder.AddForeignKey(
                name: "FK_DonGiaMTC_DM_MTC_DM_MTCId",
                table: "DonGiaMTC",
                column: "DM_MTCId",
                principalTable: "DM_MTC",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_DonGiaMTC_CapNgam_DM_MTC_CapNgam_DM_MTC_CapNgamId",
                table: "DonGiaMTC_CapNgam",
                column: "DM_MTC_CapNgamId",
                principalTable: "DM_MTC_CapNgam",
                principalColumn: "Id");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_DonGiaMTC_DM_MTC_DM_MTCId",
                table: "DonGiaMTC");

            migrationBuilder.DropForeignKey(
                name: "FK_DonGiaMTC_CapNgam_DM_MTC_CapNgam_DM_MTC_CapNgamId",
                table: "DonGiaMTC_CapNgam");

            migrationBuilder.DropIndex(
                name: "IX_DonGiaMTC_CapNgam_DM_MTC_CapNgamId",
                table: "DonGiaMTC_CapNgam");

            migrationBuilder.DropIndex(
                name: "IX_DonGiaMTC_DM_MTCId",
                table: "DonGiaMTC");

            migrationBuilder.DropColumn(
                name: "DM_MTC_CapNgamId",
                table: "DonGiaMTC_CapNgam");

            migrationBuilder.DropColumn(
                name: "DM_MTCId",
                table: "DonGiaMTC");

            migrationBuilder.AlterColumn<string>(
                name: "VanBan",
                table: "DonGiaMTC_CapNgam",
                type: "nvarchar(50)",
                maxLength: 50,
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.AlterColumn<bool>(
                name: "IsDeleted",
                table: "DonGiaMTC_CapNgam",
                type: "bit",
                nullable: false,
                defaultValue: false,
                comment: "Cờ xóa",
                oldClrType: typeof(bool),
                oldType: "bit",
                oldComment: "Cờ xóa");

            migrationBuilder.AlterColumn<decimal>(
                name: "DonGia",
                table: "DonGiaMTC_CapNgam",
                type: "numeric(18,1)",
                nullable: false,
                oldClrType: typeof(decimal),
                oldType: "decimal(18,2)");

            migrationBuilder.AlterColumn<string>(
                name: "VanBan",
                table: "DonGiaMTC",
                type: "nvarchar(50)",
                maxLength: 50,
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.AlterColumn<bool>(
                name: "IsDeleted",
                table: "DonGiaMTC",
                type: "bit",
                nullable: false,
                defaultValue: false,
                comment: "Cờ xóa",
                oldClrType: typeof(bool),
                oldType: "bit",
                oldComment: "Cờ xóa");

            migrationBuilder.AlterColumn<decimal>(
                name: "DonGia",
                table: "DonGiaMTC",
                type: "numeric(18,1)",
                nullable: false,
                oldClrType: typeof(decimal),
                oldType: "decimal(18,2)");

            migrationBuilder.AlterColumn<string>(
                name: "TenMTC",
                table: "DM_MTC_CapNgam",
                type: "nvarchar(200)",
                maxLength: 200,
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "MaMTC",
                table: "DM_MTC_CapNgam",
                type: "nvarchar(50)",
                maxLength: 50,
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.AlterColumn<bool>(
                name: "IsDeleted",
                table: "DM_MTC_CapNgam",
                type: "bit",
                nullable: false,
                defaultValue: false,
                comment: "Cờ xóa",
                oldClrType: typeof(bool),
                oldType: "bit",
                oldComment: "Cờ xóa");

            migrationBuilder.AlterColumn<string>(
                name: "DonViTinh",
                table: "DM_MTC_CapNgam",
                type: "nvarchar(50)",
                maxLength: 50,
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "TenMayThiCong",
                table: "DM_MTC",
                type: "nvarchar(200)",
                maxLength: 200,
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "MaMTC",
                table: "DM_MTC",
                type: "nvarchar(50)",
                maxLength: 50,
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_DonGiaMTC_CapNgam_IdMTC",
                table: "DonGiaMTC_CapNgam",
                column: "IdMTC");

            migrationBuilder.CreateIndex(
                name: "IX_DonGiaMTC_IdMTC",
                table: "DonGiaMTC",
                column: "IdMTC");

            migrationBuilder.AddForeignKey(
                name: "FK_DonGiaMTC_DM_MTC_IdMTC",
                table: "DonGiaMTC",
                column: "IdMTC",
                principalTable: "DM_MTC",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_DonGiaMTC_CapNgam_DM_MTC_CapNgam_IdMTC",
                table: "DonGiaMTC_CapNgam",
                column: "IdMTC",
                principalTable: "DM_MTC_CapNgam",
                principalColumn: "Id");
        }
    }
}
