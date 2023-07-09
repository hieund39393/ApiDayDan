using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Authentication.Infrastructure.Migrations
{
    public partial class add_menu_child : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<Guid>(
                name: "ParenId",
                table: "AUTH_Menu",
                type: "uniqueidentifier",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_AUTH_Menu_ParenId",
                table: "AUTH_Menu",
                column: "ParenId");

            migrationBuilder.AddForeignKey(
                name: "FK_AUTH_Menu_AUTH_Menu_ParenId",
                table: "AUTH_Menu",
                column: "ParenId",
                principalTable: "AUTH_Menu",
                principalColumn: "Id");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_AUTH_Menu_AUTH_Menu_ParenId",
                table: "AUTH_Menu");

            migrationBuilder.DropIndex(
                name: "IX_AUTH_Menu_ParenId",
                table: "AUTH_Menu");

            migrationBuilder.DropColumn(
                name: "ParenId",
                table: "AUTH_Menu");
        }
    }
}
