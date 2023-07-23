using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Authentication.Infrastructure.Migrations
{
    public partial class ChiTietChietTinh : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "ChietTinhChiTiet",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false, comment: "Id bảng, khóa chính"),
                    IdDonGiaChietTinh = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    IdCongViec = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    IdChiTiet = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    DinhMuc = table.Column<decimal>(type: "decimal(18,2)", nullable: true),
                    DonGiaChietTinhId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false, comment: "Cờ xóa"),
                    CreatedDate = table.Column<DateTime>(type: "datetime2", nullable: false, comment: "Ngày tạo"),
                    CreatedBy = table.Column<Guid>(type: "uniqueidentifier", nullable: true, comment: "Mã người tạo"),
                    UpdatedDate = table.Column<DateTime>(type: "datetime2", nullable: true, comment: "Ngày cập nhật"),
                    UpdatedBy = table.Column<Guid>(type: "uniqueidentifier", nullable: true, comment: "Mã người cập nhật")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ChietTinhChiTiet", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ChietTinhChiTiet_DonGiaChietTinh_DonGiaChietTinhId",
                        column: x => x.DonGiaChietTinhId,
                        principalTable: "DonGiaChietTinh",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateIndex(
                name: "IX_ChietTinhChiTiet_DonGiaChietTinhId",
                table: "ChietTinhChiTiet",
                column: "DonGiaChietTinhId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ChietTinhChiTiet");
        }
    }
}
