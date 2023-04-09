using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Authentication.Infrastructure.Migrations
{
    public partial class newtablebieugiacongviec8 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<Guid>(
                name: "CongViecID",
                table: "ChiTietBieuGia",
                type: "uniqueidentifier",
                nullable: true);

            migrationBuilder.AddColumn<Guid>(
                name: "KhuVucID",
                table: "ChiTietBieuGia",
                type: "uniqueidentifier",
                nullable: true);

            migrationBuilder.AddColumn<Guid>(
                name: "VungID",
                table: "ChiTietBieuGia",
                type: "uniqueidentifier",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "CongViecID",
                table: "ChiTietBieuGia");

            migrationBuilder.DropColumn(
                name: "KhuVucID",
                table: "ChiTietBieuGia");

            migrationBuilder.DropColumn(
                name: "VungID",
                table: "ChiTietBieuGia");
        }
    }
}
