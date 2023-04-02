using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Authentication.Infrastructure.Migrations
{
    public partial class add_configuration_BieuGia : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateIndex(
                name: "IX_DM_BieuGia_idLoaiBieuGia",
                table: "DM_BieuGia",
                column: "idLoaiBieuGia");

            migrationBuilder.AddForeignKey(
                name: "FK_DM_BieuGia_DM_LoaiBieuGia_idLoaiBieuGia",
                table: "DM_BieuGia",
                column: "idLoaiBieuGia",
                principalTable: "DM_LoaiBieuGia",
                principalColumn: "Id");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_DM_BieuGia_DM_LoaiBieuGia_idLoaiBieuGia",
                table: "DM_BieuGia");

            migrationBuilder.DropIndex(
                name: "IX_DM_BieuGia_idLoaiBieuGia",
                table: "DM_BieuGia");
        }
    }
}
