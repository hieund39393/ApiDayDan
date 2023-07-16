using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Authentication.Infrastructure.Migrations
{
    public partial class UP_config3 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<bool>(
                name: "IsDeleted",
                table: "DM_MTC",
                type: "bit",
                nullable: false,
                defaultValue: false,
                comment: "Cờ xóa",
                oldClrType: typeof(bool),
                oldType: "bit",
                oldComment: "Cờ xóa");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<bool>(
                name: "IsDeleted",
                table: "DM_MTC",
                type: "bit",
                nullable: false,
                comment: "Cờ xóa",
                oldClrType: typeof(bool),
                oldType: "bit",
                oldDefaultValue: false,
                oldComment: "Cờ xóa");
        }
    }
}
