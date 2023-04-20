using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Authentication.Infrastructure.Migrations
{
    public partial class add_bieugiatonghop : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<decimal>(
                name: "SoLuong",
                table: "ChiTietBieuGia",
                type: "numeric(18,2)",
                nullable: false,
                oldClrType: typeof(decimal),
                oldType: "numeric(18,1)");

            migrationBuilder.AlterColumn<decimal>(
                name: "HeSoDieuChinh_Kmtc",
                table: "ChiTietBieuGia",
                type: "numeric(18,2)",
                nullable: false,
                oldClrType: typeof(decimal),
                oldType: "numeric(18,1)");

            migrationBuilder.AlterColumn<decimal>(
                name: "HeSoDieuChinh_K2nc",
                table: "ChiTietBieuGia",
                type: "numeric(18,2)",
                nullable: false,
                oldClrType: typeof(decimal),
                oldType: "numeric(18,1)");

            migrationBuilder.AlterColumn<decimal>(
                name: "HeSoDieuChinh_K1nc",
                table: "ChiTietBieuGia",
                type: "numeric(18,2)",
                nullable: false,
                oldClrType: typeof(decimal),
                oldType: "numeric(18,1)");

            migrationBuilder.AlterColumn<decimal>(
                name: "DonGia_VL",
                table: "ChiTietBieuGia",
                type: "numeric(18,2)",
                nullable: false,
                oldClrType: typeof(decimal),
                oldType: "numeric(18,1)");

            migrationBuilder.AlterColumn<decimal>(
                name: "DonGia_NC",
                table: "ChiTietBieuGia",
                type: "numeric(18,2)",
                nullable: false,
                oldClrType: typeof(decimal),
                oldType: "numeric(18,1)");

            migrationBuilder.AlterColumn<decimal>(
                name: "DonGia_MTC",
                table: "ChiTietBieuGia",
                type: "numeric(18,2)",
                nullable: false,
                oldClrType: typeof(decimal),
                oldType: "numeric(18,1)");

            migrationBuilder.CreateTable(
                name: "BieuGiaTongHop",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false, comment: "Id bảng, khóa chính"),
                    DM_BieuGiaId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    IdBieuGia = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Quy = table.Column<int>(type: "int", nullable: false),
                    Nam = table.Column<int>(type: "int", nullable: false),
                    DonGia = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    TinhTrang = table.Column<int>(type: "int", nullable: false),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false, comment: "Cờ xóa"),
                    CreatedDate = table.Column<DateTime>(type: "datetime2", nullable: false, comment: "Ngày tạo"),
                    CreatedBy = table.Column<Guid>(type: "uniqueidentifier", nullable: true, comment: "Mã người tạo"),
                    UpdatedDate = table.Column<DateTime>(type: "datetime2", nullable: true, comment: "Ngày cập nhật"),
                    UpdatedBy = table.Column<Guid>(type: "uniqueidentifier", nullable: true, comment: "Mã người cập nhật")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_BieuGiaTongHop", x => x.Id);
                    table.ForeignKey(
                        name: "FK_BieuGiaTongHop_DM_BieuGia_DM_BieuGiaId",
                        column: x => x.DM_BieuGiaId,
                        principalTable: "DM_BieuGia",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateIndex(
                name: "IX_BieuGiaTongHop_DM_BieuGiaId",
                table: "BieuGiaTongHop",
                column: "DM_BieuGiaId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "BieuGiaTongHop");

            migrationBuilder.AlterColumn<decimal>(
                name: "SoLuong",
                table: "ChiTietBieuGia",
                type: "numeric(18,1)",
                nullable: false,
                oldClrType: typeof(decimal),
                oldType: "numeric(18,2)");

            migrationBuilder.AlterColumn<decimal>(
                name: "HeSoDieuChinh_Kmtc",
                table: "ChiTietBieuGia",
                type: "numeric(18,1)",
                nullable: false,
                oldClrType: typeof(decimal),
                oldType: "numeric(18,2)");

            migrationBuilder.AlterColumn<decimal>(
                name: "HeSoDieuChinh_K2nc",
                table: "ChiTietBieuGia",
                type: "numeric(18,1)",
                nullable: false,
                oldClrType: typeof(decimal),
                oldType: "numeric(18,2)");

            migrationBuilder.AlterColumn<decimal>(
                name: "HeSoDieuChinh_K1nc",
                table: "ChiTietBieuGia",
                type: "numeric(18,1)",
                nullable: false,
                oldClrType: typeof(decimal),
                oldType: "numeric(18,2)");

            migrationBuilder.AlterColumn<decimal>(
                name: "DonGia_VL",
                table: "ChiTietBieuGia",
                type: "numeric(18,1)",
                nullable: false,
                oldClrType: typeof(decimal),
                oldType: "numeric(18,2)");

            migrationBuilder.AlterColumn<decimal>(
                name: "DonGia_NC",
                table: "ChiTietBieuGia",
                type: "numeric(18,1)",
                nullable: false,
                oldClrType: typeof(decimal),
                oldType: "numeric(18,2)");

            migrationBuilder.AlterColumn<decimal>(
                name: "DonGia_MTC",
                table: "ChiTietBieuGia",
                type: "numeric(18,1)",
                nullable: false,
                oldClrType: typeof(decimal),
                oldType: "numeric(18,2)");
        }
    }
}
