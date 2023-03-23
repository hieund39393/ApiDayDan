using EVN.Core.Common;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;
using System.IO;

namespace Authentication.Infrastructure.EF
{
    public class ExOneDbContextFactory : IDesignTimeDbContextFactory<ExOneDbContext>
    {
        public ExOneDbContext CreateDbContext(string[] args)
        {
            IConfigurationRoot configuration = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile($"appsettings.{Environments.Development}.json")
                .Build();

            var connectionString = configuration.GetConnectionString(AppConstants.MainConnectionString);
            var optionsBuilder = new DbContextOptionsBuilder<ExOneDbContext>();
            optionsBuilder.UseSqlServer(connectionString);
            return new ExOneDbContext(optionsBuilder.Options);
        }
    }
}
