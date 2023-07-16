using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Authentication.Infrastructure.Migrations
{
    public partial class Add_DGCT2 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_DonGiaChietTinh_IdCongViec",
                table: "DonGiaChietTinh");

            migrationBuilder.CreateIndex(
                name: "IX_DonGiaChietTinh_IdCongViec",
                table: "DonGiaChietTinh",
                column: "IdCongViec");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_DonGiaChietTinh_IdCongViec",
                table: "DonGiaChietTinh");

            migrationBuilder.CreateIndex(
                name: "IX_DonGiaChietTinh_IdCongViec",
                table: "DonGiaChietTinh",
                column: "IdCongViec",
                unique: true,
                filter: "[IdCongViec] IS NOT NULL");
        }
    }
}
