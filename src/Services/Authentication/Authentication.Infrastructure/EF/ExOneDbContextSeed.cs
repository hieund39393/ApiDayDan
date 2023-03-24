using Authentication.Infrastructure.AggregatesModel.MenuAggregate;
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
                List<Module> listModule = new List<Module>();

                listModule.Add(new Module { Id = new Guid("C09E6504B3DBC74180EB85C76EB329C1"), Code = "QTHT", Name = "Quản trị hệ thống", Order = 1 });
                listModule.Add(new Module { Id = new Guid("C09E6504B3DBC74180EB85C76EB329C2"), Code = "HTBC", Name = "Hệ thống báo cáo", Order = 2 });

                context.Module.AddRange(listModule);
            }
            if (!context.Menu.Any())
            {
                List<Menu> listMenu = new List<Menu>();

                listMenu.Add(new Menu { Code = "qlnn", Name = "Quản lý người dùng", Order = 1, ModuleId = new Guid("C09E6504B3DBC74180EB85C76EB329C1") });
                listMenu.Add(new Menu { Code = "qlmn", Name = "Quản lý menu", Order = 2, ModuleId = new Guid("C09E6504B3DBC74180EB85C76EB329C1") });
            }

            await context.SaveChangesAsync();
        }
    }
}
