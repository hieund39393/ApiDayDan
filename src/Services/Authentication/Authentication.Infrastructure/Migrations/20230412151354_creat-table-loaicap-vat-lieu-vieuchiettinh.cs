using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Authentication.Infrastructure.Migrations
{
    public partial class creattableloaicapvatlieuvieuchiettinh : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "DM_LoaiCap",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false, comment: "Id bảng, khóa chính"),
                    TenLoaiCap = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: true),
                    MaLoaiCap = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    DonViTinh = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false, defaultValue: false, comment: "Cờ xóa"),
                    CreatedDate = table.Column<DateTime>(type: "datetime2", nullable: false, comment: "Ngày tạo"),
                    CreatedBy = table.Column<Guid>(type: "uniqueidentifier", maxLength: 36, nullable: true, comment: "Mã người tạo"),
                    UpdatedDate = table.Column<DateTime>(type: "datetime2", nullable: true, comment: "Ngày cập nhật"),
                    UpdatedBy = table.Column<Guid>(type: "uniqueidentifier", maxLength: 36, nullable: true, comment: "Mã người cập nhật")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DM_LoaiCap", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "DM_VatLieu",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false, comment: "Id bảng, khóa chính"),
                    TenVatLieu = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: true),
                    MaVatLieu = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    DonViTinh = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false, defaultValue: false, comment: "Cờ xóa"),
                    CreatedDate = table.Column<DateTime>(type: "datetime2", nullable: false, comment: "Ngày tạo"),
                    CreatedBy = table.Column<Guid>(type: "uniqueidentifier", maxLength: 36, nullable: true, comment: "Mã người tạo"),
                    UpdatedDate = table.Column<DateTime>(type: "datetime2", nullable: true, comment: "Ngày cập nhật"),
                    UpdatedBy = table.Column<Guid>(type: "uniqueidentifier", maxLength: 36, nullable: true, comment: "Mã người cập nhật")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DM_VatLieu", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "DM_VatLieuChietTinh",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false, comment: "Id bảng, khóa chính"),
                    TenVatLieuChietTinh = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: true),
                    MaVatLieuChietTinh = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    DonViTinh = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false, defaultValue: false, comment: "Cờ xóa"),
                    CreatedDate = table.Column<DateTime>(type: "datetime2", nullable: false, comment: "Ngày tạo"),
                    CreatedBy = table.Column<Guid>(type: "uniqueidentifier", maxLength: 36, nullable: true, comment: "Mã người tạo"),
                    UpdatedDate = table.Column<DateTime>(type: "datetime2", nullable: true, comment: "Ngày cập nhật"),
                    UpdatedBy = table.Column<Guid>(type: "uniqueidentifier", maxLength: 36, nullable: true, comment: "Mã người cập nhật")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DM_VatLieuChietTinh", x => x.Id);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "DM_LoaiCap");

            migrationBuilder.DropTable(
                name: "DM_VatLieu");

            migrationBuilder.DropTable(
                name: "DM_VatLieuChietTinh");
        }
    }
}
