using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;
using Serilog;
using System;
using System.IO;
using Authentication.Infrastructure.Extensions;
using Authentication.Infrastructure.EF;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore;
using EVN.Core.Common;
using Serilog.Sinks.MSSqlServer;
using Authentication.Infrastructure.EF.EntityConfigurations;
using System.Collections.ObjectModel;
using System.Data;
using Serilog.Events;
using Serilog.Filters;
using Authentication.Infrastructure.AggregatesModel.UserAggregate;
using System.Configuration;
using Microsoft.AspNetCore.Builder;
using EVN.Core.Extensions;

namespace Authentication.API
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);
            var host = CreateHostBuilder(args, builder).Build();

            Log.Logger = CreateSerilogLogger();
            LogHelper.Logger = CreateSystemLogger();

            Log.Information("Applying migrations ({AuthenticationDbContext})...");
            host.MigrateDbContext<ExOneDbContext>((context, services) =>
            {
                var userManager = (UserManager<User>)services.GetService(typeof(UserManager<User>));
                new ExOneDbContextSeed().SeedAsync(context, userManager).Wait();
            });

            Log.Information("Starting web host ({AuthenticationDbContext})...");
            host.Run();
        }

        public static IHostBuilder CreateHostBuilder(string[] args, WebApplicationBuilder builder) =>

            Host.CreateDefaultBuilder(args)
                .ConfigureWebHostDefaults(webBuilder =>
                {
                    webBuilder.UseStartup<Startup>();
                    webBuilder.UseKestrel(options =>
                    {
                        options.Limits.MaxRequestBodySize = Convert.ToInt64(builder.Configuration["MaxRequestBodySize"]);
                    })
                .UseContentRoot(Directory.GetCurrentDirectory())
                .UseConfiguration(builder.Configuration)
                .UseSerilog()
                .UseDefaultServiceProvider(options => options.ValidateScopes = false);
                });

        private static ILogger CreateSerilogLogger()
        {
            return new LoggerConfiguration()
                .Enrich.FromLogContext()
                .WriteTo.Console()
                .WriteTo.File(
                $"Logs/log_{DateTime.Now}.txt",
                fileSizeLimitBytes: 1_000_000,
                rollOnFileSizeLimit: true,
                shared: true,
                flushToDiskInterval: TimeSpan.FromSeconds(1))
                .CreateLogger();
        }

        private static ILogger CreateSystemLogger()
        {
            var configuration = Helpers.GetConfiguration();
            var columnOptions = GetColumnOptions();
            return new LoggerConfiguration()
                .Enrich.FromLogContext()
                //.Filter.ByExcluding(Matching.FromSource("Microsoft"))
                //.Filter.ByExcluding(Matching.FromSource("System"))
                .WriteTo.MSSqlServer(configuration.GetConnectionString(AppConstants.MainConnectionString),
                    sinkOptions: new MSSqlServerSinkOptions { TableName = SystemLogConfiguration.TableName }
                    , null, null, LogEventLevel.Information, null, columnOptions: columnOptions, null, null)
                .CreateLogger();
        }

        private static ColumnOptions GetColumnOptions()
        {
            var columnOptions = new ColumnOptions
            {
                AdditionalColumns = new Collection<SqlColumn>
               {
                   new SqlColumn("UserName", SqlDbType.NVarChar),
                   new SqlColumn("LogEvent", SqlDbType.NVarChar),
                   new SqlColumn("IP", SqlDbType.NVarChar)
               }
            };
            return columnOptions;
        }
    }
}