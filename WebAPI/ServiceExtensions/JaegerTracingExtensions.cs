using Jaeger;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using OpenTracing;

namespace WebAPI.ServiceExtensions
{
    public static class JaegerTracingExtensions
    {
        public static IServiceCollection AddJaegerTracing(this IServiceCollection services, string collectionStr)
        {
            services.AddSingleton<ITracer>(serviceProvider =>
            {
                ILoggerFactory loggerFactory = serviceProvider.GetRequiredService<ILoggerFactory>().AddConsole(minLevel: LogLevel.Information).AddDebug();
                string serviceName = serviceProvider.GetRequiredService<IHostingEnvironment>().ApplicationName;
                var tracer = GetTracer(serviceName, loggerFactory, collectionStr);
                return tracer;
            });
            return services;
        }

        private static ITracer GetTracer(string serviceName, ILoggerFactory loggerFactory, string collectorAddress)
        {     
            Configuration.SamplerConfiguration samplerConfiguration = new Configuration.SamplerConfiguration(loggerFactory)
                .WithParam(1)
                .WithType("const");
            Configuration.SenderConfiguration senderConfig = new Configuration.SenderConfiguration(loggerFactory)
                .WithEndpoint(collectorAddress);
            Configuration.ReporterConfiguration reporterConfiguration = new Configuration.ReporterConfiguration(loggerFactory)
                .WithSender(senderConfig);
            Configuration configuration = new Configuration(serviceName, loggerFactory)
                .WithReporter(reporterConfiguration)
                .WithSampler(samplerConfiguration);
            var tracer = configuration.GetTracerBuilder().WithTraceId128Bit().WithLoggerFactory(loggerFactory).Build();
            return tracer;
        }
    }
}