using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Authentication.Infrastructure.Migrations
{
    public partial class Add_NguoiXacNhan : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<DateTime>(
                name: "NgayXacNhan",
                table: "BieuGiaTongHop",
                type: "datetime2",
                nullable: true);

            migrationBuilder.AddColumn<Guid>(
                name: "NguoiXacNhan",
                table: "BieuGiaTongHop",
                type: "uniqueidentifier",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "NgayXacNhan",
                table: "BieuGiaTongHop");

            migrationBuilder.DropColumn(
                name: "NguoiXacNhan",
                table: "BieuGiaTongHop");
        }
    }
}
