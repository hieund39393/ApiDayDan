using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Authentication.Infrastructure.Migrations
{
    public partial class remove_FK_Vung_KV3 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_DM_LoaiBieuGia_DM_Vung_DM_VungId",
                table: "DM_LoaiBieuGia");

            migrationBuilder.DropForeignKey(
                name: "FK_DonGiaNhanCong_DM_Vung_IdVung",
                table: "DonGiaNhanCong");

            migrationBuilder.DropTable(
                name: "DM_Vung");

            migrationBuilder.DropIndex(
                name: "IX_DonGiaNhanCong_IdVung",
                table: "DonGiaNhanCong");

            migrationBuilder.DropIndex(
                name: "IX_DM_LoaiBieuGia_DM_VungId",
                table: "DM_LoaiBieuGia");

            migrationBuilder.DropColumn(
                name: "IdVung",
                table: "DonGiaNhanCong");

            migrationBuilder.DropColumn(
                name: "DM_VungId",
                table: "DM_LoaiBieuGia");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<Guid>(
                name: "IdVung",
                table: "DonGiaNhanCong",
                type: "uniqueidentifier",
                nullable: true);

            migrationBuilder.AddColumn<Guid>(
                name: "DM_VungId",
                table: "DM_LoaiBieuGia",
                type: "uniqueidentifier",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "DM_Vung",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false, comment: "Id bảng, khóa chính"),
                    CreatedBy = table.Column<Guid>(type: "uniqueidentifier", maxLength: 36, nullable: true, comment: "Mã người tạo"),
                    CreatedDate = table.Column<DateTime>(type: "datetime2", nullable: false, comment: "Ngày tạo"),
                    GhiChu = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: true),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false, defaultValue: false, comment: "Cờ xóa"),
                    TenVung = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: true),
                    UpdatedBy = table.Column<Guid>(type: "uniqueidentifier", maxLength: 36, nullable: true, comment: "Mã người cập nhật"),
                    UpdatedDate = table.Column<DateTime>(type: "datetime2", nullable: true, comment: "Ngày cập nhật")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DM_Vung", x => x.Id);
                });

            migrationBuilder.CreateIndex(
                name: "IX_DonGiaNhanCong_IdVung",
                table: "DonGiaNhanCong",
                column: "IdVung");

            migrationBuilder.CreateIndex(
                name: "IX_DM_LoaiBieuGia_DM_VungId",
                table: "DM_LoaiBieuGia",
                column: "DM_VungId");

            migrationBuilder.AddForeignKey(
                name: "FK_DM_LoaiBieuGia_DM_Vung_DM_VungId",
                table: "DM_LoaiBieuGia",
                column: "DM_VungId",
                principalTable: "DM_Vung",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_DonGiaNhanCong_DM_Vung_IdVung",
                table: "DonGiaNhanCong",
                column: "IdVung",
                principalTable: "DM_Vung",
                principalColumn: "Id");
        }
    }
}
