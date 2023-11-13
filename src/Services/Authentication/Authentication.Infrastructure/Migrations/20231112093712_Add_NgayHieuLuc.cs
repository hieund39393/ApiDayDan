using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Authentication.Infrastructure.Migrations
{
    public partial class Add_NgayHieuLuc : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<DateTime>(
                name: "NgayHieuLuc",
                table: "BieuGiaTongHop_CapNgam",
                type: "datetime2",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "NgayHieuLuc",
                table: "BieuGiaTongHop",
                type: "datetime2",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "NgayHieuLuc",
                table: "BieuGiaTongHop_CapNgam");

            migrationBuilder.DropColumn(
                name: "NgayHieuLuc",
                table: "BieuGiaTongHop");
        }
    }
}
