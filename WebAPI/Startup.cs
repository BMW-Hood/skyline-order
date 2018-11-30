﻿using AspectCore.Extensions.DependencyInjection;
using Common;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Cors.Internal;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using NLog.Extensions.Logging;
using NLog.Web;
using Rabbit.Extensions.Configuration;
using Repositories;
using Repositories.Impl;
using Services;
using System;
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
        private RegistyOption consul_option;

        public Startup(IConfiguration configuration)
        {
            Configuration = configuration.EnableTemplateSupport();
            connectionString = configuration.GetConnectionString("Skyline");
            tracingCollectorString = configuration.GetSection("Tracing").GetValue<string>("JaegerCollector");
            influxdb_Host = configuration.GetSection("InfluxDb").GetValue<string>("Url");
            influxdb_Database = configuration.GetSection("InfluxDb").GetValue<string>("DataBase");
            consul_option = configuration.GetSection("Consul").Get<RegistyOption>();
        }

        // This method gets called by the runtime. Use this method to add services to the container.
        public IServiceProvider ConfigureServices(IServiceCollection services)
        {
            //注册配置文件
            services.AddSingleton<IAppSettings, AppSettings>();
            //跨域访问
            services.AddCors(options=>
                options.AddPolicy("AllowAnyOrigins",builder=>{
                    builder.AllowAnyOrigin().AllowAnyHeader();
                })
            );
            services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_2_1);
            services.Configure<MvcOptions>(options =>
            {
                options.Filters.Add(new CorsAuthorizationFilterFactory("AllowAnyOrigins"));
            });


            services.AddAutoMapper();
            services.AddSwagger();
            //注册MySql
            services.AddMysql(connectionString);

            //注册metrics(监控)
            services.AddMetrics();

            //注册 Polly
            services.AddPollyHttpClient();

            //注册Repository
            services.AddScoped<IDatabaseFactory, DatabaseFactory>();
            services.AddScoped<IUnitOfWork, UnitOfWork>();
            services.AddScoped<IUserRepository, UserRepository>();
            services.AddScoped<IPaymentRepository, PaymentRepository>();
            //注册Proxy

            //注册Service
            services.AddScoped<IUserService, UserService>();
            services.AddScoped<IPaymentService, PaymentService>();

            //注册 Tracing(跟踪)
            services.AddJaegerTracing(tracingCollectorString);
            return services.BuildAspectInjectorProvider();
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
            app.UseSwaggerConfig(settings.EnableSwaggerDocument);
            app.UseJaegerTracing();
            app.UseCors("AllowAnyOrigins");        
            app.UseMvc();
            app.UseMySql(connectionString);
            app.UseConsul(consul_option);

        }
    }
}