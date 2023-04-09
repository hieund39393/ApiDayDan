using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Authentication.Infrastructure.Migrations
{
    public partial class newtablebieugiacongviec : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "BieuGiaCongViec",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false, comment: "Id bảng, khóa chính"),
                    BieuGiaID = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    VungID = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    KhuVucID = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    CongViecID = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false, defaultValue: false, comment: "Cờ xóa"),
                    CreatedDate = table.Column<DateTime>(type: "datetime2", nullable: false, comment: "Ngày tạo"),
                    CreatedBy = table.Column<Guid>(type: "uniqueidentifier", maxLength: 36, nullable: true, comment: "Mã người tạo"),
                    UpdatedDate = table.Column<DateTime>(type: "datetime2", nullable: true, comment: "Ngày cập nhật"),
                    UpdatedBy = table.Column<Guid>(type: "uniqueidentifier", maxLength: 36, nullable: true, comment: "Mã người cập nhật")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_BieuGiaCongViec", x => x.Id);
                    table.ForeignKey(
                        name: "FK_BieuGiaCongViec_DM_BieuGia_BieuGiaID",
                        column: x => x.BieuGiaID,
                        principalTable: "DM_BieuGia",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_BieuGiaCongViec_DM_CongViec_CongViecID",
                        column: x => x.CongViecID,
                        principalTable: "DM_CongViec",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_BieuGiaCongViec_DM_KhuVuc_KhuVucID",
                        column: x => x.KhuVucID,
                        principalTable: "DM_KhuVuc",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_BieuGiaCongViec_DM_Vung_VungID",
                        column: x => x.VungID,
                        principalTable: "DM_Vung",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "ChiTietBieuGia",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false, comment: "Id bảng, khóa chính"),
                    Nam = table.Column<int>(type: "int", nullable: false),
                    Quy = table.Column<int>(type: "int", nullable: false),
                    SoLuong = table.Column<int>(type: "int", nullable: false),
                    BieugiaID = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    VungID = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    CongViecID = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    KhuVucID = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    HeSoDieuChinh_K1nc = table.Column<float>(type: "real", nullable: false),
                    HeSoDieuChinh_K2nc = table.Column<float>(type: "real", nullable: false),
                    HeSoDieuChinh_K2mnc = table.Column<float>(type: "real", nullable: false),
                    DonGia_VL = table.Column<float>(type: "real", nullable: false),
                    DonGia_NC = table.Column<float>(type: "real", nullable: false),
                    DonGia_MTC = table.Column<float>(type: "real", nullable: false),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false, defaultValue: false, comment: "Cờ xóa"),
                    CreatedDate = table.Column<DateTime>(type: "datetime2", nullable: false, comment: "Ngày tạo"),
                    CreatedBy = table.Column<Guid>(type: "uniqueidentifier", maxLength: 36, nullable: true, comment: "Mã người tạo"),
                    UpdatedDate = table.Column<DateTime>(type: "datetime2", nullable: true, comment: "Ngày cập nhật"),
                    UpdatedBy = table.Column<Guid>(type: "uniqueidentifier", maxLength: 36, nullable: true, comment: "Mã người cập nhật")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ChiTietBieuGia", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ChiTietBieuGia_DM_BieuGia_BieugiaID",
                        column: x => x.BieugiaID,
                        principalTable: "DM_BieuGia",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_ChiTietBieuGia_DM_CongViec_CongViecID",
                        column: x => x.CongViecID,
                        principalTable: "DM_CongViec",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_ChiTietBieuGia_DM_KhuVuc_KhuVucID",
                        column: x => x.KhuVucID,
                        principalTable: "DM_KhuVuc",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_ChiTietBieuGia_DM_Vung_VungID",
                        column: x => x.VungID,
                        principalTable: "DM_Vung",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateIndex(
                name: "IX_BieuGiaCongViec_BieuGiaID",
                table: "BieuGiaCongViec",
                column: "BieuGiaID");

            migrationBuilder.CreateIndex(
                name: "IX_BieuGiaCongViec_CongViecID",
                table: "BieuGiaCongViec",
                column: "CongViecID");

            migrationBuilder.CreateIndex(
                name: "IX_BieuGiaCongViec_KhuVucID",
                table: "BieuGiaCongViec",
                column: "KhuVucID");

            migrationBuilder.CreateIndex(
                name: "IX_BieuGiaCongViec_VungID",
                table: "BieuGiaCongViec",
                column: "VungID");

            migrationBuilder.CreateIndex(
                name: "IX_ChiTietBieuGia_BieugiaID",
                table: "ChiTietBieuGia",
                column: "BieugiaID");

            migrationBuilder.CreateIndex(
                name: "IX_ChiTietBieuGia_CongViecID",
                table: "ChiTietBieuGia",
                column: "CongViecID");

            migrationBuilder.CreateIndex(
                name: "IX_ChiTietBieuGia_KhuVucID",
                table: "ChiTietBieuGia",
                column: "KhuVucID");

            migrationBuilder.CreateIndex(
                name: "IX_ChiTietBieuGia_VungID",
                table: "ChiTietBieuGia",
                column: "VungID");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "BieuGiaCongViec");

            migrationBuilder.DropTable(
                name: "ChiTietBieuGia");
        }
    }
}
