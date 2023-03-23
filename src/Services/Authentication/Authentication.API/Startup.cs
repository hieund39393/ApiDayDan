using EVN.Core.Extensions;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Authentication.API.Configurations;
using EVN.Core.ConfigurationSettings;
using Authentication.API.Configures;
using Authentication.Application.Infrastructure;
using FluentValidation.AspNetCore;
using Microsoft.AspNetCore.HttpOverrides;

namespace Authentication.API
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            var appSettingsSection = Configuration.GetSection("AppSettings");
            services.Configure<AppSettings>(appSettingsSection);
            var appSettings = appSettingsSection.Get<AppSettings>();
            services.AddSingleton(appSettings);
            //Add DB context
            services.AddDatabaseModule(Configuration);

            //Mediator, dependency
            services
                .AddServiceModule()
                .AddMediaR()
                .AddCors()
                .AddSwaggerModule()
                .AddControllers()
                .AddNewtonsoftJson();

            // ReactJs's default header name for sending the XSRF token.
            services.Configure<ApiBehaviorOptions>(options =>
            {
                options.SuppressModelStateInvalidFilter = true;
            });

            //Lower case api url
            services.AddRouting(options => { options.LowercaseUrls = true; });

            // AutoMapper settings
            services.AddAutoMapperModule();

            services.AddHttpContextAccessor();

            //Global filter
            services.AddMvc(options =>
            {
                options.Filters.Add(typeof(HttpGlobalExceptionFilter));
            }).AddFluentValidation(s =>
            {
                s.RegisterValidatorsFromAssemblyContaining<Startup>();
            });

            // AddAuthorizationService
            services.AddAuthorizationService(Configuration);
            services.AddHealthChecks();

        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env, ILoggerFactory loggerFactory)
        {
            var appSettings = Configuration.GetSection("AppSettings").Get<AppSettings>();
            if (!env.IsProduction())
            {
                app.UseDeveloperExceptionPage();
            }
            //https://www.thecodebuzz.com/get-ip-address-in-asp-net-core-guidelines/

            app.UseForwardedHeaders(new ForwardedHeadersOptions
            {
                ForwardedHeaders = ForwardedHeaders.XForwardedFor | ForwardedHeaders.XForwardedProto
            });
            app.UseRouting();
            app.UseCors(options => options.WithOrigins(appSettings?.AllowedHosts).AllowAnyMethod().AllowAnyHeader().AllowCredentials());
            app.UseStaticFiles();
            app.UseCookiePolicy();
            app.UseAuthentication();
            app.UseAuthorization();
            app.UseHttpsRedirection();
            app.UseOpenApi();
            app.UseAppSwagger(Configuration);
            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
                endpoints.MapHealthChecks("/health");
            });

            // Jwt Token 
            TokenExtensions.Configure(app.ApplicationServices.GetRequiredService<IHttpContextAccessor>());
        }
    }
}
