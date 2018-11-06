using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Repositories;
using WebAPI.Config;
using WebAPI.ServiceExtensions;

namespace WebAPI
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }
        private AppSettings _appSettings;

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            //注册配置文件
            services.Configure<AppSettings>(Configuration.GetSection("AppSettings"));
            _appSettings = Configuration.GetSection("AppSettings").Get<AppSettings>();

            //注册插件
            services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_2_1);
            services.AddOpenTracing();
            services.AddJaeger("http://192.168.31.214:14268/api/traces");
            services.AddMysql("Server=192.168.31.214:13306;Database=skyline;User=root;Password=123456;");

            //注册Repository
            services.AddScoped<IUserRepository, UserRepository>();

            //注册Proxy

            //注册Service
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseHsts();
            }

            app.UseHttpsRedirection();
            app.UseMvc();
        }
    }
}