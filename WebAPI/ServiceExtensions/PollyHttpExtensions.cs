using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using Polly;
using System;

namespace WebAPI.ServiceExtensions
{
    public static class PollyHttpExtensions
    {
        public static IServiceCollection AddPollyHttpClient(this IServiceCollection services)
        {
            //创建Polly
            services.AddPolicyRegistry();



            //
            services.AddHttpClient("UserService", client =>
            {
                client.BaseAddress = new Uri("http://localhost:5000/");
                client.DefaultRequestHeaders.Add("x-btcapi-usid", "41471FB4-2014-412A-9BBA-86B25725839F");
            }).AddTransientHttpErrorPolicy(builder => builder.WaitAndRetryAsync(
                new[] {
                    TimeSpan.FromSeconds(1),
                    TimeSpan.FromSeconds(5),
                    TimeSpan.FromSeconds(10)
                }
                ));




            return services;
        }

        public static IApplicationBuilder UsePollyHttpClient(this IApplicationBuilder app, bool enableSwagger)
        {
            return app;
        }
    }
}