using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Authentication.Infrastructure.Migrations
{
    public partial class updatecolumeIdPhanLoai2 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_DonGiaChietTinh_DM_VatLieu_IdVatLieu",
                table: "DonGiaChietTinh");

            migrationBuilder.RenameColumn(
                name: "IdVatLieu",
                table: "DonGiaChietTinh",
                newName: "IdVatLieuChietTinh");

            migrationBuilder.RenameIndex(
                name: "IX_DonGiaChietTinh_IdVatLieu",
                table: "DonGiaChietTinh",
                newName: "IX_DonGiaChietTinh_IdVatLieuChietTinh");

            migrationBuilder.AddForeignKey(
                name: "FK_DonGiaChietTinh_DM_VatLieuChietTinh_IdVatLieuChietTinh",
                table: "DonGiaChietTinh",
                column: "IdVatLieuChietTinh",
                principalTable: "DM_VatLieuChietTinh",
                principalColumn: "Id");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_DonGiaChietTinh_DM_VatLieuChietTinh_IdVatLieuChietTinh",
                table: "DonGiaChietTinh");

            migrationBuilder.RenameColumn(
                name: "IdVatLieuChietTinh",
                table: "DonGiaChietTinh",
                newName: "IdVatLieu");

            migrationBuilder.RenameIndex(
                name: "IX_DonGiaChietTinh_IdVatLieuChietTinh",
                table: "DonGiaChietTinh",
                newName: "IX_DonGiaChietTinh_IdVatLieu");

            migrationBuilder.AddForeignKey(
                name: "FK_DonGiaChietTinh_DM_VatLieu_IdVatLieu",
                table: "DonGiaChietTinh",
                column: "IdVatLieu",
                principalTable: "DM_VatLieu",
                principalColumn: "Id");
        }
    }
}
