using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Authentication.Infrastructure.Migrations
{
    public partial class Add_CapNgam_TB : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "DM_CongViec_CapNgam",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false, comment: "Id bảng, khóa chính"),
                    TenCongViec = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: true),
                    MaCongViec = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: true),
                    DonViTinh = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false, defaultValue: false, comment: "Cờ xóa"),
                    CreatedDate = table.Column<DateTime>(type: "datetime2", nullable: false, comment: "Ngày tạo"),
                    CreatedBy = table.Column<Guid>(type: "uniqueidentifier", maxLength: 36, nullable: true, comment: "Mã người tạo"),
                    UpdatedDate = table.Column<DateTime>(type: "datetime2", nullable: true, comment: "Ngày cập nhật"),
                    UpdatedBy = table.Column<Guid>(type: "uniqueidentifier", maxLength: 36, nullable: true, comment: "Mã người cập nhật")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DM_CongViec_CapNgam", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "DM_LoaiBieuGia_CapNgam",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false, comment: "Id bảng, khóa chính"),
                    MaLoaiBieuGia = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    TenLoaiBieuGia = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: true),
                    IdKhuVuc = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false, defaultValue: false, comment: "Cờ xóa"),
                    CreatedDate = table.Column<DateTime>(type: "datetime2", nullable: false, comment: "Ngày tạo"),
                    CreatedBy = table.Column<Guid>(type: "uniqueidentifier", maxLength: 36, nullable: true, comment: "Mã người tạo"),
                    UpdatedDate = table.Column<DateTime>(type: "datetime2", nullable: true, comment: "Ngày cập nhật"),
                    UpdatedBy = table.Column<Guid>(type: "uniqueidentifier", maxLength: 36, nullable: true, comment: "Mã người cập nhật")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DM_LoaiBieuGia_CapNgam", x => x.Id);
                    table.ForeignKey(
                        name: "FK_DM_LoaiBieuGia_CapNgam_DM_KhuVuc_IdKhuVuc",
                        column: x => x.IdKhuVuc,
                        principalTable: "DM_KhuVuc",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "DM_VatLieu_CapNgam",
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
                    table.PrimaryKey("PK_DM_VatLieu_CapNgam", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "DonGiaNhanCong_CapNgam",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false, comment: "Id bảng, khóa chính"),
                    CapBac = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    HeSo = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    IdKhuVuc = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    DonGia = table.Column<decimal>(type: "numeric(18,1)", nullable: false),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false, defaultValue: false, comment: "Cờ xóa"),
                    CreatedDate = table.Column<DateTime>(type: "datetime2", nullable: false, comment: "Ngày tạo"),
                    CreatedBy = table.Column<Guid>(type: "uniqueidentifier", maxLength: 36, nullable: true, comment: "Mã người tạo"),
                    UpdatedDate = table.Column<DateTime>(type: "datetime2", nullable: true, comment: "Ngày cập nhật"),
                    UpdatedBy = table.Column<Guid>(type: "uniqueidentifier", maxLength: 36, nullable: true, comment: "Mã người cập nhật")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DonGiaNhanCong_CapNgam", x => x.Id);
                    table.ForeignKey(
                        name: "FK_DonGiaNhanCong_CapNgam_DM_KhuVuc_IdKhuVuc",
                        column: x => x.IdKhuVuc,
                        principalTable: "DM_KhuVuc",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "DM_BieuGia_CapNgam",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false, comment: "Id bảng, khóa chính"),
                    idLoaiBieuGia = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    MaBieuGia = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: true),
                    TenBieuGia = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: true),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false, defaultValue: false, comment: "Cờ xóa"),
                    CreatedDate = table.Column<DateTime>(type: "datetime2", nullable: false, comment: "Ngày tạo"),
                    CreatedBy = table.Column<Guid>(type: "uniqueidentifier", maxLength: 36, nullable: true, comment: "Mã người tạo"),
                    UpdatedDate = table.Column<DateTime>(type: "datetime2", nullable: true, comment: "Ngày cập nhật"),
                    UpdatedBy = table.Column<Guid>(type: "uniqueidentifier", maxLength: 36, nullable: true, comment: "Mã người cập nhật")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DM_BieuGia_CapNgam", x => x.Id);
                    table.ForeignKey(
                        name: "FK_DM_BieuGia_CapNgam_DM_LoaiBieuGia_CapNgam_idLoaiBieuGia",
                        column: x => x.idLoaiBieuGia,
                        principalTable: "DM_LoaiBieuGia_CapNgam",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "DonGiaVatLieu_CapNgam",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false, comment: "Id bảng, khóa chính"),
                    IdVatLieu = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
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
                    table.PrimaryKey("PK_DonGiaVatLieu_CapNgam", x => x.Id);
                    table.ForeignKey(
                        name: "FK_DonGiaVatLieu_CapNgam_DM_VatLieu_CapNgam_IdVatLieu",
                        column: x => x.IdVatLieu,
                        principalTable: "DM_VatLieu_CapNgam",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "BieuGiaCongViec_CapNgam",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false, comment: "Id bảng, khóa chính"),
                    IdBieuGia = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    IdCongViec = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    CongViecChinh = table.Column<bool>(type: "bit", nullable: false),
                    ThuTuHienThi = table.Column<int>(type: "int", nullable: false),
                    PhanLoai = table.Column<int>(type: "int", nullable: false),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false, defaultValue: false, comment: "Cờ xóa"),
                    CreatedDate = table.Column<DateTime>(type: "datetime2", nullable: false, comment: "Ngày tạo"),
                    CreatedBy = table.Column<Guid>(type: "uniqueidentifier", maxLength: 36, nullable: true, comment: "Mã người tạo"),
                    UpdatedDate = table.Column<DateTime>(type: "datetime2", nullable: true, comment: "Ngày cập nhật"),
                    UpdatedBy = table.Column<Guid>(type: "uniqueidentifier", maxLength: 36, nullable: true, comment: "Mã người cập nhật")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_BieuGiaCongViec_CapNgam", x => x.Id);
                    table.ForeignKey(
                        name: "FK_BieuGiaCongViec_CapNgam_DM_BieuGia_CapNgam_IdBieuGia",
                        column: x => x.IdBieuGia,
                        principalTable: "DM_BieuGia_CapNgam",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_BieuGiaCongViec_CapNgam_DM_CongViec_CapNgam_IdCongViec",
                        column: x => x.IdCongViec,
                        principalTable: "DM_CongViec_CapNgam",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "BieuGiaTongHop_CapNgam",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false, comment: "Id bảng, khóa chính"),
                    IdBieuGia = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Quy = table.Column<int>(type: "int", nullable: false),
                    Nam = table.Column<int>(type: "int", nullable: false),
                    DonGia = table.Column<decimal>(type: "numeric(18,2)", nullable: false),
                    DonGia2 = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    DonGia3 = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    TinhTrang = table.Column<int>(type: "int", nullable: false),
                    NgayXacNhan = table.Column<DateTime>(type: "datetime2", nullable: true),
                    NguoiXacNhan = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false, defaultValue: false, comment: "Cờ xóa"),
                    CreatedDate = table.Column<DateTime>(type: "datetime2", nullable: false, comment: "Ngày tạo"),
                    CreatedBy = table.Column<Guid>(type: "uniqueidentifier", maxLength: 36, nullable: true, comment: "Mã người tạo"),
                    UpdatedDate = table.Column<DateTime>(type: "datetime2", nullable: true, comment: "Ngày cập nhật"),
                    UpdatedBy = table.Column<Guid>(type: "uniqueidentifier", maxLength: 36, nullable: true, comment: "Mã người cập nhật")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_BieuGiaTongHop_CapNgam", x => x.Id);
                    table.ForeignKey(
                        name: "FK_BieuGiaTongHop_CapNgam_DM_BieuGia_CapNgam_IdBieuGia",
                        column: x => x.IdBieuGia,
                        principalTable: "DM_BieuGia_CapNgam",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "ChiTietBieuGia_CapNgam",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false, comment: "Id bảng, khóa chính"),
                    IDBieuGia = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    IDCongViec = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    Nam = table.Column<int>(type: "int", nullable: false),
                    Quy = table.Column<int>(type: "int", nullable: false),
                    SoLuong = table.Column<decimal>(type: "numeric(18,2)", nullable: false),
                    HeSoDieuChinh_K1nc = table.Column<decimal>(type: "numeric(18,2)", nullable: false),
                    HeSoDieuChinh_K2nc = table.Column<decimal>(type: "numeric(18,2)", nullable: false),
                    HeSoDieuChinh_Kmtc = table.Column<decimal>(type: "numeric(18,2)", nullable: false),
                    DonGia_VL = table.Column<decimal>(type: "numeric(18,2)", nullable: false),
                    DonGia_NC = table.Column<decimal>(type: "numeric(18,2)", nullable: false),
                    DonGia_MTC = table.Column<decimal>(type: "numeric(18,2)", nullable: false),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false, defaultValue: false, comment: "Cờ xóa"),
                    CreatedDate = table.Column<DateTime>(type: "datetime2", nullable: false, comment: "Ngày tạo"),
                    CreatedBy = table.Column<Guid>(type: "uniqueidentifier", maxLength: 36, nullable: true, comment: "Mã người tạo"),
                    UpdatedDate = table.Column<DateTime>(type: "datetime2", nullable: true, comment: "Ngày cập nhật"),
                    UpdatedBy = table.Column<Guid>(type: "uniqueidentifier", maxLength: 36, nullable: true, comment: "Mã người cập nhật")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ChiTietBieuGia_CapNgam", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ChiTietBieuGia_CapNgam_DM_BieuGia_CapNgam_IDBieuGia",
                        column: x => x.IDBieuGia,
                        principalTable: "DM_BieuGia_CapNgam",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_ChiTietBieuGia_CapNgam_DM_CongViec_CapNgam_IDCongViec",
                        column: x => x.IDCongViec,
                        principalTable: "DM_CongViec_CapNgam",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateIndex(
                name: "IX_BieuGiaCongViec_CapNgam_IdBieuGia",
                table: "BieuGiaCongViec_CapNgam",
                column: "IdBieuGia");

            migrationBuilder.CreateIndex(
                name: "IX_BieuGiaCongViec_CapNgam_IdCongViec",
                table: "BieuGiaCongViec_CapNgam",
                column: "IdCongViec");

            migrationBuilder.CreateIndex(
                name: "IX_BieuGiaTongHop_CapNgam_IdBieuGia",
                table: "BieuGiaTongHop_CapNgam",
                column: "IdBieuGia");

            migrationBuilder.CreateIndex(
                name: "IX_ChiTietBieuGia_CapNgam_IDBieuGia",
                table: "ChiTietBieuGia_CapNgam",
                column: "IDBieuGia");

            migrationBuilder.CreateIndex(
                name: "IX_ChiTietBieuGia_CapNgam_IDCongViec",
                table: "ChiTietBieuGia_CapNgam",
                column: "IDCongViec");

            migrationBuilder.CreateIndex(
                name: "IX_DM_BieuGia_CapNgam_idLoaiBieuGia",
                table: "DM_BieuGia_CapNgam",
                column: "idLoaiBieuGia");

            migrationBuilder.CreateIndex(
                name: "IX_DM_LoaiBieuGia_CapNgam_IdKhuVuc",
                table: "DM_LoaiBieuGia_CapNgam",
                column: "IdKhuVuc");

            migrationBuilder.CreateIndex(
                name: "IX_DonGiaNhanCong_CapNgam_IdKhuVuc",
                table: "DonGiaNhanCong_CapNgam",
                column: "IdKhuVuc");

            migrationBuilder.CreateIndex(
                name: "IX_DonGiaVatLieu_CapNgam_IdVatLieu",
                table: "DonGiaVatLieu_CapNgam",
                column: "IdVatLieu");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "BieuGiaCongViec_CapNgam");

            migrationBuilder.DropTable(
                name: "BieuGiaTongHop_CapNgam");

            migrationBuilder.DropTable(
                name: "ChiTietBieuGia_CapNgam");

            migrationBuilder.DropTable(
                name: "DonGiaNhanCong_CapNgam");

            migrationBuilder.DropTable(
                name: "DonGiaVatLieu_CapNgam");

            migrationBuilder.DropTable(
                name: "DM_BieuGia_CapNgam");

            migrationBuilder.DropTable(
                name: "DM_CongViec_CapNgam");

            migrationBuilder.DropTable(
                name: "DM_VatLieu_CapNgam");

            migrationBuilder.DropTable(
                name: "DM_LoaiBieuGia_CapNgam");
        }
    }
}
