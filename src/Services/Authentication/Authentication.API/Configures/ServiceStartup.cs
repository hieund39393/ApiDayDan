using Authentication.Application.Queries.CommonQuery;
using Authentication.Application.Queries.DM_BieuGiaQuery;
using Authentication.Application.Queries.DM_LoaiBieuGiaQuery;
using Authentication.Application.Queries.MenuQuery;
using Authentication.Application.Queries.ModuleQuery;
using Authentication.Application.Queries.RoleQuery;
using Authentication.Application.Queries.UserQuery;
using Authentication.Application.Services;
using Authentication.Infrastructure.AggregatesModel.UserAggregate;
using Authentication.Infrastructure.EF;
using Authentication.Infrastructure.Repositories;
using EVN.Core.Common.JwtToken;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.DependencyInjection;

namespace Authentication.API.Configurations
{
    public static class ServiceStartup
    {
        public static IServiceCollection AddServiceModule(this IServiceCollection services)
        {
            // services will be added here by the generator
            services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();
            services.AddIdentity<User, Role>(options =>
            {
                //options.User.AllowedUserNameCharacters;
                options.Password.RequireDigit = true;
                options.Password.RequiredLength = 8;
                options.Password.RequireNonAlphanumeric = true;
                options.Password.RequireUppercase = true;
                options.Password.RequireLowercase = true;
            })
            .AddEntityFrameworkStores<ExOneDbContext>()
            .AddDefaultTokenProviders();
            services.AddScoped<IUnitOfWork, UnitOfWork>();
            services.AddScoped<IJwtHandler, JwtHandler>();
            services.AddScoped<IUserService, UserService>();
            services.AddScoped<IUserQuery, UserQuery>();
            services.AddScoped<IRoleQuery, RoleQuery>();
            services.AddScoped<IFileService, FileService>();
            services.AddScoped<IDomainService, DomainService>();
            services.AddScoped<ICommonQuery, CommonQuery>();
            services.AddScoped<IModuleQuery, ModuleQuery>();
            services.AddScoped<IMenuQuery, MenuQuery>();

            //Danh mục Tiến Anh
            services.AddScoped<IDM_LoaiBieuGiaQuery, DM_LoaiBieuGiaQuery>(); // quy tắc Interface trước class sau
          
            services.AddScoped<IDM_BieuGiaQuery, DM_BieuGiaQuery>(); // quy tắc Interface trước class sau
            return services;
        }
    }
}
