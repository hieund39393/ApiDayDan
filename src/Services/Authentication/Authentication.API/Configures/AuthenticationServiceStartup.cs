using EVN.Core.ConfigurationSettings;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;
using System;
using System.IdentityModel.Tokens.Jwt;
using System.Text;
using System.Threading.Tasks;

namespace Authentication.API.Configurations
{
    /// <summary>
    /// Startup
    /// </summary>
    public static class AuthenticationServiceStartup
    {
        /// <summary>
        /// Add Authorization Service
        /// </summary>
        /// <param name="services"></param>
        /// <param name="configuration"></param>
        /// <returns></returns>
        public static IServiceCollection AddAuthorizationService(this IServiceCollection services, IConfiguration configuration)
        {
            var appSettings = configuration.GetSection("AppSettings").Get<AppSettings>();
            services.AddAuthentication(o =>
            {
                o.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                o.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
                o.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;
            })
            .AddJwtBearer(o =>
            {
                o.IncludeErrorDetails = true;
                o.RefreshOnIssuerKeyNotFound = true;
                o.SaveToken = true;
                o.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuer = true,
                    ValidateAudience = true,
                    ValidateLifetime = true,
                    ValidateIssuerSigningKey = true,
                    ValidIssuer = appSettings.Jwt.Issuer,
                    ValidAudience = appSettings.Jwt.Audience,
                    IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(appSettings.Jwt.SecretKey)),
                    ClockSkew = TimeSpan.Zero,
                };
            });
            services.Configure<SecurityStampValidatorOptions>(options =>
            {
                options.ValidationInterval = TimeSpan.FromMinutes(1);
            });
            return services;
        }
    }
}
