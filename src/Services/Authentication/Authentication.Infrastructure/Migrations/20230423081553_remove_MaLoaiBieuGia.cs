using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Authentication.Infrastructure.Migrations
{
    public partial class remove_MaLoaiBieuGia : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "MaLoaiBieuGia",
                table: "DM_LoaiBieuGia");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "MaLoaiBieuGia",
                table: "DM_LoaiBieuGia",
                type: "nvarchar(20)",
                maxLength: 20,
                nullable: true);
        }
    }
}
