using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Authentication.Infrastructure.Migrations
{
    public partial class ok : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "AUTH_Action",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false, comment: "Id bảng, khóa chính"),
                    ModuleCode = table.Column<string>(type: "nvarchar(10)", maxLength: 10, nullable: true),
                    ModuleName = table.Column<string>(type: "nvarchar(250)", maxLength: 250, nullable: true),
                    ActionCode = table.Column<string>(type: "nvarchar(10)", maxLength: 10, nullable: true),
                    ActionName = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    EntityId = table.Column<int>(type: "int", nullable: false),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false, comment: "Cờ xóa"),
                    CreatedDate = table.Column<DateTime>(type: "datetime2", nullable: false, comment: "Ngày tạo"),
                    CreatedBy = table.Column<Guid>(type: "uniqueidentifier", nullable: true, comment: "Mã người tạo"),
                    UpdatedDate = table.Column<DateTime>(type: "datetime2", nullable: true, comment: "Ngày cập nhật"),
                    UpdatedBy = table.Column<Guid>(type: "uniqueidentifier", nullable: true, comment: "Mã người cập nhật")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AUTH_Action", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Department",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false, comment: "Id bảng, khóa chính"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Code = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Index = table.Column<int>(type: "int", nullable: true),
                    EntityId = table.Column<int>(type: "int", nullable: false),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false, comment: "Cờ xóa"),
                    CreatedDate = table.Column<DateTime>(type: "datetime2", nullable: false, comment: "Ngày tạo"),
                    CreatedBy = table.Column<Guid>(type: "uniqueidentifier", nullable: true, comment: "Mã người tạo"),
                    UpdatedDate = table.Column<DateTime>(type: "datetime2", nullable: true, comment: "Ngày cập nhật"),
                    UpdatedBy = table.Column<Guid>(type: "uniqueidentifier", nullable: true, comment: "Mã người cập nhật")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Department", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Module",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false, comment: "Id bảng, khóa chính"),
                    ModuleName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ModuleCode = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Url = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    NumberOrder = table.Column<int>(type: "int", nullable: false),
                    Icon = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ParentId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    ModuleParentId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    EntityId = table.Column<int>(type: "int", nullable: false),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false, comment: "Cờ xóa"),
                    CreatedDate = table.Column<DateTime>(type: "datetime2", nullable: false, comment: "Ngày tạo"),
                    CreatedBy = table.Column<Guid>(type: "uniqueidentifier", nullable: true, comment: "Mã người tạo"),
                    UpdatedDate = table.Column<DateTime>(type: "datetime2", nullable: true, comment: "Ngày cập nhật"),
                    UpdatedBy = table.Column<Guid>(type: "uniqueidentifier", nullable: true, comment: "Mã người cập nhật")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Module", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Module_Module_ModuleParentId",
                        column: x => x.ModuleParentId,
                        principalTable: "Module",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "SystemLogs",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Message = table.Column<string>(type: "nvarchar(max)", nullable: true, comment: "Chi tiết lỗi"),
                    MessageTemplate = table.Column<string>(type: "nvarchar(max)", nullable: true, comment: "Chi tiết lỗi"),
                    Level = table.Column<string>(type: "nvarchar(max)", nullable: true, comment: "Mức độ lỗi"),
                    TimeStamp = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Exception = table.Column<string>(type: "nvarchar(max)", nullable: true, comment: "Ngoại lệ"),
                    LogEvent = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: true),
                    UserName = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: true, comment: "Tên user"),
                    IP = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: true, comment: "Địa chỉ IP"),
                    Properties = table.Column<string>(type: "xml", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SystemLogs", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Team",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false, comment: "Id bảng, khóa chính"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Code = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    EntityId = table.Column<int>(type: "int", nullable: false),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false, comment: "Cờ xóa"),
                    CreatedDate = table.Column<DateTime>(type: "datetime2", nullable: false, comment: "Ngày tạo"),
                    CreatedBy = table.Column<Guid>(type: "uniqueidentifier", nullable: true, comment: "Mã người tạo"),
                    UpdatedDate = table.Column<DateTime>(type: "datetime2", nullable: true, comment: "Ngày cập nhật"),
                    UpdatedBy = table.Column<Guid>(type: "uniqueidentifier", nullable: true, comment: "Mã người cập nhật")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Team", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Unit",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false, comment: "Id bảng, khóa chính"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Code = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    EntityId = table.Column<int>(type: "int", nullable: false),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false, comment: "Cờ xóa"),
                    CreatedDate = table.Column<DateTime>(type: "datetime2", nullable: false, comment: "Ngày tạo"),
                    CreatedBy = table.Column<Guid>(type: "uniqueidentifier", nullable: true, comment: "Mã người tạo"),
                    UpdatedDate = table.Column<DateTime>(type: "datetime2", nullable: true, comment: "Ngày cập nhật"),
                    UpdatedBy = table.Column<Guid>(type: "uniqueidentifier", nullable: true, comment: "Mã người cập nhật")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Unit", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "AUTH_Roles",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    ModuleId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    CreatedDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    CreatedBy = table.Column<Guid>(type: "uniqueidentifier", maxLength: 36, nullable: true),
                    UpdatedDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    UpdatedBy = table.Column<Guid>(type: "uniqueidentifier", maxLength: 36, nullable: true),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false, defaultValue: false),
                    ConcurrencyStamp = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Type = table.Column<int>(type: "int", nullable: true),
                    Name = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: false),
                    NormalizedName = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AUTH_Roles", x => x.Id);
                    table.ForeignKey(
                        name: "FK_AUTH_Roles_Module_ModuleId",
                        column: x => x.ModuleId,
                        principalTable: "Module",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "AUTH_RoleClaims",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CreatedDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    CreatedBy = table.Column<Guid>(type: "uniqueidentifier", maxLength: 36, nullable: true),
                    UpdatedDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    UpdatedBy = table.Column<Guid>(type: "uniqueidentifier", maxLength: 36, nullable: true),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false, defaultValue: false),
                    RoleId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    ClaimType = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    ClaimValue = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AUTH_RoleClaims", x => x.Id);
                    table.ForeignKey(
                        name: "FK_AUTH_RoleClaims_AUTH_Roles_RoleId",
                        column: x => x.RoleId,
                        principalTable: "AUTH_Roles",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AUTH_Users",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true, comment: "Tên người dùng"),
                    NameUnsigned = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true, comment: "Tên người dùng không dấu"),
                    Avatar = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true, comment: "Ảnh đại diện"),
                    IsSuperAdmin = table.Column<bool>(type: "bit", nullable: false, comment: "Cờ superadmin"),
                    Actived = table.Column<bool>(type: "bit", nullable: false, comment: "Cờ kích hoạt"),
                    Dob = table.Column<DateTime>(type: "datetime2", nullable: true, comment: "Ngày sinh"),
                    SSO = table.Column<bool>(type: "bit", nullable: false),
                    CreatedDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    CreatedBy = table.Column<Guid>(type: "uniqueidentifier", maxLength: 36, nullable: true),
                    UpdatedDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    UpdatedBy = table.Column<Guid>(type: "uniqueidentifier", maxLength: 36, nullable: true),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false, defaultValue: false),
                    CMIS_CODE = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    UnitId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    DepartmentId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    TeamId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    RoleId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    Position = table.Column<int>(type: "int", nullable: false),
                    Gender = table.Column<int>(type: "int", nullable: true),
                    Index = table.Column<int>(type: "int", nullable: true),
                    UserName = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: false),
                    NormalizedUserName = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    Email = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: false),
                    NormalizedEmail = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    EmailConfirmed = table.Column<bool>(type: "bit", nullable: false),
                    PasswordHash = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    SecurityStamp = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ConcurrencyStamp = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    PhoneNumber = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: true),
                    PhoneNumberConfirmed = table.Column<bool>(type: "bit", nullable: false),
                    TwoFactorEnabled = table.Column<bool>(type: "bit", nullable: false),
                    LockoutEnd = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: true),
                    LockoutEnabled = table.Column<bool>(type: "bit", nullable: false),
                    AccessFailedCount = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AUTH_Users", x => x.Id);
                    table.ForeignKey(
                        name: "FK_AUTH_Users_AUTH_Roles_RoleId",
                        column: x => x.RoleId,
                        principalTable: "AUTH_Roles",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_AUTH_Users_Department_DepartmentId",
                        column: x => x.DepartmentId,
                        principalTable: "Department",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_AUTH_Users_Team_TeamId",
                        column: x => x.TeamId,
                        principalTable: "Team",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_AUTH_Users_Unit_UnitId",
                        column: x => x.UnitId,
                        principalTable: "Unit",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "AUTH_UserClaims",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CreatedDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    CreatedBy = table.Column<Guid>(type: "uniqueidentifier", maxLength: 36, nullable: true),
                    UpdatedDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    UpdatedBy = table.Column<Guid>(type: "uniqueidentifier", maxLength: 36, nullable: true),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false, defaultValue: false),
                    UserId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    ClaimType = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    ClaimValue = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AUTH_UserClaims", x => x.Id);
                    table.ForeignKey(
                        name: "FK_AUTH_UserClaims_AUTH_Users_UserId",
                        column: x => x.UserId,
                        principalTable: "AUTH_Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AUTH_UserLogins",
                columns: table => new
                {
                    LoginProvider = table.Column<string>(type: "nvarchar(10)", maxLength: 10, nullable: false),
                    ProviderKey = table.Column<string>(type: "nvarchar(10)", maxLength: 10, nullable: false),
                    CreatedDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    CreatedBy = table.Column<Guid>(type: "uniqueidentifier", maxLength: 36, nullable: true),
                    UpdatedDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    UpdatedBy = table.Column<Guid>(type: "uniqueidentifier", maxLength: 36, nullable: true),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false, defaultValue: false),
                    ProviderDisplayName = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: true),
                    UserId = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AUTH_UserLogins", x => new { x.LoginProvider, x.ProviderKey });
                    table.ForeignKey(
                        name: "FK_AUTH_UserLogins_AUTH_Users_UserId",
                        column: x => x.UserId,
                        principalTable: "AUTH_Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AUTH_UserRoles",
                columns: table => new
                {
                    UserId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    RoleId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    CreatedDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    CreatedBy = table.Column<Guid>(type: "uniqueidentifier", maxLength: 36, nullable: true),
                    UpdatedDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    UpdatedBy = table.Column<Guid>(type: "uniqueidentifier", maxLength: 36, nullable: true),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false, defaultValue: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AUTH_UserRoles", x => new { x.UserId, x.RoleId });
                    table.ForeignKey(
                        name: "FK_AUTH_UserRoles_AUTH_Roles_RoleId",
                        column: x => x.RoleId,
                        principalTable: "AUTH_Roles",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_AUTH_UserRoles_AUTH_Users_UserId",
                        column: x => x.UserId,
                        principalTable: "AUTH_Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AUTH_UserTokens",
                columns: table => new
                {
                    Value = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    CreatedDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    CreatedBy = table.Column<Guid>(type: "uniqueidentifier", maxLength: 36, nullable: true),
                    UpdatedDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    UpdatedBy = table.Column<Guid>(type: "uniqueidentifier", maxLength: 36, nullable: true),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false, defaultValue: false),
                    UserId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    LoginProvider = table.Column<string>(type: "nvarchar(10)", maxLength: 10, nullable: false),
                    Name = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AUTH_UserTokens", x => x.Value);
                    table.ForeignKey(
                        name: "FK_AUTH_UserTokens_AUTH_Users_UserId",
                        column: x => x.UserId,
                        principalTable: "AUTH_Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_AUTH_RoleClaims_RoleId",
                table: "AUTH_RoleClaims",
                column: "RoleId");

            migrationBuilder.CreateIndex(
                name: "IX_AUTH_Roles_ModuleId",
                table: "AUTH_Roles",
                column: "ModuleId");

            migrationBuilder.CreateIndex(
                name: "RoleNameIndex",
                table: "AUTH_Roles",
                column: "NormalizedName",
                unique: true,
                filter: "[NormalizedName] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_AUTH_UserClaims_UserId",
                table: "AUTH_UserClaims",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_AUTH_UserLogins_UserId",
                table: "AUTH_UserLogins",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_AUTH_UserRoles_RoleId",
                table: "AUTH_UserRoles",
                column: "RoleId");

            migrationBuilder.CreateIndex(
                name: "IX_AUTH_UserRoles_UserId",
                table: "AUTH_UserRoles",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "EmailIndex",
                table: "AUTH_Users",
                column: "NormalizedEmail");

            migrationBuilder.CreateIndex(
                name: "IX_AUTH_Users_DepartmentId",
                table: "AUTH_Users",
                column: "DepartmentId");

            migrationBuilder.CreateIndex(
                name: "IX_AUTH_Users_Email",
                table: "AUTH_Users",
                column: "Email");

            migrationBuilder.CreateIndex(
                name: "IX_AUTH_Users_RoleId",
                table: "AUTH_Users",
                column: "RoleId");

            migrationBuilder.CreateIndex(
                name: "IX_AUTH_Users_TeamId",
                table: "AUTH_Users",
                column: "TeamId");

            migrationBuilder.CreateIndex(
                name: "IX_AUTH_Users_UnitId",
                table: "AUTH_Users",
                column: "UnitId");

            migrationBuilder.CreateIndex(
                name: "IX_AUTH_Users_UserName",
                table: "AUTH_Users",
                column: "UserName",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "UserNameIndex",
                table: "AUTH_Users",
                column: "NormalizedUserName",
                unique: true,
                filter: "[NormalizedUserName] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_AUTH_UserTokens_UserId",
                table: "AUTH_UserTokens",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_Module_ModuleParentId",
                table: "Module",
                column: "ModuleParentId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "AUTH_Action");

            migrationBuilder.DropTable(
                name: "AUTH_RoleClaims");

            migrationBuilder.DropTable(
                name: "AUTH_UserClaims");

            migrationBuilder.DropTable(
                name: "AUTH_UserLogins");

            migrationBuilder.DropTable(
                name: "AUTH_UserRoles");

            migrationBuilder.DropTable(
                name: "AUTH_UserTokens");

            migrationBuilder.DropTable(
                name: "SystemLogs");

            migrationBuilder.DropTable(
                name: "AUTH_Users");

            migrationBuilder.DropTable(
                name: "AUTH_Roles");

            migrationBuilder.DropTable(
                name: "Department");

            migrationBuilder.DropTable(
                name: "Team");

            migrationBuilder.DropTable(
                name: "Unit");

            migrationBuilder.DropTable(
                name: "Module");
        }
    }
}
