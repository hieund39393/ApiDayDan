using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Authentication.Infrastructure.Migrations
{
    public partial class DB_intit_23 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_DonGiaNhanCong_DM_KhuVuc_IdKhuVuc",
                table: "DonGiaNhanCong");

            migrationBuilder.DropForeignKey(
                name: "FK_DonGiaNhanCong_CapNgam_DM_KhuVuc_IdKhuVuc",
                table: "DonGiaNhanCong_CapNgam");

            migrationBuilder.DropColumn(
                name: "CapBac",
                table: "DonGiaNhanCong_CapNgam");

            migrationBuilder.DropColumn(
                name: "HeSo",
                table: "DonGiaNhanCong_CapNgam");

            migrationBuilder.DropColumn(
                name: "CapBac",
                table: "DonGiaNhanCong");

            migrationBuilder.DropColumn(
                name: "HeSo",
                table: "DonGiaNhanCong");

            migrationBuilder.RenameColumn(
                name: "IdKhuVuc",
                table: "DonGiaNhanCong_CapNgam",
                newName: "IdNhanCong");

            migrationBuilder.RenameIndex(
                name: "IX_DonGiaNhanCong_CapNgam_IdKhuVuc",
                table: "DonGiaNhanCong_CapNgam",
                newName: "IX_DonGiaNhanCong_CapNgam_IdNhanCong");

            migrationBuilder.RenameColumn(
                name: "IdKhuVuc",
                table: "DonGiaNhanCong",
                newName: "IdNhanCong");

            migrationBuilder.RenameIndex(
                name: "IX_DonGiaNhanCong_IdKhuVuc",
                table: "DonGiaNhanCong",
                newName: "IX_DonGiaNhanCong_IdNhanCong");

            migrationBuilder.CreateTable(
                name: "DM_NhanCong",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false, comment: "Id bảng, khóa chính"),
                    CapBac = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    HeSo = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    IdKhuVuc = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    KhuVucId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false, comment: "Cờ xóa"),
                    CreatedDate = table.Column<DateTime>(type: "datetime2", nullable: false, comment: "Ngày tạo"),
                    CreatedBy = table.Column<Guid>(type: "uniqueidentifier", nullable: true, comment: "Mã người tạo"),
                    UpdatedDate = table.Column<DateTime>(type: "datetime2", nullable: true, comment: "Ngày cập nhật"),
                    UpdatedBy = table.Column<Guid>(type: "uniqueidentifier", nullable: true, comment: "Mã người cập nhật")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DM_NhanCong", x => x.Id);
                    table.ForeignKey(
                        name: "FK_DM_NhanCong_DM_KhuVuc_KhuVucId",
                        column: x => x.KhuVucId,
                        principalTable: "DM_KhuVuc",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "DM_NhanCong_CapNgam",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false, comment: "Id bảng, khóa chính"),
                    CapBac = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    HeSo = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    IdKhuVuc = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    KhuVucId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false, comment: "Cờ xóa"),
                    CreatedDate = table.Column<DateTime>(type: "datetime2", nullable: false, comment: "Ngày tạo"),
                    CreatedBy = table.Column<Guid>(type: "uniqueidentifier", nullable: true, comment: "Mã người tạo"),
                    UpdatedDate = table.Column<DateTime>(type: "datetime2", nullable: true, comment: "Ngày cập nhật"),
                    UpdatedBy = table.Column<Guid>(type: "uniqueidentifier", nullable: true, comment: "Mã người cập nhật")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DM_NhanCong_CapNgam", x => x.Id);
                    table.ForeignKey(
                        name: "FK_DM_NhanCong_CapNgam_DM_KhuVuc_KhuVucId",
                        column: x => x.KhuVucId,
                        principalTable: "DM_KhuVuc",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateIndex(
                name: "IX_DM_NhanCong_KhuVucId",
                table: "DM_NhanCong",
                column: "KhuVucId");

            migrationBuilder.CreateIndex(
                name: "IX_DM_NhanCong_CapNgam_KhuVucId",
                table: "DM_NhanCong_CapNgam",
                column: "KhuVucId");

            migrationBuilder.AddForeignKey(
                name: "FK_DonGiaNhanCong_DM_NhanCong_IdNhanCong",
                table: "DonGiaNhanCong",
                column: "IdNhanCong",
                principalTable: "DM_NhanCong",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_DonGiaNhanCong_CapNgam_DM_NhanCong_CapNgam_IdNhanCong",
                table: "DonGiaNhanCong_CapNgam",
                column: "IdNhanCong",
                principalTable: "DM_NhanCong_CapNgam",
                principalColumn: "Id");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_DonGiaNhanCong_DM_NhanCong_IdNhanCong",
                table: "DonGiaNhanCong");

            migrationBuilder.DropForeignKey(
                name: "FK_DonGiaNhanCong_CapNgam_DM_NhanCong_CapNgam_IdNhanCong",
                table: "DonGiaNhanCong_CapNgam");

            migrationBuilder.DropTable(
                name: "DM_NhanCong");

            migrationBuilder.DropTable(
                name: "DM_NhanCong_CapNgam");

            migrationBuilder.RenameColumn(
                name: "IdNhanCong",
                table: "DonGiaNhanCong_CapNgam",
                newName: "IdKhuVuc");

            migrationBuilder.RenameIndex(
                name: "IX_DonGiaNhanCong_CapNgam_IdNhanCong",
                table: "DonGiaNhanCong_CapNgam",
                newName: "IX_DonGiaNhanCong_CapNgam_IdKhuVuc");

            migrationBuilder.RenameColumn(
                name: "IdNhanCong",
                table: "DonGiaNhanCong",
                newName: "IdKhuVuc");

            migrationBuilder.RenameIndex(
                name: "IX_DonGiaNhanCong_IdNhanCong",
                table: "DonGiaNhanCong",
                newName: "IX_DonGiaNhanCong_IdKhuVuc");

            migrationBuilder.AddColumn<string>(
                name: "CapBac",
                table: "DonGiaNhanCong_CapNgam",
                type: "nvarchar(50)",
                maxLength: 50,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "HeSo",
                table: "DonGiaNhanCong_CapNgam",
                type: "nvarchar(50)",
                maxLength: 50,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "CapBac",
                table: "DonGiaNhanCong",
                type: "nvarchar(50)",
                maxLength: 50,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "HeSo",
                table: "DonGiaNhanCong",
                type: "nvarchar(50)",
                maxLength: 50,
                nullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_DonGiaNhanCong_DM_KhuVuc_IdKhuVuc",
                table: "DonGiaNhanCong",
                column: "IdKhuVuc",
                principalTable: "DM_KhuVuc",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_DonGiaNhanCong_CapNgam_DM_KhuVuc_IdKhuVuc",
                table: "DonGiaNhanCong_CapNgam",
                column: "IdKhuVuc",
                principalTable: "DM_KhuVuc",
                principalColumn: "Id");
        }
    }
}
