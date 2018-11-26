﻿using Microsoft.Extensions.Configuration;

namespace Common
{
    public interface IAppSettings
    {
        string ConnectionString { get; }
        bool EnableSwaggerDocument { get; }
    }

    public class AppSettings : IAppSettings
    {
        private const string DB = "Skyline";
        private IConfiguration _configuration;

        public AppSettings(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public string ConnectionString => _configuration.GetConnectionString(DB);
        public bool EnableSwaggerDocument => _configuration.GetValue<bool>("EnableSwaggerDocument");
    }
}