using Jaeger;
using Jaeger.Samplers;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using OpenTracing;
using OpenTracing.Contrib.NetCore.CoreFx;
using OpenTracing.Util;
using System;

namespace WebAPI.ServiceExtensions
{
    public static class JaegerServiceCollectionExtensions
    {
        public static IServiceCollection AddJaeger(this IServiceCollection services, string jaegerUrl)
        {
            if (services == null)
            {
                throw new ArgumentNullException(nameof(services));
            }
            var jaegerUri = new Uri(jaegerUrl);

            services.AddSingleton<ITracer>(serviceProvider =>
            {
                string serviceName = serviceProvider.GetRequiredService<IHostingEnvironment>().ApplicationName;
                ILoggerFactory loggerFactory = serviceProvider.GetRequiredService<ILoggerFactory>();

                ISampler sampler = new ConstSampler(sample: true);
                ITracer tracer = new Tracer.Builder(serviceName)
                    .WithLoggerFactory(loggerFactory)
                    .WithSampler(sampler)
                    .Build();

                GlobalTracer.Register(tracer);

                return tracer;
            });

            // Prevent endless loops when OpenTracing is tracking HTTP requests to Jaeger.
            services.Configure<HttpHandlerDiagnosticOptions>(options =>
            {
                options.IgnorePatterns.Add(request => jaegerUri.IsBaseOf(request.RequestUri));
            });

            return services;
        }
    }
}