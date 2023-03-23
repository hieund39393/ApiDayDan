using Authentication.Infrastructure.AggregatesModel.ModuleAggregate;
using Authentication.Infrastructure.AggregatesModel.UserAggregate;
using Microsoft.AspNetCore.Identity;
using System;
using System.Threading.Tasks;

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
                List<Module> module = new List<Module>();
                //donVis.Add(new DonVi { Id = new Guid("C09E6504B3DBC74180EB85C76EB329C4"), MaDonVi = "P", TenDonVi = "Tập đoàn điện lực Việt Nam", LoaiDonVi = 1, IsDongBo = true, IsTongCongTy = true, FlagLanhDaoKy = true, CreatedDate = DateTime.Now, IsDeleted = false });
                module.Add(new Module { Id = new Guid("C09E6504B3DBC74180EB85C76EB329C4") , ModuleCode = "QTHT" , ModuleName = "Quản trị hệ thống" , NumberOrder = 1 });
                module.Add(new Module { Id = new Guid("C09E6504B3DBC74180EB85C76EB329C5") , ModuleCode = "HTBC" , ModuleName = "Hệ thống báo cáo", NumberOrder = 2 });
                module.Add(new Module { Id = new Guid("C09E6504B3DBC74180EB85C76EB329C6") , ModuleCode = "QLTTG" , ModuleName = "Quản lý thông tin giá", NumberOrder = 3 });
                module.Add(new Module { Id = new Guid("C09E6504B3DBC74180EB85C76EB329C7") , ModuleCode = "QLND" , ModuleName = "Quản lý người dùng", NumberOrder = 1 , ParentId = new Guid("C09E6504B3DBC74180EB85C76EB329C4") } );
                module.Add(new Module { Id = new Guid("C09E6504B3DBC74180EB85C76EB329C8") , ModuleCode = "QLMN" , ModuleName = "Quản lý menu", NumberOrder = 2, ParentId = new Guid("C09E6504B3DBC74180EB85C76EB329C4") });
       
                context.Module.AddRange(module);
                await context.SaveChangesAsync();
            }
        }
    }
}
