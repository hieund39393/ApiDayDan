using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Authentication.Infrastructure.Migrations
{
    public partial class add_Don_GiaCap : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_GiaCap_DM_LoaiCap_IdLoaiCap",
                table: "GiaCap");

            migrationBuilder.DropPrimaryKey(
                name: "PK_GiaCap",
                table: "GiaCap");

            migrationBuilder.RenameTable(
                name: "GiaCap",
                newName: "DonGiaCap");

            migrationBuilder.RenameIndex(
                name: "IX_GiaCap_IdLoaiCap",
                table: "DonGiaCap",
                newName: "IX_DonGiaCap_IdLoaiCap");

            migrationBuilder.AddPrimaryKey(
                name: "PK_DonGiaCap",
                table: "DonGiaCap",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_DonGiaCap_DM_LoaiCap_IdLoaiCap",
                table: "DonGiaCap",
                column: "IdLoaiCap",
                principalTable: "DM_LoaiCap",
                principalColumn: "Id");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_DonGiaCap_DM_LoaiCap_IdLoaiCap",
                table: "DonGiaCap");

            migrationBuilder.DropPrimaryKey(
                name: "PK_DonGiaCap",
                table: "DonGiaCap");

            migrationBuilder.RenameTable(
                name: "DonGiaCap",
                newName: "GiaCap");

            migrationBuilder.RenameIndex(
                name: "IX_DonGiaCap_IdLoaiCap",
                table: "GiaCap",
                newName: "IX_GiaCap_IdLoaiCap");

            migrationBuilder.AddPrimaryKey(
                name: "PK_GiaCap",
                table: "GiaCap",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_GiaCap_DM_LoaiCap_IdLoaiCap",
                table: "GiaCap",
                column: "IdLoaiCap",
                principalTable: "DM_LoaiCap",
                principalColumn: "Id");
        }
    }
}
