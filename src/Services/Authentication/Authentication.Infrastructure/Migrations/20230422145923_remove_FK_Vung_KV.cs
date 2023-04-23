using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Authentication.Infrastructure.Migrations
{
    public partial class remove_FK_Vung_KV : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_DM_LoaiBieuGia_DM_Vung_VungID",
                table: "DM_LoaiBieuGia");

            migrationBuilder.RenameColumn(
                name: "VungID",
                table: "DM_LoaiBieuGia",
                newName: "DM_VungId");

            migrationBuilder.RenameIndex(
                name: "IX_DM_LoaiBieuGia_VungID",
                table: "DM_LoaiBieuGia",
                newName: "IX_DM_LoaiBieuGia_DM_VungId");

            migrationBuilder.AddForeignKey(
                name: "FK_DM_LoaiBieuGia_DM_Vung_DM_VungId",
                table: "DM_LoaiBieuGia",
                column: "DM_VungId",
                principalTable: "DM_Vung",
                principalColumn: "Id");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_DM_LoaiBieuGia_DM_Vung_DM_VungId",
                table: "DM_LoaiBieuGia");

            migrationBuilder.RenameColumn(
                name: "DM_VungId",
                table: "DM_LoaiBieuGia",
                newName: "VungID");

            migrationBuilder.RenameIndex(
                name: "IX_DM_LoaiBieuGia_DM_VungId",
                table: "DM_LoaiBieuGia",
                newName: "IX_DM_LoaiBieuGia_VungID");

            migrationBuilder.AddForeignKey(
                name: "FK_DM_LoaiBieuGia_DM_Vung_VungID",
                table: "DM_LoaiBieuGia",
                column: "VungID",
                principalTable: "DM_Vung",
                principalColumn: "Id");
        }
    }
}
