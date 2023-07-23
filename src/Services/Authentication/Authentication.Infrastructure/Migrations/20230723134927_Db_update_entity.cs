using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Authentication.Infrastructure.Migrations
{
    public partial class Db_update_entity : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<decimal>(
                name: "DonGia",
                table: "DonGiaVatLieu_CapNgam",
                type: "numeric(18,2)",
                nullable: false,
                oldClrType: typeof(decimal),
                oldType: "numeric(18,1)");

            migrationBuilder.AlterColumn<decimal>(
                name: "DonGia",
                table: "DonGiaVatLieu",
                type: "numeric(18,2)",
                nullable: false,
                oldClrType: typeof(decimal),
                oldType: "numeric(18,1)");

            migrationBuilder.AlterColumn<decimal>(
                name: "DonGia",
                table: "DonGiaNhanCong_CapNgam",
                type: "numeric(18,2)",
                nullable: false,
                oldClrType: typeof(decimal),
                oldType: "numeric(18,1)");

            migrationBuilder.AlterColumn<decimal>(
                name: "DonGia",
                table: "DonGiaNhanCong",
                type: "numeric(18,2)",
                nullable: false,
                oldClrType: typeof(decimal),
                oldType: "numeric(18,1)");

            migrationBuilder.AlterColumn<decimal>(
                name: "DonGia",
                table: "DonGiaMTC_CapNgam",
                type: "numeric(18,2)",
                nullable: false,
                oldClrType: typeof(decimal),
                oldType: "numeric(18,1)");

            migrationBuilder.AlterColumn<decimal>(
                name: "DonGia",
                table: "DonGiaMTC",
                type: "numeric(18,2)",
                nullable: false,
                oldClrType: typeof(decimal),
                oldType: "numeric(18,1)");

            migrationBuilder.AlterColumn<decimal>(
                name: "DonGiaVatLieu",
                table: "DonGiaChietTinh",
                type: "numeric(18,2)",
                nullable: true,
                oldClrType: typeof(decimal),
                oldType: "numeric(18,1)",
                oldNullable: true);

            migrationBuilder.AlterColumn<decimal>(
                name: "DonGiaNhanCongHai",
                table: "DonGiaChietTinh",
                type: "numeric(18,2)",
                nullable: true,
                oldClrType: typeof(decimal),
                oldType: "numeric(18,1)",
                oldNullable: true);

            migrationBuilder.AlterColumn<decimal>(
                name: "DonGiaNhanCongBa",
                table: "DonGiaChietTinh",
                type: "numeric(18,2)",
                nullable: true,
                oldClrType: typeof(decimal),
                oldType: "numeric(18,1)",
                oldNullable: true);

            migrationBuilder.AlterColumn<decimal>(
                name: "DonGiaNhanCong",
                table: "DonGiaChietTinh",
                type: "numeric(18,2)",
                nullable: true,
                oldClrType: typeof(decimal),
                oldType: "numeric(18,1)",
                oldNullable: true);

            migrationBuilder.AlterColumn<decimal>(
                name: "DonGiaMTC",
                table: "DonGiaChietTinh",
                type: "numeric(18,2)",
                nullable: true,
                oldClrType: typeof(decimal),
                oldType: "numeric(18,1)",
                oldNullable: true);

            migrationBuilder.AlterColumn<decimal>(
                name: "DonGia",
                table: "DonGiaCap_CapNgam",
                type: "numeric(18,2)",
                nullable: false,
                oldClrType: typeof(decimal),
                oldType: "numeric(18,1)");

            migrationBuilder.AlterColumn<decimal>(
                name: "DonGia",
                table: "DonGiaCap",
                type: "numeric(18,2)",
                nullable: false,
                oldClrType: typeof(decimal),
                oldType: "numeric(18,1)");

            migrationBuilder.AlterColumn<decimal>(
                name: "DinhMuc",
                table: "ChietTinhChiTiet",
                type: "numeric(18,2)",
                nullable: true,
                oldClrType: typeof(decimal),
                oldType: "numeric(18,1)",
                oldNullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<decimal>(
                name: "DonGia",
                table: "DonGiaVatLieu_CapNgam",
                type: "numeric(18,1)",
                nullable: false,
                oldClrType: typeof(decimal),
                oldType: "numeric(18,2)");

            migrationBuilder.AlterColumn<decimal>(
                name: "DonGia",
                table: "DonGiaVatLieu",
                type: "numeric(18,1)",
                nullable: false,
                oldClrType: typeof(decimal),
                oldType: "numeric(18,2)");

            migrationBuilder.AlterColumn<decimal>(
                name: "DonGia",
                table: "DonGiaNhanCong_CapNgam",
                type: "numeric(18,1)",
                nullable: false,
                oldClrType: typeof(decimal),
                oldType: "numeric(18,2)");

            migrationBuilder.AlterColumn<decimal>(
                name: "DonGia",
                table: "DonGiaNhanCong",
                type: "numeric(18,1)",
                nullable: false,
                oldClrType: typeof(decimal),
                oldType: "numeric(18,2)");

            migrationBuilder.AlterColumn<decimal>(
                name: "DonGia",
                table: "DonGiaMTC_CapNgam",
                type: "numeric(18,1)",
                nullable: false,
                oldClrType: typeof(decimal),
                oldType: "numeric(18,2)");

            migrationBuilder.AlterColumn<decimal>(
                name: "DonGia",
                table: "DonGiaMTC",
                type: "numeric(18,1)",
                nullable: false,
                oldClrType: typeof(decimal),
                oldType: "numeric(18,2)");

            migrationBuilder.AlterColumn<decimal>(
                name: "DonGiaVatLieu",
                table: "DonGiaChietTinh",
                type: "numeric(18,1)",
                nullable: true,
                oldClrType: typeof(decimal),
                oldType: "numeric(18,2)",
                oldNullable: true);

            migrationBuilder.AlterColumn<decimal>(
                name: "DonGiaNhanCongHai",
                table: "DonGiaChietTinh",
                type: "numeric(18,1)",
                nullable: true,
                oldClrType: typeof(decimal),
                oldType: "numeric(18,2)",
                oldNullable: true);

            migrationBuilder.AlterColumn<decimal>(
                name: "DonGiaNhanCongBa",
                table: "DonGiaChietTinh",
                type: "numeric(18,1)",
                nullable: true,
                oldClrType: typeof(decimal),
                oldType: "numeric(18,2)",
                oldNullable: true);

            migrationBuilder.AlterColumn<decimal>(
                name: "DonGiaNhanCong",
                table: "DonGiaChietTinh",
                type: "numeric(18,1)",
                nullable: true,
                oldClrType: typeof(decimal),
                oldType: "numeric(18,2)",
                oldNullable: true);

            migrationBuilder.AlterColumn<decimal>(
                name: "DonGiaMTC",
                table: "DonGiaChietTinh",
                type: "numeric(18,1)",
                nullable: true,
                oldClrType: typeof(decimal),
                oldType: "numeric(18,2)",
                oldNullable: true);

            migrationBuilder.AlterColumn<decimal>(
                name: "DonGia",
                table: "DonGiaCap_CapNgam",
                type: "numeric(18,1)",
                nullable: false,
                oldClrType: typeof(decimal),
                oldType: "numeric(18,2)");

            migrationBuilder.AlterColumn<decimal>(
                name: "DonGia",
                table: "DonGiaCap",
                type: "numeric(18,1)",
                nullable: false,
                oldClrType: typeof(decimal),
                oldType: "numeric(18,2)");

            migrationBuilder.AlterColumn<decimal>(
                name: "DinhMuc",
                table: "ChietTinhChiTiet",
                type: "numeric(18,1)",
                nullable: true,
                oldClrType: typeof(decimal),
                oldType: "numeric(18,2)",
                oldNullable: true);
        }
    }
}
