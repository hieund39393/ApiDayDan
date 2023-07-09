﻿using Authentication.Infrastructure.AggregatesModel.DM_KhuVucAggregate;
using Authentication.Infrastructure.AggregatesModel.DM_LoaiBieuGiaAggregate;
using Authentication.Infrastructure.AggregatesModel.MenuAggregate;
using Authentication.Infrastructure.AggregatesModel.ModuleAggregate;
using Authentication.Infrastructure.AggregatesModel.PermissionAggregate;
using Authentication.Infrastructure.AggregatesModel.PositionAggregate;
using Authentication.Infrastructure.AggregatesModel.UserAggregate;
using Microsoft.AspNetCore.Identity;
using System;
using System.Security;
using System.Threading.Tasks;
using static EVN.Core.Common.AppConstants;
using static EVN.Core.Common.AppEnum;

namespace Authentication.Infrastructure.EF
{
    public class ExOneDbContextSeed
    {
        public async Task SeedAsync(ExOneDbContext context, UserManager<User> userManager)
        {
            var user = await userManager.FindByNameAsync("admin");
            if (user == null)
            {
                user = new User
                {
                    UserName = "admin",
                    Name = "admin",
                    NameUnsigned = "admin",
                    Email = "admin@gmail.com",
                    PhoneNumber = "0983123456",
                    IsSuperAdmin = true,
                    Actived = true,
                    CreatedDate = System.DateTime.UtcNow,
                };
                var createResult = await userManager.CreateAsync(user);
                System.Console.WriteLine($"createResult.Succeeded = {createResult.Succeeded}");
                System.Console.WriteLine($"createResult.Errors = {Newtonsoft.Json.JsonConvert.SerializeObject(createResult.Errors)}");
                if (createResult.Succeeded)
                {
                    var result = await userManager.AddPasswordAsync(user, "Admin@123");
                    System.Console.WriteLine($"result.Errors = {Newtonsoft.Json.JsonConvert.SerializeObject(result.Errors)}");
                }
            }
            if (!context.Module.Any())
            {
                List<Module> listModule = new List<Module>();

                listModule.Add(new Module { Id = new Guid("C09E6504B3DBC74180EB85C76EB329C1"), Code = Permissions.QTHT, Name = "Quản trị hệ thống", Order = 1, Icon = "SettingOutlined" });
                listModule.Add(new Module { Id = new Guid("C09E6504B3DBC74180EB85C76EB329C2"), Code = Permissions.QTDM, Name = "Quản trị danh mục", Order = 2, Icon = "FormOutlined" });
                //listModule.Add(new Module { Id = new Guid("C09E6504B3DBC74180EB85C76EB329C3"), Code = Permissions.QLDMBG, Name = "Quản lý danh mục biểu giá", Order = 3, Icon = "OrderedListOutlined" });
                listModule.Add(new Module { Id = new Guid("C09E6504B3DBC74180EB85C76EB329C4"), Code = Permissions.QLTTDG, Name = "Quản lý thông tin đơn giá", Order = 4, Icon = "BarChartOutlined" });
                listModule.Add(new Module { Id = new Guid("C09E6504B3DBC74180EB85C76EB329C5"), Code = Permissions.QLTTCTBG, Name = "Quản lý thông tin chi tiết biểu giá", Order = 5, Icon = "FundOutlined" });
                listModule.Add(new Module { Id = new Guid("C09E6504B3DBC74180EB85C76EB329C6"), Code = Permissions.HTBC, Name = "Hệ thống báo cáo", Order = 6, Icon = "PieChartOutlined" });

                context.Module.AddRange(listModule);
                await context.SaveChangesAsync();
            }
            if (!context.Menu.Any())
            {
                List<Menu> listMenu = new List<Menu>();
                // các trang của module quản trị hệ thống
                listMenu.Add(new Menu { Id = new Guid("1E0A058FD1323C46ABE8061A890DC9EC"), Code = $"{Permissions.User}", Name = "Quản lý người dùng", Url = "/nguoi-dung", Order = 1, ModuleId = new Guid("C09E6504B3DBC74180EB85C76EB329C1") });
                listMenu.Add(new Menu { Id = new Guid("E17A625E0334C04D928300BBF8C83828"), Code = $"{Permissions.Menu}", Name = "Quản lý menu", Url = "/menu", Order = 2, ModuleId = new Guid("C09E6504B3DBC74180EB85C76EB329C1") });
                listMenu.Add(new Menu { Id = new Guid("E17A625E0334C04D928300BBF8C83829"), Code = $"{Permissions.Role}", Name = "Quản lý phân quyền", Url = "/phan-quyen", Order = 3, ModuleId = new Guid("C09E6504B3DBC74180EB85C76EB329C1") });

                // QUẢN TRỊ DANH MỤC

                listMenu.Add(new Menu { Id = new Guid("E17A625E0334C04D928300BBF8C83111"), Code = $"{Permissions.CapTrenKhong}", Name = "Cáp trên không", Url = null, Order = 0, ModuleId = new Guid("C09E6504B3DBC74180EB85C76EB329C2") });
                listMenu.Add(new Menu { Id = new Guid("E17A625E0334C04D928300BBF8C83222"), Code = $"{Permissions.CapNgam}", Name = "Cáp ngầm", Url = null, Order = 0, ModuleId = new Guid("C09E6504B3DBC74180EB85C76EB329C2") });

                listMenu.Add(new Menu { ParenId = new Guid("E17A625E0334C04D928300BBF8C83111"), Id = new Guid("E17A625E0334C04D928300BBF8C83830"), Code = $"{Permissions.LoaiBieuGia}", Name = "Quản lý loại biểu giá", Url = "/loai-bieu-gia", Order = 1, ModuleId = new Guid("C09E6504B3DBC74180EB85C76EB329C2") });
                listMenu.Add(new Menu { ParenId = new Guid("E17A625E0334C04D928300BBF8C83111"), Id = new Guid("E17A625E0334C04D928300BBF8C83832"), Code = $"{Permissions.CongViec}", Name = "Quản lý công việc", Url = "/cong-viec", Order = 3, ModuleId = new Guid("C09E6504B3DBC74180EB85C76EB329C2") });
                listMenu.Add(new Menu { ParenId = new Guid("E17A625E0334C04D928300BBF8C83111"), Id = new Guid("E17A625E0334C04D928300BBF8C83831"), Code = $"{Permissions.BieuGia}", Name = "Quản lý biểu giá", Url = "/bieu-gia", Order = 2, ModuleId = new Guid("C09E6504B3DBC74180EB85C76EB329C2") });

                listMenu.Add(new Menu { ParenId = new Guid("E17A625E0334C04D928300BBF8C83111"), Id = new Guid("E17A625E0334C04D928300BBF8C83835"), Code = $"{Permissions.LoaiCap}", Name = "Quản lý loại cáp", Url = "/loai-cap", Order = 6, ModuleId = new Guid("C09E6504B3DBC74180EB85C76EB329C2") });
                listMenu.Add(new Menu { ParenId = new Guid("E17A625E0334C04D928300BBF8C83111"), Id = new Guid("E17A625E0334C04D928300BBF8C83836"), Code = $"{Permissions.VatLieu}", Name = "Quản lý vật liệu", Url = "/vat-lieu", Order = 7, ModuleId = new Guid("C09E6504B3DBC74180EB85C76EB329C2") });
                listMenu.Add(new Menu { ParenId = new Guid("E17A625E0334C04D928300BBF8C83111"), Id = new Guid("E17A625E0334C04D928300BBF8C83837"), Code = $"{Permissions.VatLieuChietTinh}", Name = "Quản lý vật liệu chiết tinh", Url = "/vat-lieu-chiet-tinh", Order = 8, ModuleId = new Guid("C09E6504B3DBC74180EB85C76EB329C2") });
                listMenu.Add(new Menu { ParenId = new Guid("E17A625E0334C04D928300BBF8C83111"), Id = new Guid("E17A625E0334C04D928300BBF8C83843"), Code = $"{Permissions.BieuGiaCongViec}", Name = "Biểu giá công việc", Url = "/bieu-gia-cong-viec", Order = 4, ModuleId = new Guid("C09E6504B3DBC74180EB85C76EB329C2") });
                // cáp trên không
                listMenu.Add(new Menu { ParenId = new Guid("E17A625E0334C04D928300BBF8C83222"), Id = new Guid("E17A625E0334C04D928300BBF8C88000"), Code = $"{Permissions.LoaiBieuGiaCapNgam}", Name = "Quản lý loại biểu giá", Url = "/loai-bieu-gia-cap-ngam", Order = 1, ModuleId = new Guid("C09E6504B3DBC74180EB85C76EB329C2") });
                listMenu.Add(new Menu { ParenId = new Guid("E17A625E0334C04D928300BBF8C83222"), Id = new Guid("E17A625E0334C04D928300BBF8C88001"), Code = $"{Permissions.BieuGiaCapNgam}", Name = "Quản lý biểu giá", Url = "/bieu-gia-cap-ngam", Order = 2, ModuleId = new Guid("C09E6504B3DBC74180EB85C76EB329C2") });
                listMenu.Add(new Menu { ParenId = new Guid("E17A625E0334C04D928300BBF8C83222"), Id = new Guid("E17A625E0334C04D928300BBF8C88002"), Code = $"{Permissions.CongViecCapNgam}", Name = "Quản lý công việc", Url = "/cong-viec-cap-ngam", Order = 3, ModuleId = new Guid("C09E6504B3DBC74180EB85C76EB329C2") });
                listMenu.Add(new Menu { ParenId = new Guid("E17A625E0334C04D928300BBF8C83222"), Id = new Guid("E17A625E0334C04D928300BBF8C88003"), Code = $"{Permissions.BieuGiaCongViecCapNgam}", Name = "Biểu giá công việc", Url = "/bieu-gia-cong-viec-cap-ngam", Order = 4, ModuleId = new Guid("C09E6504B3DBC74180EB85C76EB329C2") });
                listMenu.Add(new Menu { ParenId = new Guid("E17A625E0334C04D928300BBF8C83222"), Id = new Guid("E17A625E0334C04D928300BBF8C88006"), Code = $"{Permissions.VatLieuCapNgam}", Name = "Quản lý vật liệu", Url = "/vat-lieu-cap-ngam", Order = 7, ModuleId = new Guid("C09E6504B3DBC74180EB85C76EB329C2") });


                //listMenu.Add(new Menu { Id = new Guid("E17A625E0334C04D928300BBF8C83834"), Code = $"{Permissions.KhuVuc}", Name = "Quản lý vùng/khu vực", Url = "/khu-vuc", Order = 5, ModuleId = new Guid("C09E6504B3DBC74180EB85C76EB329C2") });


                // QUẢN TRỊ ĐƠN GIÁ

                listMenu.Add(new Menu { Id = new Guid("C09E6504B3DBC74180EB85C76EB111C4"), Code = $"{Permissions.DGCapTrenKhong}", Name = "Cáp trên không", Url = null, Order = 0, ModuleId = new Guid("C09E6504B3DBC74180EB85C76EB329C4") });
                listMenu.Add(new Menu { Id = new Guid("C09E6504B3DBC74180EB85C76EB222C4"), Code = $"{Permissions.DGCapNgam}", Name = "Cáp ngầm", Url = null, Order = 0, ModuleId = new Guid("C09E6504B3DBC74180EB85C76EB329C4") });

                listMenu.Add(new Menu { ParenId = new Guid("C09E6504B3DBC74180EB85C76EB111C4"), Id = new Guid("E17A625E0334C04D928300BBF8C83838"), Code = $"{Permissions.DonGiaCap}", Name = "Đơn giá cáp", Url = "/don-gia-cap", Order = 1, ModuleId = new Guid("C09E6504B3DBC74180EB85C76EB329C4") });
                listMenu.Add(new Menu { ParenId = new Guid("C09E6504B3DBC74180EB85C76EB111C4"), Id = new Guid("E17A625E0334C04D928300BBF8C83839"), Code = $"{Permissions.DonGiaNhanCong}", Name = "Đơn giá nhân công", Url = "/don-gia-nhan-cong", Order = 2, ModuleId = new Guid("C09E6504B3DBC74180EB85C76EB329C4") });
                listMenu.Add(new Menu { ParenId = new Guid("C09E6504B3DBC74180EB85C76EB111C4"), Id = new Guid("E17A625E0334C04D928300BBF8C83840"), Code = $"{Permissions.DonGiaVatLieu}", Name = "Đơn giá vật liệu", Url = "/don-gia-vat-lieu", Order = 3, ModuleId = new Guid("C09E6504B3DBC74180EB85C76EB329C4") });
                listMenu.Add(new Menu { ParenId = new Guid("C09E6504B3DBC74180EB85C76EB111C4"), Id = new Guid("E17A625E0334C04D928300BBF8C83841"), Code = $"{Permissions.DonGiaVatLieuChietTinh}", Name = "Đơn giá vật liệu chiết tinh", Url = "/don-gia-vat-lieu-chiet-tinh", Order = 4, ModuleId = new Guid("C09E6504B3DBC74180EB85C76EB329C4") });
                // cáp ngầm
                listMenu.Add(new Menu { ParenId = new Guid("C09E6504B3DBC74180EB85C76EB222C4"), Id = new Guid("E17A625E0334C04D928300BBF8C83890"), Code = $"{Permissions.DonGiaNhanCongCapNgam}", Name = "Đơn giá nhân công", Url = "/don-gia-nhan-cong-cap-ngam", Order = 2, ModuleId = new Guid("C09E6504B3DBC74180EB85C76EB329C4") });
                listMenu.Add(new Menu { ParenId = new Guid("C09E6504B3DBC74180EB85C76EB222C4"), Id = new Guid("E17A625E0334C04D928300BBF8C83891"), Code = $"{Permissions.DonGiaVatLieuCapNgam}", Name = "Đơn giá vật liệu", Url = "/don-gia-vat-lieu-cap-ngam", Order = 3, ModuleId = new Guid("C09E6504B3DBC74180EB85C76EB329C4") });

                // QUẢN LÝ THÔNG TIN CHI TIẾT BIỂU GIÁ

                listMenu.Add(new Menu { Id = new Guid("C09E6504B3DBC74180EB85C76EB333C4"), Code = $"{Permissions.CTCapTrenKhong}", Name = "Cáp trên không", Url = null, Order = 0, ModuleId = new Guid("C09E6504B3DBC74180EB85C76EB329C5") });
                listMenu.Add(new Menu { Id = new Guid("C09E6504B3DBC74180EB85C76EB444C4"), Code = $"{Permissions.CTCapNgam}", Name = "Cáp ngầm", Url = null, Order = 0, ModuleId = new Guid("C09E6504B3DBC74180EB85C76EB329C5") });

                listMenu.Add(new Menu { ParenId = new Guid("C09E6504B3DBC74180EB85C76EB333C4"), Id = new Guid("E17A625E0334C04D928300BBF8C83842"), Code = $"{Permissions.ChiTietBieuGia}", Name = "Chi tiết biểu giá", Url = "/chi-tiet-bieu-gia", Order = 1, ModuleId = new Guid("C09E6504B3DBC74180EB85C76EB329C5") });
                listMenu.Add(new Menu { ParenId = new Guid("C09E6504B3DBC74180EB85C76EB333C4"), Id = new Guid("E17A625E0334C04D928300BBF8C83845"), Code = $"{Permissions.GuiDuyetBieuGia}", Name = "Gửi duyệt biểu giá", Url = "/gui-duyet-bieu-gia", Order = 2, ModuleId = new Guid("C09E6504B3DBC74180EB85C76EB329C5") });
                listMenu.Add(new Menu { ParenId = new Guid("C09E6504B3DBC74180EB85C76EB333C4"), Id = new Guid("E17A625E0334C04D928300BBF8C83846"), Code = $"{Permissions.XacNhanBieuGia}", Name = "Xác nhận biểu giá", Url = "/xac-nhan-bieu-gia", Order = 3, ModuleId = new Guid("C09E6504B3DBC74180EB85C76EB329C5") });

                listMenu.Add(new Menu { ParenId = new Guid("C09E6504B3DBC74180EB85C76EB444C4"), Id = new Guid("E17A625E0334C04D928300BBF8C80000"), Code = $"{Permissions.ChiTietBieuGiaCapNgam}", Name = "Chi tiết biểu giá", Url = "/chi-tiet-bieu-gia-cap-ngam", Order = 1, ModuleId = new Guid("C09E6504B3DBC74180EB85C76EB329C5") });
                listMenu.Add(new Menu { ParenId = new Guid("C09E6504B3DBC74180EB85C76EB444C4"), Id = new Guid("E17A625E0334C04D928300BBF8C80001"), Code = $"{Permissions.GuiDuyetBieuGiaCapNgam}", Name = "Gửi duyệt biểu giá", Url = "/gui-duyet-bieu-gia-cap-ngam", Order = 2, ModuleId = new Guid("C09E6504B3DBC74180EB85C76EB329C5") });
                listMenu.Add(new Menu { ParenId = new Guid("C09E6504B3DBC74180EB85C76EB444C4"), Id = new Guid("E17A625E0334C04D928300BBF8C80002"), Code = $"{Permissions.XacNhanBieuGiaCapNgam}", Name = "Xác nhận biểu giá", Url = "/xac-nhan-bieu-gia-cap-ngam", Order = 3, ModuleId = new Guid("C09E6504B3DBC74180EB85C76EB329C5") });


                context.Menu.AddRange(listMenu);
                await context.SaveChangesAsync();
            }

            if (!context.Permission.Any())
            {
                List<Permission> permissions = new List<Permission>();

                // Quản lý người dùng
                permissions.Add(new Permission { Name = "Xem", Code = $"{Permissions.User}{Permissions.View}", MenuId = new Guid("1E0A058FD1323C46ABE8061A890DC9EC") });
                permissions.Add(new Permission { Name = "Tạo", Code = $"{Permissions.User}{Permissions.Create}", MenuId = new Guid("1E0A058FD1323C46ABE8061A890DC9EC") });
                permissions.Add(new Permission { Name = "Sửa", Code = $"{Permissions.User}{Permissions.Update}", MenuId = new Guid("1E0A058FD1323C46ABE8061A890DC9EC") });
                permissions.Add(new Permission { Name = "Xóa", Code = $"{Permissions.User}{Permissions.Delete}", MenuId = new Guid("1E0A058FD1323C46ABE8061A890DC9EC") });

                //quản lý menu
                permissions.Add(new Permission { Name = "Xem", Code = $"{Permissions.Menu}{Permissions.View}", MenuId = new Guid("E17A625E0334C04D928300BBF8C83829") });
                permissions.Add(new Permission { Name = "Tạo", Code = $"{Permissions.Menu}{Permissions.Create}", MenuId = new Guid("E17A625E0334C04D928300BBF8C83829") });
                permissions.Add(new Permission { Name = "Sửa", Code = $"{Permissions.Menu}{Permissions.Update}", MenuId = new Guid("E17A625E0334C04D928300BBF8C83829") });
                permissions.Add(new Permission { Name = "Xóa", Code = $"{Permissions.Menu}{Permissions.Delete}", MenuId = new Guid("E17A625E0334C04D928300BBF8C83829") });


                // Quản lý quyền                 
                permissions.Add(new Permission { Name = "Xem", Code = $"{Permissions.Role}{Permissions.View}", MenuId = new Guid("E17A625E0334C04D928300BBF8C83828") });
                permissions.Add(new Permission { Name = "Tạo", Code = $"{Permissions.Role}{Permissions.Create}", MenuId = new Guid("E17A625E0334C04D928300BBF8C83828") });
                permissions.Add(new Permission { Name = "Sửa", Code = $"{Permissions.Role}{Permissions.Update}", MenuId = new Guid("E17A625E0334C04D928300BBF8C83828") });
                permissions.Add(new Permission { Name = "Xóa", Code = $"{Permissions.Role}{Permissions.Delete}", MenuId = new Guid("E17A625E0334C04D928300BBF8C83828") });


                #region QUẢN TRỊ DANH MỤC
                // Quản lý loại biểu giá
                permissions.Add(new Permission { Name = "Xem", Code = $"{Permissions.LoaiBieuGia}{Permissions.View}", MenuId = new Guid("E17A625E0334C04D928300BBF8C83830") });
                permissions.Add(new Permission { Name = "Tạo", Code = $"{Permissions.LoaiBieuGia}{Permissions.Create}", MenuId = new Guid("E17A625E0334C04D928300BBF8C83830") });
                permissions.Add(new Permission { Name = "Sửa", Code = $"{Permissions.LoaiBieuGia}{Permissions.Update}", MenuId = new Guid("E17A625E0334C04D928300BBF8C83830") });
                permissions.Add(new Permission { Name = "Xóa", Code = $"{Permissions.LoaiBieuGia}{Permissions.Delete}", MenuId = new Guid("E17A625E0334C04D928300BBF8C83830") });


                // Quản lý biểu giá
                permissions.Add(new Permission { Name = "Xem", Code = $"{Permissions.BieuGia}{Permissions.View}", MenuId = new Guid("E17A625E0334C04D928300BBF8C83831") });
                permissions.Add(new Permission { Name = "Tạo", Code = $"{Permissions.BieuGia}{Permissions.Create}", MenuId = new Guid("E17A625E0334C04D928300BBF8C83831") });
                permissions.Add(new Permission { Name = "Sửa", Code = $"{Permissions.BieuGia}{Permissions.Update}", MenuId = new Guid("E17A625E0334C04D928300BBF8C83831") });
                permissions.Add(new Permission { Name = "Xóa", Code = $"{Permissions.BieuGia}{Permissions.Delete}", MenuId = new Guid("E17A625E0334C04D928300BBF8C83831") });

                // Quản lý công việc
                permissions.Add(new Permission { Name = "Xem", Code = $"{Permissions.CongViec}{Permissions.View}", MenuId = new Guid("E17A625E0334C04D928300BBF8C83832") });
                permissions.Add(new Permission { Name = "Tạo", Code = $"{Permissions.CongViec}{Permissions.Create}", MenuId = new Guid("E17A625E0334C04D928300BBF8C83832") });
                permissions.Add(new Permission { Name = "Sửa", Code = $"{Permissions.CongViec}{Permissions.Update}", MenuId = new Guid("E17A625E0334C04D928300BBF8C83832") });
                permissions.Add(new Permission { Name = "Xóa", Code = $"{Permissions.CongViec}{Permissions.Delete}", MenuId = new Guid("E17A625E0334C04D928300BBF8C83832") });

                // Quản lý vùng/ khu vực
                permissions.Add(new Permission { Name = "Xem", Code = $"{Permissions.KhuVuc}{Permissions.View}", MenuId = new Guid("E17A625E0334C04D928300BBF8C83834") });
                permissions.Add(new Permission { Name = "Tạo", Code = $"{Permissions.KhuVuc}{Permissions.Create}", MenuId = new Guid("E17A625E0334C04D928300BBF8C83834") });
                permissions.Add(new Permission { Name = "Sửa", Code = $"{Permissions.KhuVuc}{Permissions.Update}", MenuId = new Guid("E17A625E0334C04D928300BBF8C83834") });
                permissions.Add(new Permission { Name = "Xóa", Code = $"{Permissions.KhuVuc}{Permissions.Delete}", MenuId = new Guid("E17A625E0334C04D928300BBF8C83834") });

                // Quản lý loại cáp
                permissions.Add(new Permission { Name = "Xem", Code = $"{Permissions.LoaiCap}{Permissions.View}", MenuId = new Guid("E17A625E0334C04D928300BBF8C83835") });
                permissions.Add(new Permission { Name = "Tạo", Code = $"{Permissions.LoaiCap}{Permissions.Create}", MenuId = new Guid("E17A625E0334C04D928300BBF8C83835") });
                permissions.Add(new Permission { Name = "Sửa", Code = $"{Permissions.LoaiCap}{Permissions.Update}", MenuId = new Guid("E17A625E0334C04D928300BBF8C83835") });
                permissions.Add(new Permission { Name = "Xóa", Code = $"{Permissions.LoaiCap}{Permissions.Delete}", MenuId = new Guid("E17A625E0334C04D928300BBF8C83835") });

                // Quản lý vật liệu
                permissions.Add(new Permission { Name = "Xem", Code = $"{Permissions.VatLieu}{Permissions.View}", MenuId = new Guid("E17A625E0334C04D928300BBF8C83836") });
                permissions.Add(new Permission { Name = "Tạo", Code = $"{Permissions.VatLieu}{Permissions.Create}", MenuId = new Guid("E17A625E0334C04D928300BBF8C83836") });
                permissions.Add(new Permission { Name = "Sửa", Code = $"{Permissions.VatLieu}{Permissions.Update}", MenuId = new Guid("E17A625E0334C04D928300BBF8C83836") });
                permissions.Add(new Permission { Name = "Xóa", Code = $"{Permissions.VatLieu}{Permissions.Delete}", MenuId = new Guid("E17A625E0334C04D928300BBF8C83836") });

                // Quản lý vật liệu chiết tinh
                permissions.Add(new Permission { Name = "Xem", Code = $"{Permissions.VatLieuChietTinh}{Permissions.View}", MenuId = new Guid("E17A625E0334C04D928300BBF8C83837") });
                permissions.Add(new Permission { Name = "Tạo", Code = $"{Permissions.VatLieuChietTinh}{Permissions.Create}", MenuId = new Guid("E17A625E0334C04D928300BBF8C83837") });
                permissions.Add(new Permission { Name = "Sửa", Code = $"{Permissions.VatLieuChietTinh}{Permissions.Update}", MenuId = new Guid("E17A625E0334C04D928300BBF8C83837") });
                permissions.Add(new Permission { Name = "Xóa", Code = $"{Permissions.VatLieuChietTinh}{Permissions.Delete}", MenuId = new Guid("E17A625E0334C04D928300BBF8C83837") });

                // Biểu giá công việc
                permissions.Add(new Permission { Name = "Xem", Code = $"{Permissions.BieuGiaCongViec}{Permissions.View}", MenuId = new Guid("E17A625E0334C04D928300BBF8C83843") });
                permissions.Add(new Permission { Name = "Tạo", Code = $"{Permissions.BieuGiaCongViec}{Permissions.Create}", MenuId = new Guid("E17A625E0334C04D928300BBF8C83843") });
                permissions.Add(new Permission { Name = "Sửa", Code = $"{Permissions.BieuGiaCongViec}{Permissions.Update}", MenuId = new Guid("E17A625E0334C04D928300BBF8C83843") });
                permissions.Add(new Permission { Name = "Xóa", Code = $"{Permissions.BieuGiaCongViec}{Permissions.Delete}", MenuId = new Guid("E17A625E0334C04D928300BBF8C83843") });
                #endregion


                #region Quản lý thông tin đơn giá
                // Đơn giá cáp
                permissions.Add(new Permission { Name = "Xem", Code = $"{Permissions.DonGiaCap}{Permissions.View}", MenuId = new Guid("E17A625E0334C04D928300BBF8C83838") });
                permissions.Add(new Permission { Name = "Tạo", Code = $"{Permissions.DonGiaCap}{Permissions.Create}", MenuId = new Guid("E17A625E0334C04D928300BBF8C83838") });
                permissions.Add(new Permission { Name = "Sửa", Code = $"{Permissions.DonGiaCap}{Permissions.Update}", MenuId = new Guid("E17A625E0334C04D928300BBF8C83838") });
                permissions.Add(new Permission { Name = "Xóa", Code = $"{Permissions.DonGiaCap}{Permissions.Delete}", MenuId = new Guid("E17A625E0334C04D928300BBF8C83838") });

                // Đơn giá nhân công
                permissions.Add(new Permission { Name = "Xem", Code = $"{Permissions.DonGiaNhanCong}{Permissions.View}", MenuId = new Guid("E17A625E0334C04D928300BBF8C83839") });
                permissions.Add(new Permission { Name = "Tạo", Code = $"{Permissions.DonGiaNhanCong}{Permissions.Create}", MenuId = new Guid("E17A625E0334C04D928300BBF8C83839") });
                permissions.Add(new Permission { Name = "Sửa", Code = $"{Permissions.DonGiaNhanCong}{Permissions.Update}", MenuId = new Guid("E17A625E0334C04D928300BBF8C83839") });
                permissions.Add(new Permission { Name = "Xóa", Code = $"{Permissions.DonGiaNhanCong}{Permissions.Delete}", MenuId = new Guid("E17A625E0334C04D928300BBF8C83839") });


                // Đơn giá vật liệu
                permissions.Add(new Permission { Name = "Xem", Code = $"{Permissions.DonGiaVatLieu}{Permissions.View}", MenuId = new Guid("E17A625E0334C04D928300BBF8C83840") });
                permissions.Add(new Permission { Name = "Tạo", Code = $"{Permissions.DonGiaVatLieu}{Permissions.Create}", MenuId = new Guid("E17A625E0334C04D928300BBF8C83840") });
                permissions.Add(new Permission { Name = "Sửa", Code = $"{Permissions.DonGiaVatLieu}{Permissions.Update}", MenuId = new Guid("E17A625E0334C04D928300BBF8C83840") });
                permissions.Add(new Permission { Name = "Xóa", Code = $"{Permissions.DonGiaVatLieu}{Permissions.Delete}", MenuId = new Guid("E17A625E0334C04D928300BBF8C83840") });

                // Đơn giá vật liệu chiết tinh
                permissions.Add(new Permission { Name = "Xem", Code = $"{Permissions.DonGiaVatLieuChietTinh}{Permissions.View}", MenuId = new Guid("E17A625E0334C04D928300BBF8C83841") });
                permissions.Add(new Permission { Name = "Tạo", Code = $"{Permissions.DonGiaVatLieuChietTinh}{Permissions.Create}", MenuId = new Guid("E17A625E0334C04D928300BBF8C83841") });
                permissions.Add(new Permission { Name = "Sửa", Code = $"{Permissions.DonGiaVatLieuChietTinh}{Permissions.Update}", MenuId = new Guid("E17A625E0334C04D928300BBF8C83841") });
                permissions.Add(new Permission { Name = "Xóa", Code = $"{Permissions.DonGiaVatLieuChietTinh}{Permissions.Delete}", MenuId = new Guid("E17A625E0334C04D928300BBF8C83841") });

                #endregion

                #region QUẢN TRỊ CHI TIẾT BIỂU GIÁ
                // Chi tiết biểu giá
                permissions.Add(new Permission { Name = "Xem", Code = $"{Permissions.ChiTietBieuGia}{Permissions.View}", MenuId = new Guid("E17A625E0334C04D928300BBF8C83842") });
                permissions.Add(new Permission { Name = "Sửa", Code = $"{Permissions.ChiTietBieuGia}{Permissions.Update}", MenuId = new Guid("E17A625E0334C04D928300BBF8C83842") });

                // Xác nhận biểu giá
                //permissions.Add(new Permission { Name = "Gửi duyệt", Code = $"{Permissions.XacNhanBieuGia}{Permissions.Send}", MenuId = new Guid("E17A625E0334C04D928300BBF8C83844") });
                //permissions.Add(new Permission { Name = "Duyệt", Code = $"{Permissions.XacNhanBieuGia}{Permissions.Approve}", MenuId = new Guid("E17A625E0334C04D928300BBF8C83844") });

                #endregion
                context.Permission.AddRange(permissions);

            }

            if (!context.Position.Any())
            {
                List<Position> positions = new List<Position>();
                positions.Add(new Position { Id = new Guid("A803B9302260C647960DC65AB20AD553"), Title = "Quản trị viên", Value = PositionEnum.Administrator.GetHashCode(), CreatedDate = DateTime.Now });
                positions.Add(new Position { Id = new Guid("63B7B10A719245409815FC3296F0341F"), Title = "Tổng giám đốc", Value = PositionEnum.TongGiamDoc.GetHashCode(), CreatedDate = DateTime.Now });
                positions.Add(new Position { Id = new Guid("C2561F284076114CB824B39D1F366F2B"), Title = "Phó tổng giám đốc", Value = PositionEnum.PhoTongGiamDoc.GetHashCode(), CreatedDate = DateTime.Now });
                positions.Add(new Position { Id = new Guid("E17B5211E053D840A7C6E12C312DA8B1"), Title = "Trưởng ban kỹ thuật", Value = PositionEnum.TruongBanKyThuat.GetHashCode(), CreatedDate = DateTime.Now });
                positions.Add(new Position { Id = new Guid("35EEEB98CF39BC47A0A2B1E93D5F0E20"), Title = "Phó trưởng ban kỹ thuật", Value = PositionEnum.PhoTruongBanKyThuat.GetHashCode(), CreatedDate = DateTime.Now });
                positions.Add(new Position { Id = new Guid("D16AD90DB6F27E48A73F59194F55F444"), Title = "Chuyên viên", Value = PositionEnum.ChuyenVien.GetHashCode(), CreatedDate = DateTime.Now });
                positions.Add(new Position { Id = new Guid("FA95767484EA1741B8BC84B4444F2CB0"), Title = "Giám đốc", Value = PositionEnum.GiamDoc.GetHashCode(), CreatedDate = DateTime.Now });
                positions.Add(new Position { Id = new Guid("F391BE860CA2AB4E869FC700C63CC5B6"), Title = "Phó giám đốc", Value = PositionEnum.PhoGiamDoc.GetHashCode(), CreatedDate = DateTime.Now });
                positions.Add(new Position { Id = new Guid("3A3DC165F1505B4DAE5D2B02873CD184"), Title = "Trưởng phòng kỹ thuật", Value = PositionEnum.TruongPhongKyThuat.GetHashCode(), CreatedDate = DateTime.Now });
                positions.Add(new Position { Id = new Guid("E2342C808E83E7419335E4BF0CF84B53"), Title = "Phó phòng kỹ thuật", Value = PositionEnum.PhoPhongKyThuat.GetHashCode(), CreatedDate = DateTime.Now });
                positions.Add(new Position { Id = new Guid("65CC60AEC4FF37428E4D92F15C621C50"), Title = "Đội trưởng", Value = PositionEnum.DoiTruong.GetHashCode(), CreatedDate = DateTime.Now });
                positions.Add(new Position { Id = new Guid("7414F6643B0405418DE28A140157B3C7"), Title = "Đội phó", Value = PositionEnum.DoiPho.GetHashCode(), CreatedDate = DateTime.Now });
                positions.Add(new Position { Id = new Guid("E9CA3E167CD7A44D9BEC9E8B91258FFE"), Title = "Nhân viên", Value = PositionEnum.NhanVien.GetHashCode(), CreatedDate = DateTime.Now });
                positions.Add(new Position { Id = new Guid("919FDA58162FA44BA242A94503351EF0"), Title = "Công nhân", Value = PositionEnum.CongNhan.GetHashCode(), CreatedDate = DateTime.Now });
                context.Position.AddRange(positions);
            }

            if (!context.DM_KhuVuc.Any())
            {
                var listVungKhuVuc = new List<DM_KhuVuc>();

                listVungKhuVuc.Add(new DM_KhuVuc { Id = new Guid("C09E6504B3DBC74180EB85C76EB329C1"), TenKhuVuc = "Vùng I - Khu Vực 1" });
                listVungKhuVuc.Add(new DM_KhuVuc { Id = new Guid("C09E6504B3DBC74180EB85C76EB329C2"), TenKhuVuc = "Vùng I - Khu Vực 2" });
                listVungKhuVuc.Add(new DM_KhuVuc { Id = new Guid("C09E6504B3DBC74180EB85C76EB329C3"), TenKhuVuc = "Vùng II" });

                context.DM_KhuVuc.AddRange(listVungKhuVuc);
                await context.SaveChangesAsync();
            }

            if (!context.DM_LoaiBieuGia.Any())
            {
                var listLoaiBieuGia = new List<DM_LoaiBieuGia>();

                listLoaiBieuGia.Add(new DM_LoaiBieuGia { IdKhuVuc = new Guid("C09E6504B3DBC74180EB85C76EB329C1"), TenLoaiBieuGia = "Đơn giá lắp đặt dây sau công tơ" });
                listLoaiBieuGia.Add(new DM_LoaiBieuGia { IdKhuVuc = new Guid("C09E6504B3DBC74180EB85C76EB329C1"), TenLoaiBieuGia = "Đơn giá thay dây dẫn điện sau công tơ" });
                listLoaiBieuGia.Add(new DM_LoaiBieuGia { IdKhuVuc = new Guid("C09E6504B3DBC74180EB85C76EB329C1"), TenLoaiBieuGia = "Đơn giá thay đổi công suất SDĐ/Thay đổi loại công tơ" });
                listLoaiBieuGia.Add(new DM_LoaiBieuGia { IdKhuVuc = new Guid("C09E6504B3DBC74180EB85C76EB329C1"), TenLoaiBieuGia = "Đơn giá thay đổi vị trí thiết bị đo đếm" });
                listLoaiBieuGia.Add(new DM_LoaiBieuGia { IdKhuVuc = new Guid("C09E6504B3DBC74180EB85C76EB329C2"), TenLoaiBieuGia = "Đơn giá lắp đặt dây sau công tơ" });
                listLoaiBieuGia.Add(new DM_LoaiBieuGia { IdKhuVuc = new Guid("C09E6504B3DBC74180EB85C76EB329C2"), TenLoaiBieuGia = "Đơn giá thay dây dẫn điện sau công tơ" });
                listLoaiBieuGia.Add(new DM_LoaiBieuGia { IdKhuVuc = new Guid("C09E6504B3DBC74180EB85C76EB329C2"), TenLoaiBieuGia = "Đơn giá thay đổi công suất SDĐ/Thay đổi loại công tơ" });
                listLoaiBieuGia.Add(new DM_LoaiBieuGia { IdKhuVuc = new Guid("C09E6504B3DBC74180EB85C76EB329C2"), TenLoaiBieuGia = "Đơn giá thay đổi vị trí thiết bị đo đếm" });
                listLoaiBieuGia.Add(new DM_LoaiBieuGia { IdKhuVuc = new Guid("C09E6504B3DBC74180EB85C76EB329C3"), TenLoaiBieuGia = "Đơn giá lắp đặt dây sau công tơ" });
                listLoaiBieuGia.Add(new DM_LoaiBieuGia { IdKhuVuc = new Guid("C09E6504B3DBC74180EB85C76EB329C3"), TenLoaiBieuGia = "Đơn giá thay dây dẫn điện sau công tơ" });
                listLoaiBieuGia.Add(new DM_LoaiBieuGia { IdKhuVuc = new Guid("C09E6504B3DBC74180EB85C76EB329C3"), TenLoaiBieuGia = "Đơn giá thay đổi công suất SDĐ/Thay đổi loại công tơ" });
                listLoaiBieuGia.Add(new DM_LoaiBieuGia { IdKhuVuc = new Guid("C09E6504B3DBC74180EB85C76EB329C3"), TenLoaiBieuGia = "Đơn giá thay đổi vị trí thiết bị đo đếm" });

                await context.DM_LoaiBieuGia.AddRangeAsync(listLoaiBieuGia);
                await context.SaveChangesAsync();
            }

            await context.SaveChangesAsync();
        }
    }
}
