using App.Metrics;
using App.Metrics.AspNetCore;
using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Hosting;
using NLog.Web;
using System;

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
                    var url = Environment.GetEnvironmentVariable("INFLUXDB_URL");
                    var db = Environment.GetEnvironmentVariable("INFLUXDB_DATABASE");
                    builder.Report.ToInfluxDb(url, db);
                })
                .UseMetrics()
                .UseStartup<Startup>()
                //添加 Nlog
                .UseNLog();
    }
}