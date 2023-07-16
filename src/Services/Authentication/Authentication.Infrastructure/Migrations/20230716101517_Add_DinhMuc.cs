using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Authentication.Infrastructure.Migrations
{
    public partial class Add_DinhMuc : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<decimal>(
                name: "DinhMuc",
                table: "DonGiaVatLieu_CapNgam",
                type: "decimal(18,2)",
                nullable: true);

            migrationBuilder.AddColumn<decimal>(
                name: "DinhMucCu",
                table: "DonGiaVatLieu_CapNgam",
                type: "decimal(18,2)",
                nullable: true);

            migrationBuilder.AddColumn<decimal>(
                name: "DonGiaCu",
                table: "DonGiaVatLieu_CapNgam",
                type: "decimal(18,2)",
                nullable: true);

            migrationBuilder.AddColumn<decimal>(
                name: "DinhMuc",
                table: "DonGiaVatLieu",
                type: "decimal(18,2)",
                nullable: true);

            migrationBuilder.AddColumn<decimal>(
                name: "DinhMucCu",
                table: "DonGiaVatLieu",
                type: "decimal(18,2)",
                nullable: true);

            migrationBuilder.AddColumn<decimal>(
                name: "DonGiaCu",
                table: "DonGiaVatLieu",
                type: "decimal(18,2)",
                nullable: true);

            migrationBuilder.AddColumn<decimal>(
                name: "DinhMuc",
                table: "DonGiaNhanCong_CapNgam",
                type: "decimal(18,2)",
                nullable: true);

            migrationBuilder.AddColumn<decimal>(
                name: "DinhMucCu",
                table: "DonGiaNhanCong_CapNgam",
                type: "decimal(18,2)",
                nullable: true);

            migrationBuilder.AddColumn<decimal>(
                name: "DonGiaCu",
                table: "DonGiaNhanCong_CapNgam",
                type: "decimal(18,2)",
                nullable: true);

            migrationBuilder.AddColumn<decimal>(
                name: "DinhMuc",
                table: "DonGiaNhanCong",
                type: "decimal(18,2)",
                nullable: true);

            migrationBuilder.AddColumn<decimal>(
                name: "DinhMucCu",
                table: "DonGiaNhanCong",
                type: "decimal(18,2)",
                nullable: true);

            migrationBuilder.AddColumn<decimal>(
                name: "DonGiaCu",
                table: "DonGiaNhanCong",
                type: "decimal(18,2)",
                nullable: true);

            migrationBuilder.AddColumn<decimal>(
                name: "DinhMuc",
                table: "DonGiaMTC_CapNgam",
                type: "decimal(18,2)",
                nullable: true);

            migrationBuilder.AddColumn<decimal>(
                name: "DinhMucCu",
                table: "DonGiaMTC_CapNgam",
                type: "decimal(18,2)",
                nullable: true);

            migrationBuilder.AddColumn<decimal>(
                name: "DonGiaCu",
                table: "DonGiaMTC_CapNgam",
                type: "decimal(18,2)",
                nullable: true);

            migrationBuilder.AddColumn<decimal>(
                name: "DinhMuc",
                table: "DonGiaMTC",
                type: "decimal(18,2)",
                nullable: true);

            migrationBuilder.AddColumn<decimal>(
                name: "DinhMucCu",
                table: "DonGiaMTC",
                type: "decimal(18,2)",
                nullable: true);

            migrationBuilder.AddColumn<decimal>(
                name: "DonGiaCu",
                table: "DonGiaMTC",
                type: "decimal(18,2)",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "DinhMuc",
                table: "DonGiaVatLieu_CapNgam");

            migrationBuilder.DropColumn(
                name: "DinhMucCu",
                table: "DonGiaVatLieu_CapNgam");

            migrationBuilder.DropColumn(
                name: "DonGiaCu",
                table: "DonGiaVatLieu_CapNgam");

            migrationBuilder.DropColumn(
                name: "DinhMuc",
                table: "DonGiaVatLieu");

            migrationBuilder.DropColumn(
                name: "DinhMucCu",
                table: "DonGiaVatLieu");

            migrationBuilder.DropColumn(
                name: "DonGiaCu",
                table: "DonGiaVatLieu");

            migrationBuilder.DropColumn(
                name: "DinhMuc",
                table: "DonGiaNhanCong_CapNgam");

            migrationBuilder.DropColumn(
                name: "DinhMucCu",
                table: "DonGiaNhanCong_CapNgam");

            migrationBuilder.DropColumn(
                name: "DonGiaCu",
                table: "DonGiaNhanCong_CapNgam");

            migrationBuilder.DropColumn(
                name: "DinhMuc",
                table: "DonGiaNhanCong");

            migrationBuilder.DropColumn(
                name: "DinhMucCu",
                table: "DonGiaNhanCong");

            migrationBuilder.DropColumn(
                name: "DonGiaCu",
                table: "DonGiaNhanCong");

            migrationBuilder.DropColumn(
                name: "DinhMuc",
                table: "DonGiaMTC_CapNgam");

            migrationBuilder.DropColumn(
                name: "DinhMucCu",
                table: "DonGiaMTC_CapNgam");

            migrationBuilder.DropColumn(
                name: "DonGiaCu",
                table: "DonGiaMTC_CapNgam");

            migrationBuilder.DropColumn(
                name: "DinhMuc",
                table: "DonGiaMTC");

            migrationBuilder.DropColumn(
                name: "DinhMucCu",
                table: "DonGiaMTC");

            migrationBuilder.DropColumn(
                name: "DonGiaCu",
                table: "DonGiaMTC");
        }
    }
}
