using Authentication.API;
using AutoMapper;
using Microsoft.Extensions.DependencyInjection;

namespace Authentication.API.Configurations 
{
    public static class AutoMapperStartup {
        public static IServiceCollection AddAutoMapperModule(this IServiceCollection services)
        {
            services.AddAutoMapper(typeof(Startup));
            return services;
        }
    }
}
