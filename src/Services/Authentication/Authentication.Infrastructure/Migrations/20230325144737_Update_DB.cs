using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Authentication.Infrastructure.Migrations
{
    public partial class Update_DB : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_AUTH_Users_UserName",
                table: "AUTH_Users");

            migrationBuilder.DropColumn(
                name: "CMIS_CODE",
                table: "AUTH_Users");

            migrationBuilder.DropColumn(
                name: "Index",
                table: "AUTH_Users");

            migrationBuilder.DropColumn(
                name: "Position",
                table: "AUTH_Users");

            migrationBuilder.AlterColumn<string>(
                name: "UserName",
                table: "AUTH_Users",
                type: "nvarchar(56)",
                maxLength: 56,
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(256)",
                oldMaxLength: 256);

            migrationBuilder.AlterColumn<string>(
                name: "Email",
                table: "AUTH_Users",
                type: "nvarchar(256)",
                maxLength: 256,
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(256)",
                oldMaxLength: 256);

            migrationBuilder.AddColumn<Guid>(
                name: "PositionId",
                table: "AUTH_Users",
                type: "uniqueidentifier",
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "IsActive",
                table: "AUTH_Menu",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.CreateTable(
                name: "AUTH_Permission",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false, comment: "Id bảng, khóa chính"),
                    Code = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    Name = table.Column<string>(type: "nvarchar(150)", maxLength: 150, nullable: false),
                    MenuId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false, defaultValue: false, comment: "Cờ xóa"),
                    CreatedDate = table.Column<DateTime>(type: "datetime2", nullable: false, comment: "Ngày tạo"),
                    CreatedBy = table.Column<Guid>(type: "uniqueidentifier", maxLength: 36, nullable: true, comment: "Mã người tạo"),
                    UpdatedDate = table.Column<DateTime>(type: "datetime2", nullable: true, comment: "Ngày cập nhật"),
                    UpdatedBy = table.Column<Guid>(type: "uniqueidentifier", maxLength: 36, nullable: true, comment: "Mã người cập nhật")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AUTH_Permission", x => x.Id);
                    table.ForeignKey(
                        name: "FK_AUTH_Permission_AUTH_Menu_MenuId",
                        column: x => x.MenuId,
                        principalTable: "AUTH_Menu",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AUTH_Position",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false, comment: "Id bảng, khóa chính"),
                    Value = table.Column<int>(type: "int", nullable: false),
                    Title = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false, comment: "Cờ xóa"),
                    CreatedDate = table.Column<DateTime>(type: "datetime2", nullable: false, comment: "Ngày tạo"),
                    CreatedBy = table.Column<Guid>(type: "uniqueidentifier", nullable: true, comment: "Mã người tạo"),
                    UpdatedDate = table.Column<DateTime>(type: "datetime2", nullable: true, comment: "Ngày cập nhật"),
                    UpdatedBy = table.Column<Guid>(type: "uniqueidentifier", nullable: true, comment: "Mã người cập nhật")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AUTH_Position", x => x.Id);
                });

            migrationBuilder.CreateIndex(
                name: "IX_AUTH_Users_PositionId",
                table: "AUTH_Users",
                column: "PositionId");

            migrationBuilder.CreateIndex(
                name: "IX_AUTH_Users_UserName",
                table: "AUTH_Users",
                column: "UserName",
                unique: true,
                filter: "[UserName] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_AUTH_Permission_Code",
                table: "AUTH_Permission",
                column: "Code");

            migrationBuilder.CreateIndex(
                name: "IX_AUTH_Permission_MenuId",
                table: "AUTH_Permission",
                column: "MenuId");

            migrationBuilder.AddForeignKey(
                name: "FK_AUTH_Users_AUTH_Position_PositionId",
                table: "AUTH_Users",
                column: "PositionId",
                principalTable: "AUTH_Position",
                principalColumn: "Id");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_AUTH_Users_AUTH_Position_PositionId",
                table: "AUTH_Users");

            migrationBuilder.DropTable(
                name: "AUTH_Permission");

            migrationBuilder.DropTable(
                name: "AUTH_Position");

            migrationBuilder.DropIndex(
                name: "IX_AUTH_Users_PositionId",
                table: "AUTH_Users");

            migrationBuilder.DropIndex(
                name: "IX_AUTH_Users_UserName",
                table: "AUTH_Users");

            migrationBuilder.DropColumn(
                name: "PositionId",
                table: "AUTH_Users");

            migrationBuilder.DropColumn(
                name: "IsActive",
                table: "AUTH_Menu");

            migrationBuilder.AlterColumn<string>(
                name: "UserName",
                table: "AUTH_Users",
                type: "nvarchar(256)",
                maxLength: 256,
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(56)",
                oldMaxLength: 56,
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "Email",
                table: "AUTH_Users",
                type: "nvarchar(256)",
                maxLength: 256,
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(256)",
                oldMaxLength: 256,
                oldNullable: true);

            migrationBuilder.AddColumn<string>(
                name: "CMIS_CODE",
                table: "AUTH_Users",
                type: "nvarchar(50)",
                maxLength: 50,
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "Index",
                table: "AUTH_Users",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "Position",
                table: "AUTH_Users",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_AUTH_Users_UserName",
                table: "AUTH_Users",
                column: "UserName",
                unique: true);
        }
    }
}
