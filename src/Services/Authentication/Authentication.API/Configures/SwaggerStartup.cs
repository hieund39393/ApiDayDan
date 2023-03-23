using System.Collections.Generic;
using System.IO;
using Authentication.API.Configures;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.OpenApi.Models;

namespace Authentication.API.Configurations {
    public static class SwaggerConfiguration {
        public static IServiceCollection AddSwaggerModule(this IServiceCollection services)
        {
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo
                {
                    Title = "EVN EX1 - AUTHENTICATION API GATEWAY",
                    Version = "v1",
                    Description = "Swagger aides in development across the entire API lifecycle, from design and documentation, to test and deployment.",
                });
                c.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
                {
                    Description = "JWT Authorization header using the Bearer scheme. Example: \"Authorization: Bearer {token}\"",
                    Name = "Authorization",
                    In = ParameterLocation.Header,
                    Type = SecuritySchemeType.ApiKey,
                    Scheme = "Bearer"
                });
                c.AddSecurityRequirement(new OpenApiSecurityRequirement()
                {
                    {
                        new OpenApiSecurityScheme
                        {
                            Reference = new OpenApiReference
                            {
                                Type = ReferenceType.SecurityScheme,
                                Id = "Bearer"
                            },
                            In = ParameterLocation.Header,

                        },
                        new List<string>()
                    }
                });
                var xmlFile = Path.ChangeExtension(typeof(Startup).Assembly.Location, ".xml");
                c.IncludeXmlComments(xmlFile);
            });
            return services;
        }

        public static IApplicationBuilder UseAppSwagger(this IApplicationBuilder app, IConfiguration configuration)
        {
            app.UseSwagger(c =>
            {
                c.SerializeAsV2 = true;
            });

            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint("/swagger/v1/swagger.json", "EVN EX1 - AUTHENTICATION API V1");
            });
            return app;
        }
    }
}
