using AutoMapper;
using Common;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Repositories;
using Repositories.Impl;
using Services;
using WebAPI.Middlewares;
using WebAPI.ServiceExtensions;


namespace WebAPI
{
    public class Startup
    {
        public IConfiguration Configuration { get; }
        private string connectionString;
        private string tracingCollectorString;
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
            connectionString = configuration.GetConnectionString("Skyline");
            tracingCollectorString = configuration.GetSection("Tracing").GetValue<string>("JaegerCollector");

        }




        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            //注册配置文件
            services.AddSingleton<IAppSettings, AppSettings>();

            services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_2_1);

            services.AddAutoMapper();

            //注册MySql
            services.AddMysql(connectionString);

            //注册 Tracing(跟踪)
            services.AddJaegerTracing(tracingCollectorString);

            //注册metrics(监控)
            services.AddMetrics();

            //注册 Logging(日志)
            

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

            app.UseJaegerTracing();
            app.UseMvc();
            app.UseMySql(connectionString);

        }
    }
}