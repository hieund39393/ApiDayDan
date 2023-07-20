using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Authentication.Infrastructure.Migrations
{
    public partial class Delete_VLCT : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_DonGiaChietTinh_DM_VatLieuChietTinh_DM_VatLieuChietTinhId",
                table: "DonGiaChietTinh");

            migrationBuilder.DropTable(
                name: "DM_VatLieuChietTinh");

            migrationBuilder.DropIndex(
                name: "IX_DonGiaChietTinh_DM_VatLieuChietTinhId",
                table: "DonGiaChietTinh");

            migrationBuilder.DropColumn(
                name: "DM_VatLieuChietTinhId",
                table: "DonGiaChietTinh");

            migrationBuilder.AddColumn<int>(
                name: "VungKhuVuc",
                table: "DonGiaVatLieu_CapNgam",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "VungKhuVuc",
                table: "DonGiaNhanCong_CapNgam",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "VungKhuVuc",
                table: "DonGiaMTC_CapNgam",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "VungKhuVuc",
                table: "CauHinhChietTinh_CapNgam",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateTable(
                name: "DM_LoaiCap_CapNgam",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false, comment: "Id bảng, khóa chính"),
                    TenLoaiCap = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    MaLoaiCap = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    DonViTinh = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    VungKhuVuc = table.Column<int>(type: "int", nullable: false),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false, comment: "Cờ xóa"),
                    CreatedDate = table.Column<DateTime>(type: "datetime2", nullable: false, comment: "Ngày tạo"),
                    CreatedBy = table.Column<Guid>(type: "uniqueidentifier", nullable: true, comment: "Mã người tạo"),
                    UpdatedDate = table.Column<DateTime>(type: "datetime2", nullable: true, comment: "Ngày cập nhật"),
                    UpdatedBy = table.Column<Guid>(type: "uniqueidentifier", nullable: true, comment: "Mã người cập nhật")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DM_LoaiCap_CapNgam", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "DonGiaChietTinh_CapNgam",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false, comment: "Id bảng, khóa chính"),
                    IdCongViec = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    DonGiaVatLieu = table.Column<decimal>(type: "decimal(18,2)", nullable: true),
                    DonGiaNhanCong = table.Column<decimal>(type: "decimal(18,2)", nullable: true),
                    DonGiaMTC = table.Column<decimal>(type: "decimal(18,2)", nullable: true),
                    DM_CongViec_CapNgamId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    VungKhuVuc = table.Column<int>(type: "int", nullable: false),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false, comment: "Cờ xóa"),
                    CreatedDate = table.Column<DateTime>(type: "datetime2", nullable: false, comment: "Ngày tạo"),
                    CreatedBy = table.Column<Guid>(type: "uniqueidentifier", nullable: true, comment: "Mã người tạo"),
                    UpdatedDate = table.Column<DateTime>(type: "datetime2", nullable: true, comment: "Ngày cập nhật"),
                    UpdatedBy = table.Column<Guid>(type: "uniqueidentifier", nullable: true, comment: "Mã người cập nhật")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DonGiaChietTinh_CapNgam", x => x.Id);
                    table.ForeignKey(
                        name: "FK_DonGiaChietTinh_CapNgam_DM_CongViec_CapNgam_DM_CongViec_CapNgamId",
                        column: x => x.DM_CongViec_CapNgamId,
                        principalTable: "DM_CongViec_CapNgam",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "DonGiaCap_CapNgam",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false, comment: "Id bảng, khóa chính"),
                    IdLoaiCap = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    VanBan = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    DonGia = table.Column<decimal>(type: "numeric(18,1)", nullable: false),
                    VungKhuVuc = table.Column<int>(type: "int", nullable: false),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false, defaultValue: false, comment: "Cờ xóa"),
                    CreatedDate = table.Column<DateTime>(type: "datetime2", nullable: false, comment: "Ngày tạo"),
                    CreatedBy = table.Column<Guid>(type: "uniqueidentifier", maxLength: 36, nullable: true, comment: "Mã người tạo"),
                    UpdatedDate = table.Column<DateTime>(type: "datetime2", nullable: true, comment: "Ngày cập nhật"),
                    UpdatedBy = table.Column<Guid>(type: "uniqueidentifier", maxLength: 36, nullable: true, comment: "Mã người cập nhật")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DonGiaCap_CapNgam", x => x.Id);
                    table.ForeignKey(
                        name: "FK_DonGiaCap_CapNgam_DM_LoaiCap_CapNgam_IdLoaiCap",
                        column: x => x.IdLoaiCap,
                        principalTable: "DM_LoaiCap_CapNgam",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateIndex(
                name: "IX_DonGiaCap_CapNgam_IdLoaiCap",
                table: "DonGiaCap_CapNgam",
                column: "IdLoaiCap");

            migrationBuilder.CreateIndex(
                name: "IX_DonGiaChietTinh_CapNgam_DM_CongViec_CapNgamId",
                table: "DonGiaChietTinh_CapNgam",
                column: "DM_CongViec_CapNgamId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "DonGiaCap_CapNgam");

            migrationBuilder.DropTable(
                name: "DonGiaChietTinh_CapNgam");

            migrationBuilder.DropTable(
                name: "DM_LoaiCap_CapNgam");

            migrationBuilder.DropColumn(
                name: "VungKhuVuc",
                table: "DonGiaVatLieu_CapNgam");

            migrationBuilder.DropColumn(
                name: "VungKhuVuc",
                table: "DonGiaNhanCong_CapNgam");

            migrationBuilder.DropColumn(
                name: "VungKhuVuc",
                table: "DonGiaMTC_CapNgam");

            migrationBuilder.DropColumn(
                name: "VungKhuVuc",
                table: "CauHinhChietTinh_CapNgam");

            migrationBuilder.AddColumn<Guid>(
                name: "DM_VatLieuChietTinhId",
                table: "DonGiaChietTinh",
                type: "uniqueidentifier",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "DM_VatLieuChietTinh",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false, comment: "Id bảng, khóa chính"),
                    CreatedBy = table.Column<Guid>(type: "uniqueidentifier", maxLength: 36, nullable: true, comment: "Mã người tạo"),
                    CreatedDate = table.Column<DateTime>(type: "datetime2", nullable: false, comment: "Ngày tạo"),
                    DonViTinh = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false, defaultValue: false, comment: "Cờ xóa"),
                    MaVatLieuChietTinh = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    TenVatLieuChietTinh = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: true),
                    UpdatedBy = table.Column<Guid>(type: "uniqueidentifier", maxLength: 36, nullable: true, comment: "Mã người cập nhật"),
                    UpdatedDate = table.Column<DateTime>(type: "datetime2", nullable: true, comment: "Ngày cập nhật")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DM_VatLieuChietTinh", x => x.Id);
                });

            migrationBuilder.CreateIndex(
                name: "IX_DonGiaChietTinh_DM_VatLieuChietTinhId",
                table: "DonGiaChietTinh",
                column: "DM_VatLieuChietTinhId");

            migrationBuilder.AddForeignKey(
                name: "FK_DonGiaChietTinh_DM_VatLieuChietTinh_DM_VatLieuChietTinhId",
                table: "DonGiaChietTinh",
                column: "DM_VatLieuChietTinhId",
                principalTable: "DM_VatLieuChietTinh",
                principalColumn: "Id");
        }
    }
}
