using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Authentication.Infrastructure.Migrations
{
    public partial class Update_IdKhuVuc : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_DM_LoaiBieuGia_DM_KhuVuc_KhuVucID",
                table: "DM_LoaiBieuGia");

            migrationBuilder.RenameColumn(
                name: "KhuVucID",
                table: "DM_LoaiBieuGia",
                newName: "IdKhuVuc");

            migrationBuilder.RenameIndex(
                name: "IX_DM_LoaiBieuGia_KhuVucID",
                table: "DM_LoaiBieuGia",
                newName: "IX_DM_LoaiBieuGia_IdKhuVuc");

            migrationBuilder.AddForeignKey(
                name: "FK_DM_LoaiBieuGia_DM_KhuVuc_IdKhuVuc",
                table: "DM_LoaiBieuGia",
                column: "IdKhuVuc",
                principalTable: "DM_KhuVuc",
                principalColumn: "Id");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_DM_LoaiBieuGia_DM_KhuVuc_IdKhuVuc",
                table: "DM_LoaiBieuGia");

            migrationBuilder.RenameColumn(
                name: "IdKhuVuc",
                table: "DM_LoaiBieuGia",
                newName: "KhuVucID");

            migrationBuilder.RenameIndex(
                name: "IX_DM_LoaiBieuGia_IdKhuVuc",
                table: "DM_LoaiBieuGia",
                newName: "IX_DM_LoaiBieuGia_KhuVucID");

            migrationBuilder.AddForeignKey(
                name: "FK_DM_LoaiBieuGia_DM_KhuVuc_KhuVucID",
                table: "DM_LoaiBieuGia",
                column: "KhuVucID",
                principalTable: "DM_KhuVuc",
                principalColumn: "Id");
        }
    }
}
