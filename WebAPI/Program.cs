using App.Metrics;
using App.Metrics.AspNetCore;
using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Hosting;
using NLog.Web;

namespace WebAPI
{
    public class Program
    {
        public static void Main(string[] args)
        {
            CreateWebHostBuilder(args).Build().Run();
        }

        public static IWebHostBuilder CreateWebHostBuilder(string[] args) =>
            WebHost.CreateDefaultBuilder(args)
                //添加 Metrics(App.Metrics)
                .ConfigureMetricsWithDefaults(builder =>
                {
                    builder.Report.ToInfluxDb("http://74.82.210.81:8086", "metricsdatabase");
                })
                .UseMetrics()
                .UseStartup<Startup>()
                //添加 Nlog
                .UseNLog();
    }
}