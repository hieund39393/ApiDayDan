using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Authentication.Infrastructure.Migrations
{
    public partial class MTC_MA : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "MaVatLieu",
                table: "DM_MTC",
                newName: "MaMTC");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "MaMTC",
                table: "DM_MTC",
                newName: "MaVatLieu");
        }
    }
}
