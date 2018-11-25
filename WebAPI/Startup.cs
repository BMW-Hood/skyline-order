using App.Metrics;
using Common;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

using NLog.Extensions.Logging;
using NLog.Web;
using Repositories;
using Repositories.Impl;
using Services;
using Swashbuckle.AspNetCore.Swagger;
using System.Collections.Generic;
using System.Linq;
using WebAPI.Middlewares;
using WebAPI.ServiceExtensions;

namespace WebAPI
{
    public class Startup
    {
        public IConfiguration Configuration { get; }
        private string connectionString;
        private string tracingCollectorString;
        private string influxdb_Host;
        private string influxdb_Database;
        private string consul_url;

        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
            connectionString = EnvironmentHelper.GetEnvironmentVariable(configuration.GetConnectionString("Skyline"));
            tracingCollectorString = EnvironmentHelper.GetEnvironmentVariable(configuration.GetSection("Tracing").GetValue<string>("JaegerCollector"));
            influxdb_Host = EnvironmentHelper.GetEnvironmentVariable(configuration.GetSection("InfluxDb").GetValue<string>("Url"));
            influxdb_Database = EnvironmentHelper.GetEnvironmentVariable(configuration.GetSection("InfluxDb").GetValue<string>("DataBase"));
            consul_url = EnvironmentHelper.GetEnvironmentVariable(configuration.GetSection("Consul").GetValue<string>("Url"));
        }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            //注册配置文件
            services.AddSingleton<IAppSettings, AppSettings>();
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new Info {
                    Title = "Skyline API",
                    Version = "v1"          
                });
                c.AddSecurityDefinition("UsId", new ApiKeyScheme { In = "header", Description = "Please enter UsId", Name = "x-btcapi-usid", Type = "apiKey" });
                c.AddSecurityRequirement(new Dictionary<string, IEnumerable<string>> {
                { "UsId", Enumerable.Empty<string>() },
            });

            });
            services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_2_1);
            services.AddAutoMapper();



            //注册MySql
            services.AddMysql(connectionString);

            //注册 Tracing(跟踪)
            services.AddJaegerTracing(tracingCollectorString);

            //注册metrics(监控)
            services.AddMetrics();

            //注册Repository
            services.AddScoped<IDatabaseFactory, DatabaseFactory>();
            services.AddScoped<IUnitOfWork, UnitOfWork>();
            services.AddScoped<IUserRepository, UserRepository>();
            services.AddScoped<IPaymentRepository, PaymentRepository>();
            //注册Proxy

            //注册Service
            services.AddScoped<IUserService, UserService>();
            services.AddScoped<IPaymentService, PaymentService>();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env, IAppSettings settings)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseHsts();
            }

            //logger日志
            env.ConfigureNLog("nlog.config");

            app.UseStaticFiles();
            app.UseSwagger();

            // Enable middleware to serve swagger-ui (HTML, JS, CSS, etc.), 
            // specifying the Swagger JSON endpoint.
            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint("/swagger/v1/swagger.json", "Skyline API v1");
                c.RoutePrefix = "swagger";
            });
            app.UseMvc();
            app.UseJaegerTracing();
            app.UseMySql(connectionString);
            app.UseConsul(consul_url);
        }
    }
}