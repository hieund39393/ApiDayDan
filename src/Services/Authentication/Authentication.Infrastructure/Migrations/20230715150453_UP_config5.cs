using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Authentication.Infrastructure.Migrations
{
    public partial class UP_config5 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateIndex(
                name: "IX_CauHinhChietTinh_CapNgam_IdCongViec",
                table: "CauHinhChietTinh_CapNgam",
                column: "IdCongViec");

            migrationBuilder.CreateIndex(
                name: "IX_CauHinhChietTinh_IdCongViec",
                table: "CauHinhChietTinh",
                column: "IdCongViec");

            migrationBuilder.AddForeignKey(
                name: "FK_CauHinhChietTinh_DM_CongViec_IdCongViec",
                table: "CauHinhChietTinh",
                column: "IdCongViec",
                principalTable: "DM_CongViec",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_CauHinhChietTinh_CapNgam_DM_CongViec_IdCongViec",
                table: "CauHinhChietTinh_CapNgam",
                column: "IdCongViec",
                principalTable: "DM_CongViec",
                principalColumn: "Id");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_CauHinhChietTinh_DM_CongViec_IdCongViec",
                table: "CauHinhChietTinh");

            migrationBuilder.DropForeignKey(
                name: "FK_CauHinhChietTinh_CapNgam_DM_CongViec_IdCongViec",
                table: "CauHinhChietTinh_CapNgam");

            migrationBuilder.DropIndex(
                name: "IX_CauHinhChietTinh_CapNgam_IdCongViec",
                table: "CauHinhChietTinh_CapNgam");

            migrationBuilder.DropIndex(
                name: "IX_CauHinhChietTinh_IdCongViec",
                table: "CauHinhChietTinh");
        }
    }
}
