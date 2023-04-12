using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Authentication.Infrastructure.Migrations
{
    public partial class creattableDonGianNhanCong : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "DonGiaNhanCong",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false, comment: "Id bảng, khóa chính"),
                    CapBac = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    HeSo = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    DonGia = table.Column<decimal>(type: "numeric(18,1)", nullable: false),
                    IdVung = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    IdKhuVuc = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false, defaultValue: false, comment: "Cờ xóa"),
                    CreatedDate = table.Column<DateTime>(type: "datetime2", nullable: false, comment: "Ngày tạo"),
                    CreatedBy = table.Column<Guid>(type: "uniqueidentifier", maxLength: 36, nullable: true, comment: "Mã người tạo"),
                    UpdatedDate = table.Column<DateTime>(type: "datetime2", nullable: true, comment: "Ngày cập nhật"),
                    UpdatedBy = table.Column<Guid>(type: "uniqueidentifier", maxLength: 36, nullable: true, comment: "Mã người cập nhật")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DonGiaNhanCong", x => x.Id);
                    table.ForeignKey(
                        name: "FK_DonGiaNhanCong_DM_KhuVuc_IdKhuVuc",
                        column: x => x.IdKhuVuc,
                        principalTable: "DM_KhuVuc",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_DonGiaNhanCong_DM_Vung_IdVung",
                        column: x => x.IdVung,
                        principalTable: "DM_Vung",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateIndex(
                name: "IX_DonGiaNhanCong_IdKhuVuc",
                table: "DonGiaNhanCong",
                column: "IdKhuVuc");

            migrationBuilder.CreateIndex(
                name: "IX_DonGiaNhanCong_IdVung",
                table: "DonGiaNhanCong",
                column: "IdVung");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "DonGiaNhanCong");
        }
    }
}
