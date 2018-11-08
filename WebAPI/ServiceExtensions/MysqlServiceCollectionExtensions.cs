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