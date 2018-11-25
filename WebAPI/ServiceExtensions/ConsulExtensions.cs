using System;
using Consul;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.DependencyInjection;

namespace WebAPI.ServiceExtensions
{
    public static class ConsulExtensions
    {

        public static IApplicationBuilder UseConsul(this IApplicationBuilder app, string consulUrl)
        {

            var servicename = "skyline-webapi";
            var lifetime = app.ApplicationServices.GetRequiredService<IApplicationLifetime>();
            var  conculClient = new ConsulClient(x => x.Address = new Uri(consulUrl));//请求注册的 Consul 地址
            var httpCheck = new AgentServiceCheck()
            {
                DeregisterCriticalServiceAfter = TimeSpan.FromSeconds(5),//服务启动多久后注册
                Interval = TimeSpan.FromSeconds(10),//健康检查时间间隔，或者称为心跳间隔
                HTTP = $"http://{servicename}/api/health",//健康检查地址
                Timeout = TimeSpan.FromSeconds(20)
            };            // Register service with consul
            var registration = new AgentServiceRegistration()
            {
                Checks = new[] { httpCheck },
                ID = Guid.NewGuid().ToString(),
                Name = servicename,
                Address = servicename,
                Port = 80,
                Tags = new[] { $"urlprefix-/{servicename}" }//添加 urlprefix-/servicename 格式的 tag 标签，以便 Fabio 识别 };
            };

            conculClient.Agent.ServiceRegister(registration).Wait();//服务启动时注册，内部实现其实就是使用 Consul API 进行注册（HttpClient发起）

            lifetime.ApplicationStopping.Register(() =>
            {
                conculClient.Agent.ServiceDeregister(registration.ID).Wait();//服务停止时取消注册 }); return app;
            });

            return app;
        }
    
    }
}