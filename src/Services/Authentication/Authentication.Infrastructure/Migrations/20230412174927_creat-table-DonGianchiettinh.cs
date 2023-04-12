using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Authentication.Infrastructure.Migrations
{
    public partial class creattableDonGianchiettinh : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "DonGiaChietTinh",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false, comment: "Id bảng, khóa chính"),
                    IdVatLieu = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    DonGia = table.Column<decimal>(type: "numeric(18,1)", nullable: false),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false, defaultValue: false, comment: "Cờ xóa"),
                    CreatedDate = table.Column<DateTime>(type: "datetime2", nullable: false, comment: "Ngày tạo"),
                    CreatedBy = table.Column<Guid>(type: "uniqueidentifier", maxLength: 36, nullable: true, comment: "Mã người tạo"),
                    UpdatedDate = table.Column<DateTime>(type: "datetime2", nullable: true, comment: "Ngày cập nhật"),
                    UpdatedBy = table.Column<Guid>(type: "uniqueidentifier", maxLength: 36, nullable: true, comment: "Mã người cập nhật")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DonGiaChietTinh", x => x.Id);
                    table.ForeignKey(
                        name: "FK_DonGiaChietTinh_DM_VatLieu_IdVatLieu",
                        column: x => x.IdVatLieu,
                        principalTable: "DM_VatLieu",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateIndex(
                name: "IX_DonGiaChietTinh_IdVatLieu",
                table: "DonGiaChietTinh",
                column: "IdVatLieu");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "DonGiaChietTinh");
        }
    }
}
