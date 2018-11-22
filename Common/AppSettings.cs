using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Extensions.Configuration.EnvironmentVariables;
namespace Common
{
    public interface IAppSettings
    {
        string ConnectionString { get; }
    }
    public class AppSettings : IAppSettings
    {
        private const string DB = "Skyline";
        private IConfiguration _configuration;
        public AppSettings(IConfiguration configuration)
        {
            _configuration = configuration;
        }
        public string ConnectionString => EnvironmentHelper.GetEnvironmentVariable(_configuration.GetConnectionString(DB));
    }

}
