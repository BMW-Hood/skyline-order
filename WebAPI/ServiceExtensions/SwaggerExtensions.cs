using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using Swashbuckle.AspNetCore.Swagger;
using System.Collections.Generic;
using System.Linq;

namespace WebAPI.ServiceExtensions
{
    public static class SwaggerExtensions
    {
        public static IServiceCollection AddSwagger(this IServiceCollection services)
        {
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new Info
                {
                    Title = "Skyline API",
                    Version = "v1"
                });

                c.AddSecurityDefinition("UsId", new ApiKeyScheme { In = "header", Description = "Please enter UsId", Name = "x-btcapi-usid", Type = "apiKey" });
                c.AddSecurityRequirement(new Dictionary<string, IEnumerable<string>> {
                { "UsId", Enumerable.Empty<string>() }, });

            });
            return services;
        }

        public static IApplicationBuilder UseSwaggerConfig(this IApplicationBuilder app, bool enableSwagger)
        {
            if (enableSwagger)
            {
                app.UseStaticFiles();
                app.UseSwagger();
                // Enable middleware to serve swagger-ui (HTML, JS, CSS, etc.),
                // specifying the Swagger JSON endpoint.
                app.UseSwaggerUI(c =>
                {
                    c.SwaggerEndpoint("/swagger/v1/swagger.json", "Skyline API v1");
                    c.RoutePrefix = "swagger";
                });

            }
            return app;

        }
    }
}