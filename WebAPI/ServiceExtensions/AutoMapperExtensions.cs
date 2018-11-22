using AutoMapper;
using Contracts;
using Microsoft.Extensions.DependencyInjection;

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