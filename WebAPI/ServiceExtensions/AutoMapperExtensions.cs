using AutoMapper;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Contracts;
namespace WebAPI.ServiceExtensions
{
    public static class AutoMapperServiceExtensions
    {
        public static IServiceCollection AddAutoMapper(this IServiceCollection services)
        {
            var _mapperConfiguration = new MapperConfiguration(cfg =>
             {
                 cfg.AddProfile(new AutoMapperProfile());
             });
            services.AddSingleton<IMapper>(sp => _mapperConfiguration.CreateMapper());
            return services;
        }
    }
}
