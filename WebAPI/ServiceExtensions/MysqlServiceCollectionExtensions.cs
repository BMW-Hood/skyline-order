using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Pomelo.EntityFrameworkCore.MySql.Infrastructure;
using Repositories;
using System;

namespace WebAPI.ServiceExtensions
{
    public static class MysqlServiceCollectionExtensions
    {
        public static IServiceCollection AddMysql(this IServiceCollection services, string connectionString)
        {
            connectionString = "Server=192.168.31.214:13306;Database=skyline;User=root;Password=123456;";
            services.AddDbContextPool<MySqlDbContext>(
                options => options.UseMySql(connectionString, mysqlOptions =>
                     {
                         mysqlOptions.ServerVersion(new Version(5, 7, 17), ServerType.MySql); // replace with your Server Version and Type
                    }
            ));
            return services;
        }
    }
}