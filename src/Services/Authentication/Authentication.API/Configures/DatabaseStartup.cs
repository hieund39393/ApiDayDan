using Authentication.Infrastructure.EF;
using EVN.Core.Common;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Authentication.API.Configurations
{
    public static class DatabaseConfiguration
    {
        public static IServiceCollection AddDatabaseModule(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddDbContext<ExOneDbContext>(options =>
                           options.UseSqlServer(configuration.GetConnectionString(AppConstants.MainConnectionString)));
            return services;
        }
    }
}
