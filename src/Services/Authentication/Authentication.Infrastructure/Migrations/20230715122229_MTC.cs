using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Authentication.Infrastructure.Migrations
{
    public partial class MTC : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "CauHinhChietTinh",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false, comment: "Id bảng, khóa chính"),
                    IdCongViec = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    IdChiTiet = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    PhanLoai = table.Column<int>(type: "int", nullable: false),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false, comment: "Cờ xóa"),
                    CreatedDate = table.Column<DateTime>(type: "datetime2", nullable: false, comment: "Ngày tạo"),
                    CreatedBy = table.Column<Guid>(type: "uniqueidentifier", nullable: true, comment: "Mã người tạo"),
                    UpdatedDate = table.Column<DateTime>(type: "datetime2", nullable: true, comment: "Ngày cập nhật"),
                    UpdatedBy = table.Column<Guid>(type: "uniqueidentifier", nullable: true, comment: "Mã người cập nhật")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CauHinhChietTinh", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "CauHinhChietTinh_CapNgam",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false, comment: "Id bảng, khóa chính"),
                    IdCongViec = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    IdChiTiet = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    PhanLoai = table.Column<int>(type: "int", nullable: false),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false, comment: "Cờ xóa"),
                    CreatedDate = table.Column<DateTime>(type: "datetime2", nullable: false, comment: "Ngày tạo"),
                    CreatedBy = table.Column<Guid>(type: "uniqueidentifier", nullable: true, comment: "Mã người tạo"),
                    UpdatedDate = table.Column<DateTime>(type: "datetime2", nullable: true, comment: "Ngày cập nhật"),
                    UpdatedBy = table.Column<Guid>(type: "uniqueidentifier", nullable: true, comment: "Mã người cập nhật")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CauHinhChietTinh_CapNgam", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "DM_MTC",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false, comment: "Id bảng, khóa chính"),
                    TenMayThiCong = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: true),
                    MaVatLieu = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    DonViTinh = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false, comment: "Cờ xóa"),
                    CreatedDate = table.Column<DateTime>(type: "datetime2", nullable: false, comment: "Ngày tạo"),
                    CreatedBy = table.Column<Guid>(type: "uniqueidentifier", nullable: true, comment: "Mã người tạo"),
                    UpdatedDate = table.Column<DateTime>(type: "datetime2", nullable: true, comment: "Ngày cập nhật"),
                    UpdatedBy = table.Column<Guid>(type: "uniqueidentifier", nullable: true, comment: "Mã người cập nhật")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DM_MTC", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "DM_MTC_CapNgam",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false, comment: "Id bảng, khóa chính"),
                    TenMTC = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: true),
                    MaMTC = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    DonViTinh = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false, defaultValue: false, comment: "Cờ xóa"),
                    CreatedDate = table.Column<DateTime>(type: "datetime2", nullable: false, comment: "Ngày tạo"),
                    CreatedBy = table.Column<Guid>(type: "uniqueidentifier", maxLength: 36, nullable: true, comment: "Mã người tạo"),
                    UpdatedDate = table.Column<DateTime>(type: "datetime2", nullable: true, comment: "Ngày cập nhật"),
                    UpdatedBy = table.Column<Guid>(type: "uniqueidentifier", maxLength: 36, nullable: true, comment: "Mã người cập nhật")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DM_MTC_CapNgam", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "DonGiaMTC",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false, comment: "Id bảng, khóa chính"),
                    IdMTC = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    VanBan = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    DonGia = table.Column<decimal>(type: "numeric(18,1)", nullable: false),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false, defaultValue: false, comment: "Cờ xóa"),
                    CreatedDate = table.Column<DateTime>(type: "datetime2", nullable: false, comment: "Ngày tạo"),
                    CreatedBy = table.Column<Guid>(type: "uniqueidentifier", maxLength: 36, nullable: true, comment: "Mã người tạo"),
                    UpdatedDate = table.Column<DateTime>(type: "datetime2", nullable: true, comment: "Ngày cập nhật"),
                    UpdatedBy = table.Column<Guid>(type: "uniqueidentifier", maxLength: 36, nullable: true, comment: "Mã người cập nhật")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DonGiaMTC", x => x.Id);
                    table.ForeignKey(
                        name: "FK_DonGiaMTC_DM_MTC_IdMTC",
                        column: x => x.IdMTC,
                        principalTable: "DM_MTC",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "DonGiaMTC_CapNgam",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false, comment: "Id bảng, khóa chính"),
                    IdMTC = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    VanBan = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    DonGia = table.Column<decimal>(type: "numeric(18,1)", nullable: false),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false, defaultValue: false, comment: "Cờ xóa"),
                    CreatedDate = table.Column<DateTime>(type: "datetime2", nullable: false, comment: "Ngày tạo"),
                    CreatedBy = table.Column<Guid>(type: "uniqueidentifier", maxLength: 36, nullable: true, comment: "Mã người tạo"),
                    UpdatedDate = table.Column<DateTime>(type: "datetime2", nullable: true, comment: "Ngày cập nhật"),
                    UpdatedBy = table.Column<Guid>(type: "uniqueidentifier", maxLength: 36, nullable: true, comment: "Mã người cập nhật")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DonGiaMTC_CapNgam", x => x.Id);
                    table.ForeignKey(
                        name: "FK_DonGiaMTC_CapNgam_DM_MTC_CapNgam_IdMTC",
                        column: x => x.IdMTC,
                        principalTable: "DM_MTC_CapNgam",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateIndex(
                name: "IX_DonGiaMTC_IdMTC",
                table: "DonGiaMTC",
                column: "IdMTC");

            migrationBuilder.CreateIndex(
                name: "IX_DonGiaMTC_CapNgam_IdMTC",
                table: "DonGiaMTC_CapNgam",
                column: "IdMTC");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "CauHinhChietTinh");

            migrationBuilder.DropTable(
                name: "CauHinhChietTinh_CapNgam");

            migrationBuilder.DropTable(
                name: "DonGiaMTC");

            migrationBuilder.DropTable(
                name: "DonGiaMTC_CapNgam");

            migrationBuilder.DropTable(
                name: "DM_MTC");

            migrationBuilder.DropTable(
                name: "DM_MTC_CapNgam");
        }
    }
}
