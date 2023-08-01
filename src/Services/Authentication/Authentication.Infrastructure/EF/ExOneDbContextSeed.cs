using Authentication.Infrastructure.AggregatesModel.DM_KhuVucAggregate;
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
                listMenu.Add(new Menu { Id = new Guid("E22A625E0334C04D928300BBF8C83822"), Code = $"{Permissions.CauHinh}", Name = "Quản lý cấu hình", Url = "/cau-hinh", Order = 4, ModuleId = new Guid("C09E6504B3DBC74180EB85C76EB329C1") });

                // QUẢN TRỊ DANH MỤC

                listMenu.Add(new Menu { Id = new Guid("E17A625E0334C04D928300BBF8C83111"), Code = $"{Permissions.CapTrenKhong}", Name = "Cáp trên không", Url = null, Order = 0, ModuleId = new Guid("C09E6504B3DBC74180EB85C76EB329C2") });
                listMenu.Add(new Menu { Id = new Guid("E17A625E0334C04D928300BBF8C83222"), Code = $"{Permissions.CapNgam}", Name = "Cáp ngầm", Url = null, Order = 0, ModuleId = new Guid("C09E6504B3DBC74180EB85C76EB329C2") });

                listMenu.Add(new Menu { ParenId = new Guid("E17A625E0334C04D928300BBF8C83111"), Id = new Guid("E17A625E0334C04D928300BBF8C83830"), Code = $"{Permissions.LoaiBieuGia}", Name = "Quản lý loại biểu giá", Url = "/loai-bieu-gia", Order = 1, ModuleId = new Guid("C09E6504B3DBC74180EB85C76EB329C2") });
                listMenu.Add(new Menu { ParenId = new Guid("E17A625E0334C04D928300BBF8C83111"), Id = new Guid("E17A625E0334C04D928300BBF8C83832"), Code = $"{Permissions.CongViec}", Name = "Quản lý công việc", Url = "/cong-viec", Order = 3, ModuleId = new Guid("C09E6504B3DBC74180EB85C76EB329C2") });
                listMenu.Add(new Menu { ParenId = new Guid("E17A625E0334C04D928300BBF8C83111"), Id = new Guid("E17A625E0334C04D928300BBF8C83831"), Code = $"{Permissions.BieuGia}", Name = "Quản lý biểu giá", Url = "/bieu-gia", Order = 2, ModuleId = new Guid("C09E6504B3DBC74180EB85C76EB329C2") });

                listMenu.Add(new Menu { ParenId = new Guid("E17A625E0334C04D928300BBF8C83111"), Id = new Guid("E17A625E0334C04D928300BBF8C83835"), Code = $"{Permissions.LoaiCap}", Name = "Quản lý loại cáp", Url = "/loai-cap", Order = 6, ModuleId = new Guid("C09E6504B3DBC74180EB85C76EB329C2") });
                listMenu.Add(new Menu { ParenId = new Guid("E17A625E0334C04D928300BBF8C83111"), Id = new Guid("E17A625E0334C04D928300BBF8C83836"), Code = $"{Permissions.VatLieu}", Name = "Quản lý vật liệu", Url = "/vat-lieu", Order = 7, ModuleId = new Guid("C09E6504B3DBC74180EB85C76EB329C2") });
                listMenu.Add(new Menu { ParenId = new Guid("E17A625E0334C04D928300BBF8C83111"), Id = new Guid("E17A625E0334C04D928300BBF8C81116"), Code = $"{Permissions.NhanCong}", Name = "Quản lý nhân công", Url = "/nhan-cong", Order = 8, ModuleId = new Guid("C09E6504B3DBC74180EB85C76EB329C2") });
                listMenu.Add(new Menu { ParenId = new Guid("E17A625E0334C04D928300BBF8C83111"), Id = new Guid("E17A625E0334C04D928300BBF8C10001"), Code = $"{Permissions.MTC}", Name = "Quản lý máy thi công", Url = "/may-thi-cong", Order = 9, ModuleId = new Guid("C09E6504B3DBC74180EB85C76EB329C2") });
                //listMenu.Add(new Menu { ParenId = new Guid("E17A625E0334C04D928300BBF8C83111"), Id = new Guid("E17A625E0334C04D928300BBF8C83837"), Code = $"{Permissions.VatLieuChietTinh}", Name = "Quản lý vật liệu chiết tinh", Url = "/vat-lieu-chiet-tinh", Order = 8, ModuleId = new Guid("C09E6504B3DBC74180EB85C76EB329C2") });
                listMenu.Add(new Menu { ParenId = new Guid("E17A625E0334C04D928300BBF8C83111"), Id = new Guid("E17A625E0334C04D928300BBF8C83843"), Code = $"{Permissions.BieuGiaCongViec}", Name = "Biểu giá công việc", Url = "/bieu-gia-cong-viec", Order = 4, ModuleId = new Guid("C09E6504B3DBC74180EB85C76EB329C2") });
                // cáp trên không
                listMenu.Add(new Menu { ParenId = new Guid("E17A625E0334C04D928300BBF8C83222"), Id = new Guid("E17A625E0334C04D928300BBF8C88000"), Code = $"{Permissions.LoaiBieuGiaCapNgam}", Name = "Quản lý loại biểu giá", Url = "/loai-bieu-gia-cap-ngam", Order = 1, ModuleId = new Guid("C09E6504B3DBC74180EB85C76EB329C2") });
                listMenu.Add(new Menu { ParenId = new Guid("E17A625E0334C04D928300BBF8C83222"), Id = new Guid("E17A625E0334C04D928300BBF8C88001"), Code = $"{Permissions.BieuGiaCapNgam}", Name = "Quản lý biểu giá", Url = "/bieu-gia-cap-ngam", Order = 2, ModuleId = new Guid("C09E6504B3DBC74180EB85C76EB329C2") });
                listMenu.Add(new Menu { ParenId = new Guid("E17A625E0334C04D928300BBF8C83222"), Id = new Guid("E17A625E0334C04D928300BBF8C88002"), Code = $"{Permissions.CongViecCapNgam}", Name = "Quản lý công việc", Url = "/cong-viec-cap-ngam", Order = 3, ModuleId = new Guid("C09E6504B3DBC74180EB85C76EB329C2") });
                listMenu.Add(new Menu { ParenId = new Guid("E17A625E0334C04D928300BBF8C83222"), Id = new Guid("E17A625E0334C04D928300BBF8C88003"), Code = $"{Permissions.BieuGiaCongViecCapNgam}", Name = "Biểu giá công việc", Url = "/bieu-gia-cong-viec-cap-ngam", Order = 4, ModuleId = new Guid("C09E6504B3DBC74180EB85C76EB329C2") });
                listMenu.Add(new Menu { ParenId = new Guid("E17A625E0334C04D928300BBF8C83222"), Id = new Guid("E17A625E0334C04D928300BBF8C88666"), Code = $"{Permissions.LoaiCapCapNgam}", Name = "Quản lý loại cáp", Url = "/loai-cap-cap-ngam", Order = 6, ModuleId = new Guid("C09E6504B3DBC74180EB85C76EB329C2") });
                listMenu.Add(new Menu { ParenId = new Guid("E17A625E0334C04D928300BBF8C83222"), Id = new Guid("E17A625E0334C04D928300BBF8C88006"), Code = $"{Permissions.VatLieuCapNgam}", Name = "Quản lý vật liệu", Url = "/vat-lieu-cap-ngam", Order = 7, ModuleId = new Guid("C09E6504B3DBC74180EB85C76EB329C2") });
                listMenu.Add(new Menu { ParenId = new Guid("E17A625E0334C04D928300BBF8C83222"), Id = new Guid("E17A625E0334C04D928300BBF8C81117"), Code = $"{Permissions.NhanCongCapNgam}", Name = "Quản lý nhân công", Url = "/nhan-cong-cap-ngam", Order = 8, ModuleId = new Guid("C09E6504B3DBC74180EB85C76EB329C2") });
                listMenu.Add(new Menu { ParenId = new Guid("E17A625E0334C04D928300BBF8C83222"), Id = new Guid("E17A625E0334C04D928300BBF8C10002"), Code = $"{Permissions.MTCCapNgam}", Name = "Quản lý máy thi công", Url = "/may-thi-cong-cap-ngam", Order = 9, ModuleId = new Guid("C09E6504B3DBC74180EB85C76EB329C2") });


                //listMenu.Add(new Menu { Id = new Guid("E17A625E0334C04D928300BBF8C83834"), Code = $"{Permissions.KhuVuc}", Name = "Quản lý vùng/khu vực", Url = "/khu-vuc", Order = 5, ModuleId = new Guid("C09E6504B3DBC74180EB85C76EB329C2") });


                // QUẢN TRỊ ĐƠN GIÁ

                listMenu.Add(new Menu { Id = new Guid("C09E6504B3DBC74180EB85C76EB111C4"), Code = $"{Permissions.DGCapTrenKhong}", Name = "Cáp trên không", Url = null, Order = 0, ModuleId = new Guid("C09E6504B3DBC74180EB85C76EB329C4") });
                listMenu.Add(new Menu { Id = new Guid("C09E6504B3DBC74180EB85C76EB222C4"), Code = $"{Permissions.DGCapNgam}", Name = "Cáp ngầm", Url = null, Order = 0, ModuleId = new Guid("C09E6504B3DBC74180EB85C76EB329C4") });

                listMenu.Add(new Menu { ParenId = new Guid("C09E6504B3DBC74180EB85C76EB111C4"), Id = new Guid("E17A625E0334C04D928300BBF8C83838"), Code = $"{Permissions.DonGiaCap}", Name = "Đơn giá cáp", Url = "/don-gia-cap", Order = 1, ModuleId = new Guid("C09E6504B3DBC74180EB85C76EB329C4") });
                listMenu.Add(new Menu { ParenId = new Guid("C09E6504B3DBC74180EB85C76EB111C4"), Id = new Guid("E17A625E0334C04D928300BBF8C83839"), Code = $"{Permissions.DonGiaNhanCong}", Name = "Đơn giá nhân công", Url = "/don-gia-nhan-cong", Order = 2, ModuleId = new Guid("C09E6504B3DBC74180EB85C76EB329C4") });
                listMenu.Add(new Menu { ParenId = new Guid("C09E6504B3DBC74180EB85C76EB111C4"), Id = new Guid("E17A625E0334C04D928300BBF8C83840"), Code = $"{Permissions.DonGiaVatLieu}", Name = "Đơn giá vật liệu", Url = "/don-gia-vat-lieu", Order = 3, ModuleId = new Guid("C09E6504B3DBC74180EB85C76EB329C4") });
                listMenu.Add(new Menu { ParenId = new Guid("C09E6504B3DBC74180EB85C76EB111C4"), Id = new Guid("E17A625E0334C04D928300BBF8C10003"), Code = $"{Permissions.DonGiaMTC}", Name = "Đơn giá máy thi công", Url = "/don-gia-may-thi-cong", Order = 3, ModuleId = new Guid("C09E6504B3DBC74180EB85C76EB329C4") });
                listMenu.Add(new Menu { ParenId = new Guid("C09E6504B3DBC74180EB85C76EB111C4"), Id = new Guid("E17A625E0334C04D928300BBF8C83841"), Code = $"{Permissions.DonGiaVatLieuChietTinh}", Name = "Đơn giá vật liệu chiết tính", Url = "/don-gia-vat-lieu-chiet-tinh", Order = 4, ModuleId = new Guid("C09E6504B3DBC74180EB85C76EB329C4") });
                listMenu.Add(new Menu { ParenId = new Guid("C09E6504B3DBC74180EB85C76EB111C4"), Id = new Guid("E17A625E0334C04D928300BBF8C88100"), Code = $"{Permissions.CauHinhChietTinh}", Name = "Cấu hình chiết tính", Url = "/cau-hinh-chiet-tinh", Order = 5, ModuleId = new Guid("C09E6504B3DBC74180EB85C76EB329C4") });
                // cáp ngầm
                listMenu.Add(new Menu { ParenId = new Guid("C09E6504B3DBC74180EB85C76EB222C4"), Id = new Guid("E17A625E0334C04D928300BBF8C83888"), Code = $"{Permissions.DonGiaCapCapNgam}", Name = "Đơn giá cáp", Url = "/don-gia-cap-cap-ngam", Order = 1, ModuleId = new Guid("C09E6504B3DBC74180EB85C76EB329C4") });
                listMenu.Add(new Menu { ParenId = new Guid("C09E6504B3DBC74180EB85C76EB222C4"), Id = new Guid("E17A625E0334C04D928300BBF8C83890"), Code = $"{Permissions.DonGiaNhanCongCapNgam}", Name = "Đơn giá nhân công", Url = "/don-gia-nhan-cong-cap-ngam", Order = 2, ModuleId = new Guid("C09E6504B3DBC74180EB85C76EB329C4") });
                listMenu.Add(new Menu { ParenId = new Guid("C09E6504B3DBC74180EB85C76EB222C4"), Id = new Guid("E17A625E0334C04D928300BBF8C83891"), Code = $"{Permissions.DonGiaVatLieuCapNgam}", Name = "Đơn giá vật liệu", Url = "/don-gia-vat-lieu-cap-ngam", Order = 3, ModuleId = new Guid("C09E6504B3DBC74180EB85C76EB329C4") });
                listMenu.Add(new Menu { ParenId = new Guid("C09E6504B3DBC74180EB85C76EB222C4"), Id = new Guid("E17A625E0334C04D928300BBF8C10004"), Code = $"{Permissions.DonGiaMTCCapNgam}", Name = "Đơn giá máy thi công", Url = "/don-gia-may-thi-cong-cap-ngam", Order = 3, ModuleId = new Guid("C09E6504B3DBC74180EB85C76EB329C4") });
                listMenu.Add(new Menu { ParenId = new Guid("C09E6504B3DBC74180EB85C76EB222C4"), Id = new Guid("E17A625E0334C04D928300BBF8C83889"), Code = $"{Permissions.DonGiaVatLieuChietTinhCapNgam}", Name = "Đơn giá vật liệu chiết tính", Url = "/don-gia-vat-lieu-chiet-tinh-cap-ngam", Order = 4, ModuleId = new Guid("C09E6504B3DBC74180EB85C76EB329C4") });
                listMenu.Add(new Menu { ParenId = new Guid("C09E6504B3DBC74180EB85C76EB222C4"), Id = new Guid("E17A625E0334C04D928300BBF8C88101"), Code = $"{Permissions.CauHinhChietTinhCapNgam}", Name = "Cấu hình chiết tính", Url = "/cau-hinh-chiet-tinh-cap-ngam", Order = 5, ModuleId = new Guid("C09E6504B3DBC74180EB85C76EB329C4") });

                // QUẢN LÝ THÔNG TIN CHI TIẾT BIỂU GIÁ

                listMenu.Add(new Menu { Id = new Guid("C09E6504B3DBC74180EB85C76EB333C4"), Code = $"{Permissions.CTCapTrenKhong}", Name = "Cáp trên không", Url = null, Order = 0, ModuleId = new Guid("C09E6504B3DBC74180EB85C76EB329C5") });
                listMenu.Add(new Menu { Id = new Guid("C09E6504B3DBC74180EB85C76EB444C4"), Code = $"{Permissions.CTCapNgam}", Name = "Cáp ngầm", Url = null, Order = 0, ModuleId = new Guid("C09E6504B3DBC74180EB85C76EB329C5") });

                listMenu.Add(new Menu { ParenId = new Guid("C09E6504B3DBC74180EB85C76EB333C4"), Id = new Guid("E17A625E0334C04D928300BBF8C90000"), Code = $"{Permissions.DongBoBieuGia}", Name = "Đồng bộ biểu giá", Url = "/dong-bo-bieu-gia", Order = 1, ModuleId = new Guid("C09E6504B3DBC74180EB85C76EB329C5") });
                listMenu.Add(new Menu { ParenId = new Guid("C09E6504B3DBC74180EB85C76EB333C4"), Id = new Guid("E17A625E0334C04D928300BBF8C83842"), Code = $"{Permissions.ChiTietBieuGia}", Name = "Chi tiết biểu giá", Url = "/chi-tiet-bieu-gia", Order = 2, ModuleId = new Guid("C09E6504B3DBC74180EB85C76EB329C5") });
                listMenu.Add(new Menu { ParenId = new Guid("C09E6504B3DBC74180EB85C76EB333C4"), Id = new Guid("E17A625E0334C04D928300BBF8C83845"), Code = $"{Permissions.GuiDuyetBieuGia}", Name = "Gửi duyệt biểu giá", Url = "/gui-duyet-bieu-gia", Order = 3, ModuleId = new Guid("C09E6504B3DBC74180EB85C76EB329C5") });
                listMenu.Add(new Menu { ParenId = new Guid("C09E6504B3DBC74180EB85C76EB333C4"), Id = new Guid("E17A625E0334C04D928300BBF8C83846"), Code = $"{Permissions.XacNhanBieuGia}", Name = "Xác nhận biểu giá", Url = "/xac-nhan-bieu-gia", Order = 4, ModuleId = new Guid("C09E6504B3DBC74180EB85C76EB329C5") });

                listMenu.Add(new Menu { ParenId = new Guid("C09E6504B3DBC74180EB85C76EB444C4"), Id = new Guid("E17A625E0334C04D928300BBF8C90001"), Code = $"{Permissions.DongBoBieuGiaCapNgam}", Name = "Đồng bộ biểu giá", Url = "/dong-bo-bieu-gia-cap-ngam", Order = 1, ModuleId = new Guid("C09E6504B3DBC74180EB85C76EB329C5") });
                listMenu.Add(new Menu { ParenId = new Guid("C09E6504B3DBC74180EB85C76EB444C4"), Id = new Guid("E17A625E0334C04D928300BBF8C80000"), Code = $"{Permissions.ChiTietBieuGiaCapNgam}", Name = "Chi tiết biểu giá", Url = "/chi-tiet-bieu-gia-cap-ngam", Order = 2, ModuleId = new Guid("C09E6504B3DBC74180EB85C76EB329C5") });
                listMenu.Add(new Menu { ParenId = new Guid("C09E6504B3DBC74180EB85C76EB444C4"), Id = new Guid("E17A625E0334C04D928300BBF8C80001"), Code = $"{Permissions.GuiDuyetBieuGiaCapNgam}", Name = "Gửi duyệt biểu giá", Url = "/gui-duyet-bieu-gia-cap-ngam", Order = 3, ModuleId = new Guid("C09E6504B3DBC74180EB85C76EB329C5") });
                listMenu.Add(new Menu { ParenId = new Guid("C09E6504B3DBC74180EB85C76EB444C4"), Id = new Guid("E17A625E0334C04D928300BBF8C80002"), Code = $"{Permissions.XacNhanBieuGiaCapNgam}", Name = "Xác nhận biểu giá", Url = "/xac-nhan-bieu-gia-cap-ngam", Order = 4, ModuleId = new Guid("C09E6504B3DBC74180EB85C76EB329C5") });


                listMenu.Add(new Menu { Id = new Guid("E17A625E0334C04D928300BBF8C90002"), Code = $"{Permissions.BaoCaoCapTrenKhong}", Name = "Tổng hợp đơn giá cáp trên không", Url = "/bao-cao-tong-hop-don-gia-cap-tren-khong", Order = 1, ModuleId = new Guid("C09E6504B3DBC74180EB85C76EB329C6") });
                listMenu.Add(new Menu { Id = new Guid("E17A625E0334C04D928300BBF8C90003"), Code = $"{Permissions.BaoCaoCapNgam}", Name = "Tổng hợp đơn giá cáp ngầm", Url = "/bao-cao-tong-hop-don-gia-cap-ngam", Order = 2, ModuleId = new Guid("C09E6504B3DBC74180EB85C76EB329C6") });



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
                //quản lý cấu hình
                permissions.Add(new Permission { Name = "Xem", Code = $"{Permissions.CauHinh}{Permissions.View}", MenuId = new Guid("E22A625E0334C04D928300BBF8C83822") });
                permissions.Add(new Permission { Name = "Tạo", Code = $"{Permissions.CauHinh}{Permissions.Create}", MenuId = new Guid("E22A625E0334C04D928300BBF8C83822") });
                permissions.Add(new Permission { Name = "Sửa", Code = $"{Permissions.CauHinh}{Permissions.Update}", MenuId = new Guid("E22A625E0334C04D928300BBF8C83822") });
                permissions.Add(new Permission { Name = "Xóa", Code = $"{Permissions.CauHinh}{Permissions.Delete}", MenuId = new Guid("E22A625E0334C04D928300BBF8C83822") });


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
                // Quản lý loại biểu giá cáp ngầm
                permissions.Add(new Permission { Name = "Xem", Code = $"{Permissions.LoaiBieuGiaCapNgam}{Permissions.View}", MenuId = new Guid("E17A625E0334C04D928300BBF8C88000") });
                permissions.Add(new Permission { Name = "Tạo", Code = $"{Permissions.LoaiBieuGiaCapNgam}{Permissions.Create}", MenuId = new Guid("E17A625E0334C04D928300BBF8C88000") });
                permissions.Add(new Permission { Name = "Sửa", Code = $"{Permissions.LoaiBieuGiaCapNgam}{Permissions.Update}", MenuId = new Guid("E17A625E0334C04D928300BBF8C88000") });
                permissions.Add(new Permission { Name = "Xóa", Code = $"{Permissions.LoaiBieuGiaCapNgam}{Permissions.Delete}", MenuId = new Guid("E17A625E0334C04D928300BBF8C88000") });

                // Quản lý biểu giá
                permissions.Add(new Permission { Name = "Xem", Code = $"{Permissions.BieuGia}{Permissions.View}", MenuId = new Guid("E17A625E0334C04D928300BBF8C83831") });
                permissions.Add(new Permission { Name = "Tạo", Code = $"{Permissions.BieuGia}{Permissions.Create}", MenuId = new Guid("E17A625E0334C04D928300BBF8C83831") });
                permissions.Add(new Permission { Name = "Sửa", Code = $"{Permissions.BieuGia}{Permissions.Update}", MenuId = new Guid("E17A625E0334C04D928300BBF8C83831") });
                permissions.Add(new Permission { Name = "Xóa", Code = $"{Permissions.BieuGia}{Permissions.Delete}", MenuId = new Guid("E17A625E0334C04D928300BBF8C83831") });
                // Quản lý biểu giá
                permissions.Add(new Permission { Name = "Xem", Code = $"{Permissions.BieuGiaCapNgam}{Permissions.View}", MenuId = new Guid("E17A625E0334C04D928300BBF8C88001") });
                permissions.Add(new Permission { Name = "Tạo", Code = $"{Permissions.BieuGiaCapNgam}{Permissions.Create}", MenuId = new Guid("E17A625E0334C04D928300BBF8C88001") });
                permissions.Add(new Permission { Name = "Sửa", Code = $"{Permissions.BieuGiaCapNgam}{Permissions.Update}", MenuId = new Guid("E17A625E0334C04D928300BBF8C88001") });
                permissions.Add(new Permission { Name = "Xóa", Code = $"{Permissions.BieuGiaCapNgam}{Permissions.Delete}", MenuId = new Guid("E17A625E0334C04D928300BBF8C88001") });

                // Quản lý công việc
                permissions.Add(new Permission { Name = "Xem", Code = $"{Permissions.CongViec}{Permissions.View}", MenuId = new Guid("E17A625E0334C04D928300BBF8C83832") });
                permissions.Add(new Permission { Name = "Tạo", Code = $"{Permissions.CongViec}{Permissions.Create}", MenuId = new Guid("E17A625E0334C04D928300BBF8C83832") });
                permissions.Add(new Permission { Name = "Sửa", Code = $"{Permissions.CongViec}{Permissions.Update}", MenuId = new Guid("E17A625E0334C04D928300BBF8C83832") });
                permissions.Add(new Permission { Name = "Xóa", Code = $"{Permissions.CongViec}{Permissions.Delete}", MenuId = new Guid("E17A625E0334C04D928300BBF8C83832") });
                // Quản lý công việc cáp ngầm
                permissions.Add(new Permission { Name = "Xem", Code = $"{Permissions.CongViecCapNgam}{Permissions.View}", MenuId = new Guid("E17A625E0334C04D928300BBF8C88002") });
                permissions.Add(new Permission { Name = "Tạo", Code = $"{Permissions.CongViecCapNgam}{Permissions.Create}", MenuId = new Guid("E17A625E0334C04D928300BBF8C88002") });
                permissions.Add(new Permission { Name = "Sửa", Code = $"{Permissions.CongViecCapNgam}{Permissions.Update}", MenuId = new Guid("E17A625E0334C04D928300BBF8C88002") });
                permissions.Add(new Permission { Name = "Xóa", Code = $"{Permissions.CongViecCapNgam}{Permissions.Delete}", MenuId = new Guid("E17A625E0334C04D928300BBF8C88002") });

                // Quản lý loại cáp
                permissions.Add(new Permission { Name = "Xem", Code = $"{Permissions.LoaiCap}{Permissions.View}", MenuId = new Guid("E17A625E0334C04D928300BBF8C83835") });
                permissions.Add(new Permission { Name = "Tạo", Code = $"{Permissions.LoaiCap}{Permissions.Create}", MenuId = new Guid("E17A625E0334C04D928300BBF8C83835") });
                permissions.Add(new Permission { Name = "Sửa", Code = $"{Permissions.LoaiCap}{Permissions.Update}", MenuId = new Guid("E17A625E0334C04D928300BBF8C83835") });
                permissions.Add(new Permission { Name = "Xóa", Code = $"{Permissions.LoaiCap}{Permissions.Delete}", MenuId = new Guid("E17A625E0334C04D928300BBF8C83835") });
                // Quản lý loại cáp cáp ngầm
                permissions.Add(new Permission { Name = "Xem", Code = $"{Permissions.LoaiCapCapNgam}{Permissions.View}", MenuId = new Guid("E17A625E0334C04D928300BBF8C88666") });
                permissions.Add(new Permission { Name = "Tạo", Code = $"{Permissions.LoaiCapCapNgam}{Permissions.Create}", MenuId = new Guid("E17A625E0334C04D928300BBF8C88666") });
                permissions.Add(new Permission { Name = "Sửa", Code = $"{Permissions.LoaiCapCapNgam}{Permissions.Update}", MenuId = new Guid("E17A625E0334C04D928300BBF8C88666") });
                permissions.Add(new Permission { Name = "Xóa", Code = $"{Permissions.LoaiCapCapNgam}{Permissions.Delete}", MenuId = new Guid("E17A625E0334C04D928300BBF8C88666") });

                // Quản lý vật liệu
                permissions.Add(new Permission { Name = "Xem", Code = $"{Permissions.VatLieu}{Permissions.View}", MenuId = new Guid("E17A625E0334C04D928300BBF8C83836") });
                permissions.Add(new Permission { Name = "Tạo", Code = $"{Permissions.VatLieu}{Permissions.Create}", MenuId = new Guid("E17A625E0334C04D928300BBF8C83836") });
                permissions.Add(new Permission { Name = "Sửa", Code = $"{Permissions.VatLieu}{Permissions.Update}", MenuId = new Guid("E17A625E0334C04D928300BBF8C83836") });
                permissions.Add(new Permission { Name = "Xóa", Code = $"{Permissions.VatLieu}{Permissions.Delete}", MenuId = new Guid("E17A625E0334C04D928300BBF8C83836") });
                // Quản lý vật liệu cáp ngầm
                permissions.Add(new Permission { Name = "Xem", Code = $"{Permissions.VatLieuCapNgam}{Permissions.View}", MenuId = new Guid("E17A625E0334C04D928300BBF8C88006") });
                permissions.Add(new Permission { Name = "Tạo", Code = $"{Permissions.VatLieuCapNgam}{Permissions.Create}", MenuId = new Guid("E17A625E0334C04D928300BBF8C88006") });
                permissions.Add(new Permission { Name = "Sửa", Code = $"{Permissions.VatLieuCapNgam}{Permissions.Update}", MenuId = new Guid("E17A625E0334C04D928300BBF8C88006") });
                permissions.Add(new Permission { Name = "Xóa", Code = $"{Permissions.VatLieuCapNgam}{Permissions.Delete}", MenuId = new Guid("E17A625E0334C04D928300BBF8C88006") });

                // Quản lý nhân công
                permissions.Add(new Permission { Name = "Xem", Code = $"{Permissions.NhanCong}{Permissions.View}", MenuId = new Guid("E17A625E0334C04D928300BBF8C81116") });
                permissions.Add(new Permission { Name = "Tạo", Code = $"{Permissions.NhanCong}{Permissions.Create}", MenuId = new Guid("E17A625E0334C04D928300BBF8C81116") });
                permissions.Add(new Permission { Name = "Sửa", Code = $"{Permissions.NhanCong}{Permissions.Update}", MenuId = new Guid("E17A625E0334C04D928300BBF8C81116") });
                permissions.Add(new Permission { Name = "Xóa", Code = $"{Permissions.NhanCong}{Permissions.Delete}", MenuId = new Guid("E17A625E0334C04D928300BBF8C81116") });
                // Quản lý nhân công cáp ngầm
                permissions.Add(new Permission { Name = "Xem", Code = $"{Permissions.NhanCongCapNgam}{Permissions.View}", MenuId = new Guid("E17A625E0334C04D928300BBF8C81117") });
                permissions.Add(new Permission { Name = "Tạo", Code = $"{Permissions.NhanCongCapNgam}{Permissions.Create}", MenuId = new Guid("E17A625E0334C04D928300BBF8C81117") });
                permissions.Add(new Permission { Name = "Sửa", Code = $"{Permissions.NhanCongCapNgam}{Permissions.Update}", MenuId = new Guid("E17A625E0334C04D928300BBF8C81117") });
                permissions.Add(new Permission { Name = "Xóa", Code = $"{Permissions.NhanCongCapNgam}{Permissions.Delete}", MenuId = new Guid("E17A625E0334C04D928300BBF8C81117") });

                // Quản lý Máy thi công
                permissions.Add(new Permission { Name = "Xem", Code = $"{Permissions.MTC}{Permissions.View}", MenuId = new Guid("E17A625E0334C04D928300BBF8C10001") });
                permissions.Add(new Permission { Name = "Tạo", Code = $"{Permissions.MTC}{Permissions.Create}", MenuId = new Guid("E17A625E0334C04D928300BBF8C10001") });
                permissions.Add(new Permission { Name = "Sửa", Code = $"{Permissions.MTC}{Permissions.Update}", MenuId = new Guid("E17A625E0334C04D928300BBF8C10001") });
                permissions.Add(new Permission { Name = "Xóa", Code = $"{Permissions.MTC}{Permissions.Delete}", MenuId = new Guid("E17A625E0334C04D928300BBF8C10001") });
                // Quản lý Máy thi công cáp ngầm
                permissions.Add(new Permission { Name = "Xem", Code = $"{Permissions.MTCCapNgam}{Permissions.View}", MenuId = new Guid("E17A625E0334C04D928300BBF8C10002") });
                permissions.Add(new Permission { Name = "Tạo", Code = $"{Permissions.MTCCapNgam}{Permissions.Create}", MenuId = new Guid("E17A625E0334C04D928300BBF8C10002") });
                permissions.Add(new Permission { Name = "Sửa", Code = $"{Permissions.MTCCapNgam}{Permissions.Update}", MenuId = new Guid("E17A625E0334C04D928300BBF8C10002") });
                permissions.Add(new Permission { Name = "Xóa", Code = $"{Permissions.MTCCapNgam}{Permissions.Delete}", MenuId = new Guid("E17A625E0334C04D928300BBF8C10002") });

                // Biểu giá công việc
                permissions.Add(new Permission { Name = "Xem", Code = $"{Permissions.BieuGiaCongViec}{Permissions.View}", MenuId = new Guid("E17A625E0334C04D928300BBF8C83843") });
                permissions.Add(new Permission { Name = "Tạo", Code = $"{Permissions.BieuGiaCongViec}{Permissions.Create}", MenuId = new Guid("E17A625E0334C04D928300BBF8C83843") });
                permissions.Add(new Permission { Name = "Sửa", Code = $"{Permissions.BieuGiaCongViec}{Permissions.Update}", MenuId = new Guid("E17A625E0334C04D928300BBF8C83843") });
                permissions.Add(new Permission { Name = "Xóa", Code = $"{Permissions.BieuGiaCongViec}{Permissions.Delete}", MenuId = new Guid("E17A625E0334C04D928300BBF8C83843") });
                // Biểu giá công việc cáp ngầm
                permissions.Add(new Permission { Name = "Xem", Code = $"{Permissions.BieuGiaCongViecCapNgam}{Permissions.View}", MenuId = new Guid("E17A625E0334C04D928300BBF8C88003") });
                permissions.Add(new Permission { Name = "Tạo", Code = $"{Permissions.BieuGiaCongViecCapNgam}{Permissions.Create}", MenuId = new Guid("E17A625E0334C04D928300BBF8C88003") });
                permissions.Add(new Permission { Name = "Sửa", Code = $"{Permissions.BieuGiaCongViecCapNgam}{Permissions.Update}", MenuId = new Guid("E17A625E0334C04D928300BBF8C88003") });
                permissions.Add(new Permission { Name = "Xóa", Code = $"{Permissions.BieuGiaCongViecCapNgam}{Permissions.Delete}", MenuId = new Guid("E17A625E0334C04D928300BBF8C88003") });

                #endregion

                #region Quản lý thông tin đơn giá
                // Đơn giá cáp
                permissions.Add(new Permission { Name = "Xem", Code = $"{Permissions.DonGiaCap}{Permissions.View}", MenuId = new Guid("E17A625E0334C04D928300BBF8C83838") });
                permissions.Add(new Permission { Name = "Tạo", Code = $"{Permissions.DonGiaCap}{Permissions.Create}", MenuId = new Guid("E17A625E0334C04D928300BBF8C83838") });
                permissions.Add(new Permission { Name = "Sửa", Code = $"{Permissions.DonGiaCap}{Permissions.Update}", MenuId = new Guid("E17A625E0334C04D928300BBF8C83838") });
                permissions.Add(new Permission { Name = "Xóa", Code = $"{Permissions.DonGiaCap}{Permissions.Delete}", MenuId = new Guid("E17A625E0334C04D928300BBF8C83838") });
                // Đơn giá cáp ngầm
                permissions.Add(new Permission { Name = "Xem", Code = $"{Permissions.DonGiaCapCapNgam}{Permissions.View}", MenuId = new Guid("E17A625E0334C04D928300BBF8C83888") });
                permissions.Add(new Permission { Name = "Tạo", Code = $"{Permissions.DonGiaCapCapNgam}{Permissions.Create}", MenuId = new Guid("E17A625E0334C04D928300BBF8C83888") });
                permissions.Add(new Permission { Name = "Sửa", Code = $"{Permissions.DonGiaCapCapNgam}{Permissions.Update}", MenuId = new Guid("E17A625E0334C04D928300BBF8C83888") });
                permissions.Add(new Permission { Name = "Xóa", Code = $"{Permissions.DonGiaCapCapNgam}{Permissions.Delete}", MenuId = new Guid("E17A625E0334C04D928300BBF8C83888") });

                // Đơn giá nhân công
                permissions.Add(new Permission { Name = "Xem", Code = $"{Permissions.DonGiaNhanCong}{Permissions.View}", MenuId = new Guid("E17A625E0334C04D928300BBF8C83839") });
                permissions.Add(new Permission { Name = "Tạo", Code = $"{Permissions.DonGiaNhanCong}{Permissions.Create}", MenuId = new Guid("E17A625E0334C04D928300BBF8C83839") });
                permissions.Add(new Permission { Name = "Sửa", Code = $"{Permissions.DonGiaNhanCong}{Permissions.Update}", MenuId = new Guid("E17A625E0334C04D928300BBF8C83839") });
                permissions.Add(new Permission { Name = "Xóa", Code = $"{Permissions.DonGiaNhanCong}{Permissions.Delete}", MenuId = new Guid("E17A625E0334C04D928300BBF8C83839") });
                // Đơn giá nhân công cáp ngầm
                permissions.Add(new Permission { Name = "Xem", Code = $"{Permissions.DonGiaNhanCongCapNgam}{Permissions.View}", MenuId = new Guid("E17A625E0334C04D928300BBF8C83890") });
                permissions.Add(new Permission { Name = "Tạo", Code = $"{Permissions.DonGiaNhanCongCapNgam}{Permissions.Create}", MenuId = new Guid("E17A625E0334C04D928300BBF8C83890") });
                permissions.Add(new Permission { Name = "Sửa", Code = $"{Permissions.DonGiaNhanCongCapNgam}{Permissions.Update}", MenuId = new Guid("E17A625E0334C04D928300BBF8C83890") });
                permissions.Add(new Permission { Name = "Xóa", Code = $"{Permissions.DonGiaNhanCongCapNgam}{Permissions.Delete}", MenuId = new Guid("E17A625E0334C04D928300BBF8C83890") });

                // Đơn giá vật liệu
                permissions.Add(new Permission { Name = "Xem", Code = $"{Permissions.DonGiaVatLieu}{Permissions.View}", MenuId = new Guid("E17A625E0334C04D928300BBF8C83840") });
                permissions.Add(new Permission { Name = "Tạo", Code = $"{Permissions.DonGiaVatLieu}{Permissions.Create}", MenuId = new Guid("E17A625E0334C04D928300BBF8C83840") });
                permissions.Add(new Permission { Name = "Sửa", Code = $"{Permissions.DonGiaVatLieu}{Permissions.Update}", MenuId = new Guid("E17A625E0334C04D928300BBF8C83840") });
                permissions.Add(new Permission { Name = "Xóa", Code = $"{Permissions.DonGiaVatLieu}{Permissions.Delete}", MenuId = new Guid("E17A625E0334C04D928300BBF8C83840") });
                // Đơn giá vật liệu cáp ngầm
                permissions.Add(new Permission { Name = "Xem", Code = $"{Permissions.DonGiaVatLieuCapNgam}{Permissions.View}", MenuId = new Guid("E17A625E0334C04D928300BBF8C83891") });
                permissions.Add(new Permission { Name = "Tạo", Code = $"{Permissions.DonGiaVatLieuCapNgam}{Permissions.Create}", MenuId = new Guid("E17A625E0334C04D928300BBF8C83891") });
                permissions.Add(new Permission { Name = "Sửa", Code = $"{Permissions.DonGiaVatLieuCapNgam}{Permissions.Update}", MenuId = new Guid("E17A625E0334C04D928300BBF8C83891") });
                permissions.Add(new Permission { Name = "Xóa", Code = $"{Permissions.DonGiaVatLieuCapNgam}{Permissions.Delete}", MenuId = new Guid("E17A625E0334C04D928300BBF8C83891") });

                // Đơn giá máy thi công
                permissions.Add(new Permission { Name = "Xem", Code = $"{Permissions.DonGiaMTC}{Permissions.View}", MenuId = new Guid("E17A625E0334C04D928300BBF8C10003") });
                permissions.Add(new Permission { Name = "Tạo", Code = $"{Permissions.DonGiaMTC}{Permissions.Create}", MenuId = new Guid("E17A625E0334C04D928300BBF8C10003") });
                permissions.Add(new Permission { Name = "Sửa", Code = $"{Permissions.DonGiaMTC}{Permissions.Update}", MenuId = new Guid("E17A625E0334C04D928300BBF8C10003") });
                permissions.Add(new Permission { Name = "Xóa", Code = $"{Permissions.DonGiaMTC}{Permissions.Delete}", MenuId = new Guid("E17A625E0334C04D928300BBF8C10003") });
                // Đơn giá máy thi công cáp ngầm
                permissions.Add(new Permission { Name = "Xem", Code = $"{Permissions.DonGiaMTCCapNgam}{Permissions.View}", MenuId = new Guid("E17A625E0334C04D928300BBF8C10004") });
                permissions.Add(new Permission { Name = "Tạo", Code = $"{Permissions.DonGiaMTCCapNgam}{Permissions.Create}", MenuId = new Guid("E17A625E0334C04D928300BBF8C10004") });
                permissions.Add(new Permission { Name = "Sửa", Code = $"{Permissions.DonGiaMTCCapNgam}{Permissions.Update}", MenuId = new Guid("E17A625E0334C04D928300BBF8C10004") });
                permissions.Add(new Permission { Name = "Xóa", Code = $"{Permissions.DonGiaMTCCapNgam}{Permissions.Delete}", MenuId = new Guid("E17A625E0334C04D928300BBF8C10004") });

                // Đơn giá vật liệu chiết tinh
                permissions.Add(new Permission { Name = "Xem", Code = $"{Permissions.DonGiaVatLieuChietTinh}{Permissions.View}", MenuId = new Guid("E17A625E0334C04D928300BBF8C83841") });
                permissions.Add(new Permission { Name = "Tạo", Code = $"{Permissions.DonGiaVatLieuChietTinh}{Permissions.Create}", MenuId = new Guid("E17A625E0334C04D928300BBF8C83841") });
                permissions.Add(new Permission { Name = "Sửa", Code = $"{Permissions.DonGiaVatLieuChietTinh}{Permissions.Update}", MenuId = new Guid("E17A625E0334C04D928300BBF8C83841") });
                permissions.Add(new Permission { Name = "Xóa", Code = $"{Permissions.DonGiaVatLieuChietTinh}{Permissions.Delete}", MenuId = new Guid("E17A625E0334C04D928300BBF8C83841") });
                // Đơn giá vật liệu chiết tinh cáp ngầm
                permissions.Add(new Permission { Name = "Xem", Code = $"{Permissions.DonGiaVatLieuChietTinhCapNgam}{Permissions.View}", MenuId = new Guid("E17A625E0334C04D928300BBF8C83889") });
                permissions.Add(new Permission { Name = "Tạo", Code = $"{Permissions.DonGiaVatLieuChietTinhCapNgam}{Permissions.Create}", MenuId = new Guid("E17A625E0334C04D928300BBF8C83889") });
                permissions.Add(new Permission { Name = "Sửa", Code = $"{Permissions.DonGiaVatLieuChietTinhCapNgam}{Permissions.Update}", MenuId = new Guid("E17A625E0334C04D928300BBF8C83889") });
                permissions.Add(new Permission { Name = "Xóa", Code = $"{Permissions.DonGiaVatLieuChietTinhCapNgam}{Permissions.Delete}", MenuId = new Guid("E17A625E0334C04D928300BBF8C83889") });

                // Cấu hình chiết tính
                permissions.Add(new Permission { Name = "Xem", Code = $"{Permissions.CauHinhChietTinh}{Permissions.View}", MenuId = new Guid("E17A625E0334C04D928300BBF8C88100") });
                permissions.Add(new Permission { Name = "Tạo", Code = $"{Permissions.CauHinhChietTinh}{Permissions.Create}", MenuId = new Guid("E17A625E0334C04D928300BBF8C88100") });
                permissions.Add(new Permission { Name = "Sửa", Code = $"{Permissions.CauHinhChietTinh}{Permissions.Update}", MenuId = new Guid("E17A625E0334C04D928300BBF8C88100") });
                permissions.Add(new Permission { Name = "Xóa", Code = $"{Permissions.CauHinhChietTinh}{Permissions.Delete}", MenuId = new Guid("E17A625E0334C04D928300BBF8C88100") });
                // Cấu hình chiết tính cáp ngầm
                permissions.Add(new Permission { Name = "Xem", Code = $"{Permissions.CauHinhChietTinhCapNgam}{Permissions.View}", MenuId = new Guid("E17A625E0334C04D928300BBF8C88101") });
                permissions.Add(new Permission { Name = "Tạo", Code = $"{Permissions.CauHinhChietTinhCapNgam}{Permissions.Create}", MenuId = new Guid("E17A625E0334C04D928300BBF8C88101") });
                permissions.Add(new Permission { Name = "Sửa", Code = $"{Permissions.CauHinhChietTinhCapNgam}{Permissions.Update}", MenuId = new Guid("E17A625E0334C04D928300BBF8C88101") });
                permissions.Add(new Permission { Name = "Xóa", Code = $"{Permissions.CauHinhChietTinhCapNgam}{Permissions.Delete}", MenuId = new Guid("E17A625E0334C04D928300BBF8C88101") });


                #endregion

                #region QUẢN TRỊ CHI TIẾT BIỂU GIÁ
                // Đồng bộ
                permissions.Add(new Permission { Name = "Đồng bộ", Code = $"{Permissions.DongBoBieuGia}{Permissions.View}", MenuId = new Guid("E17A625E0334C04D928300BBF8C90000") });
                // Chi tiết biểu giá
                permissions.Add(new Permission { Name = "Xem", Code = $"{Permissions.ChiTietBieuGia}{Permissions.View}", MenuId = new Guid("E17A625E0334C04D928300BBF8C83842") });
                permissions.Add(new Permission { Name = "Sửa", Code = $"{Permissions.ChiTietBieuGia}{Permissions.Update}", MenuId = new Guid("E17A625E0334C04D928300BBF8C83842") });
                // Gửi duyệt
                permissions.Add(new Permission { Name = "Gửi duyệt", Code = $"{Permissions.GuiDuyetBieuGia}{Permissions.View}", MenuId = new Guid("E17A625E0334C04D928300BBF8C83845") });
                // Xác nhận biểu giá
                permissions.Add(new Permission { Name = "Duyệt", Code = $"{Permissions.XacNhanBieuGia}{Permissions.View}", MenuId = new Guid("E17A625E0334C04D928300BBF8C83846") });


                // Đồng bộ cáp ngầm
                permissions.Add(new Permission { Name = "Đồng bộ", Code = $"{Permissions.DongBoBieuGiaCapNgam}{Permissions.View}", MenuId = new Guid("E17A625E0334C04D928300BBF8C90001") });
                // Chi tiết biểu giá cáp ngầm
                permissions.Add(new Permission { Name = "Xem", Code = $"{Permissions.ChiTietBieuGiaCapNgam}{Permissions.View}", MenuId = new Guid("E17A625E0334C04D928300BBF8C80000") });
                permissions.Add(new Permission { Name = "Sửa", Code = $"{Permissions.ChiTietBieuGiaCapNgam}{Permissions.Update}", MenuId = new Guid("E17A625E0334C04D928300BBF8C80000") });
                // Gửi duyệt cáp ngầm
                permissions.Add(new Permission { Name = "Gửi duyệt", Code = $"{Permissions.GuiDuyetBieuGiaCapNgam}{Permissions.View}", MenuId = new Guid("E17A625E0334C04D928300BBF8C80001") });
                // Xác nhận biểu giá cáp ngầm
                permissions.Add(new Permission { Name = "Duyệt", Code = $"{Permissions.XacNhanBieuGiaCapNgam}{Permissions.View}", MenuId = new Guid("E17A625E0334C04D928300BBF8C80002") });

                #endregion

                #region HỆ THỐNG BÁO CÁO
                permissions.Add(new Permission { Name = "Xem", Code = $"{Permissions.BaoCaoCapTrenKhong}{Permissions.Delete}", MenuId = new Guid("E17A625E0334C04D928300BBF8C90002") });
                permissions.Add(new Permission { Name = "Xem", Code = $"{Permissions.BaoCaoCapNgam}{Permissions.Delete}", MenuId = new Guid("E17A625E0334C04D928300BBF8C90003") });

                #endregion

                context.Permission.AddRange(permissions);

            }

            if (!context.Position.Any())
            {
                List<Position> positions = new List<Position>();
                positions.Add(new Position { Id = new Guid("A803B9302260C647960DC65AB20AD553"), Title = "Quản trị viên", Value = PositionEnum.Administrator.GetHashCode(), CreatedDate = DateTime.Now });
                positions.Add(new Position { Id = new Guid("63B7B10A719245409815FC3296F0341F"), Title = "Lãnh đạo B09", Value = PositionEnum.LanhDaoB09.GetHashCode(), CreatedDate = DateTime.Now });
                positions.Add(new Position { Id = new Guid("C2561F284076114CB824B39D1F366F2B"), Title = "Chuyên viên B09", Value = PositionEnum.ChuyenVienB09.GetHashCode(), CreatedDate = DateTime.Now });
                positions.Add(new Position { Id = new Guid("E17B5211E053D840A7C6E12C312DA8B1"), Title = "Lãnh đạo B08", Value = PositionEnum.LanhDaoB08.GetHashCode(), CreatedDate = DateTime.Now });
                positions.Add(new Position { Id = new Guid("35EEEB98CF39BC47A0A2B1E93D5F0E20"), Title = "Chuyên viên B08", Value = PositionEnum.ChuyenVienB08.GetHashCode(), CreatedDate = DateTime.Now });
                context.Position.AddRange(positions);
            }

            if (!context.DM_KhuVuc.Any())
            {
                var listVungKhuVuc = new List<DM_KhuVuc>();

                listVungKhuVuc.Add(new DM_KhuVuc { Id = new Guid("C09E6504B3DBC74180EB85C76EB329C1"), TenKhuVuc = "Vùng I - Khu Vực 1", GhiChu = "1" });
                listVungKhuVuc.Add(new DM_KhuVuc { Id = new Guid("C09E6504B3DBC74180EB85C76EB329C2"), TenKhuVuc = "Vùng I - Khu Vực 2", GhiChu = "2" });
                listVungKhuVuc.Add(new DM_KhuVuc { Id = new Guid("C09E6504B3DBC74180EB85C76EB329C3"), TenKhuVuc = "Vùng II", GhiChu = "3" });

                context.DM_KhuVuc.AddRange(listVungKhuVuc);
                await context.SaveChangesAsync();
            }

            if (!context.DM_LoaiBieuGia.Any())
            {
                var listLoaiBieuGia = new List<DM_LoaiBieuGia>();

                listLoaiBieuGia.Add(new DM_LoaiBieuGia { IdKhuVuc = new Guid("C09E6504B3DBC74180EB85C76EB329C1"), MaLoaiBieuGia = "1", TenLoaiBieuGia = "Đơn giá lắp đặt dây sau công tơ" });
                listLoaiBieuGia.Add(new DM_LoaiBieuGia { IdKhuVuc = new Guid("C09E6504B3DBC74180EB85C76EB329C1"), MaLoaiBieuGia = "2", TenLoaiBieuGia = "Đơn giá thay dây dẫn điện sau công tơ" });
                listLoaiBieuGia.Add(new DM_LoaiBieuGia { IdKhuVuc = new Guid("C09E6504B3DBC74180EB85C76EB329C1"), MaLoaiBieuGia = "3", TenLoaiBieuGia = "Đơn giá thay đổi công suất SDĐ/Thay đổi loại công tơ" });
                listLoaiBieuGia.Add(new DM_LoaiBieuGia { IdKhuVuc = new Guid("C09E6504B3DBC74180EB85C76EB329C1"), MaLoaiBieuGia = "4", TenLoaiBieuGia = "Đơn giá thay đổi vị trí thiết bị đo đếm" });
                listLoaiBieuGia.Add(new DM_LoaiBieuGia { IdKhuVuc = new Guid("C09E6504B3DBC74180EB85C76EB329C2"), MaLoaiBieuGia = "1", TenLoaiBieuGia = "Đơn giá lắp đặt dây sau công tơ" });
                listLoaiBieuGia.Add(new DM_LoaiBieuGia { IdKhuVuc = new Guid("C09E6504B3DBC74180EB85C76EB329C2"), MaLoaiBieuGia = "2", TenLoaiBieuGia = "Đơn giá thay dây dẫn điện sau công tơ" });
                listLoaiBieuGia.Add(new DM_LoaiBieuGia { IdKhuVuc = new Guid("C09E6504B3DBC74180EB85C76EB329C2"), MaLoaiBieuGia = "3", TenLoaiBieuGia = "Đơn giá thay đổi công suất SDĐ/Thay đổi loại công tơ" });
                listLoaiBieuGia.Add(new DM_LoaiBieuGia { IdKhuVuc = new Guid("C09E6504B3DBC74180EB85C76EB329C2"), MaLoaiBieuGia = "4", TenLoaiBieuGia = "Đơn giá thay đổi vị trí thiết bị đo đếm" });
                listLoaiBieuGia.Add(new DM_LoaiBieuGia { IdKhuVuc = new Guid("C09E6504B3DBC74180EB85C76EB329C3"), MaLoaiBieuGia = "1", TenLoaiBieuGia = "Đơn giá lắp đặt dây sau công tơ" });
                listLoaiBieuGia.Add(new DM_LoaiBieuGia { IdKhuVuc = new Guid("C09E6504B3DBC74180EB85C76EB329C3"), MaLoaiBieuGia = "2", TenLoaiBieuGia = "Đơn giá thay dây dẫn điện sau công tơ" });
                listLoaiBieuGia.Add(new DM_LoaiBieuGia { IdKhuVuc = new Guid("C09E6504B3DBC74180EB85C76EB329C3"), MaLoaiBieuGia = "3", TenLoaiBieuGia = "Đơn giá thay đổi công suất SDĐ/Thay đổi loại công tơ" });
                listLoaiBieuGia.Add(new DM_LoaiBieuGia { IdKhuVuc = new Guid("C09E6504B3DBC74180EB85C76EB329C3"), MaLoaiBieuGia = "4", TenLoaiBieuGia = "Đơn giá thay đổi vị trí thiết bị đo đếm" });

                await context.DM_LoaiBieuGia.AddRangeAsync(listLoaiBieuGia);
                await context.SaveChangesAsync();

            }

            await context.SaveChangesAsync();
        }
    }
}
