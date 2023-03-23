using Authentication.Infrastructure.AggregatesModel.UserAggregate;
using Microsoft.AspNetCore.Identity;
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
        }
    }
}
