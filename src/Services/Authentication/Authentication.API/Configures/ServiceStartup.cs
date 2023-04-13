using Authentication.Application.Queries.BieuGiaCongViecQuery;
using Authentication.Application.Queries.CommonQuery;
using Authentication.Application.Queries.DM_BieuGiaQuery;
using Authentication.Application.Queries.DM_CongViecQuery;
using Authentication.Application.Queries.DM_KhuVucQuery;
using Authentication.Application.Queries.DM_LoaiBieuGiaQuery;
using Authentication.Application.Queries.DM_LoaiCapQuery;
using Authentication.Application.Queries.DM_VatLieuChietTinhQuery;
using Authentication.Application.Queries.DM_VatLieuQuery;
using Authentication.Application.Queries.DM_VungQuery;
using Authentication.Application.Queries.DonGiaChietTinhQuery;
using Authentication.Application.Queries.DonGiaNhanCongQuery;
using Authentication.Application.Queries.DonGiaVatLieuQuery;
using Authentication.Application.Queries.GiaCapQuery;
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
                options.Password.RequireDigit = false;
                options.Password.RequiredLength = 6;
                options.Password.RequireNonAlphanumeric = false;
                options.Password.RequireUppercase = false;
                options.Password.RequireLowercase = false;
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
            services.AddScoped<IDM_KhuVucQuery, DM_KhuVucQuery>(); // quy tắc Interface trước class sau
            services.AddScoped<IDM_VungQuery, DM_VungQuery>(); // quy tắc Interface trước class sau
            services.AddScoped<IDM_CongViecQuery, DM_CongViecQuery>(); // quy tắc Interface trước class sau
           
            services.AddScoped<IDM_LoaiCapQuery, DM_LoaiCapQuery>(); // quy tắc Interface trước class sau
            services.AddScoped<IDM_VatLieuQuery, DM_VatLieuQuery>(); // quy tắc Interface trước class sau
            services.AddScoped<IDM_VatLieuChietTinhQuery, DM_VatLieuChietTinhQuery>(); // quy tắc Interface trước class sau
            services.AddScoped<IGiaCapQuery, GiaCapQuery>(); // quy tắc Interface trước class sau
            services.AddScoped<IDonGiaVatLieuQuery, DonGiaVatLieuQuery>(); // quy tắc Interface trước class sau
            services.AddScoped<IDonGiaNhanCongQuery, DonGiaNhanCongQuery>(); // quy tắc Interface trước class sau
            services.AddScoped<IDonGiaChietTinhQuery, DonGiaChietTinhQuery>(); // quy tắc Interface trước class sau
          
            services.AddScoped<IDM_BieuGiaQuery, DM_BieuGiaQuery>(); // quy tắc Interface trước class sau
            services.AddScoped<IBieuGiaCongViecQuery, BieuGiaCongViecQuery>(); // quy tắc Interface trước class sau
            return services;
        }
    }
}
