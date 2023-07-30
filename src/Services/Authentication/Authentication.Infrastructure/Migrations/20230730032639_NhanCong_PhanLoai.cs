using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Authentication.Infrastructure.Migrations
{
    public partial class NhanCong_PhanLoai : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "PhanLoai",
                table: "DM_NhanCong_CapNgam",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateTable(
                name: "ChietTinhChiTiet_CapNgam",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false, comment: "Id bảng, khóa chính"),
                    IdDonGiaChietTinh = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    IdCongViec = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    IdChiTiet = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    DinhMuc = table.Column<decimal>(type: "decimal(18,2)", nullable: true),
                    PhanLoai = table.Column<int>(type: "int", nullable: false),
                    DonGiaChietTinh_CapNgamId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false, comment: "Cờ xóa"),
                    CreatedDate = table.Column<DateTime>(type: "datetime2", nullable: false, comment: "Ngày tạo"),
                    CreatedBy = table.Column<Guid>(type: "uniqueidentifier", nullable: true, comment: "Mã người tạo"),
                    UpdatedDate = table.Column<DateTime>(type: "datetime2", nullable: true, comment: "Ngày cập nhật"),
                    UpdatedBy = table.Column<Guid>(type: "uniqueidentifier", nullable: true, comment: "Mã người cập nhật")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ChietTinhChiTiet_CapNgam", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ChietTinhChiTiet_CapNgam_DonGiaChietTinh_CapNgam_DonGiaChietTinh_CapNgamId",
                        column: x => x.DonGiaChietTinh_CapNgamId,
                        principalTable: "DonGiaChietTinh_CapNgam",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateIndex(
                name: "IX_ChietTinhChiTiet_CapNgam_DonGiaChietTinh_CapNgamId",
                table: "ChietTinhChiTiet_CapNgam",
                column: "DonGiaChietTinh_CapNgamId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ChietTinhChiTiet_CapNgam");

            migrationBuilder.DropColumn(
                name: "PhanLoai",
                table: "DM_NhanCong_CapNgam");
        }
    }
}
