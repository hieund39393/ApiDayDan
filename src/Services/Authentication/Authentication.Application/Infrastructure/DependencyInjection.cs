using Microsoft.Extensions.DependencyInjection;
using System.Reflection;
using MediatR;

namespace Authentication.Application.Infrastructure
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddMediaR(this IServiceCollection services)
        {
            services.AddMediatR(Assembly.GetExecutingAssembly());
            return services;
        }
    }
}
