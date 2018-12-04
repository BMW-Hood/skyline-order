using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Hosting.Server.Features;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.NetworkInformation;
using System.Text;
using WebAPI;

namespace UnitTest
{
   public class StartupFixture: IDisposable
    {

        private readonly IWebHost _server;
        public IServiceProvider Services { get; }

        public HttpClient Client { get; }

        public string ServiceBaseUrl { get; }

        public StartupFixture()
        {
            var builder = WebHost.CreateDefaultBuilder()
                .UseUrls($"http://localhost:{GetRandomPort()}")
                .UseStartup<Startup>();

            _server = builder.Build();
            _server.Start();

            var url = _server.ServerFeatures.Get<IServerAddressesFeature>().Addresses.First();
            Services = _server.Services;
            ServiceBaseUrl = $"{url}/api/";

            Client = GetHttpClient();

            Initialize();
        }


        private HttpClient GetHttpClient()
        {
            var client = new HttpClient() { BaseAddress = new Uri(ServiceBaseUrl) };
            client.DefaultRequestHeaders.Add("x-btcapi-usid", "7c71acfc-9812-4b2b-a394-24548aa433dc");
            return client;

        }

        /// <summary>
        /// TestDataInitialize
        /// </summary>
        private void Initialize()
        {
            // ...
        }

        public void Dispose()
        {
            Client.Dispose();
            _server.Dispose();
        }

        private static readonly Random Random = new Random();

        private static int GetRandomPort()
        {
            var activePorts = IPGlobalProperties.GetIPGlobalProperties().GetActiveTcpListeners().Select(_ => _.Port).ToList();

            var randomPort = Random.Next(10000, 65535);

            while (activePorts.Contains(randomPort))
            {
                randomPort = Random.Next(10000, 65535);
            }

            return randomPort;
        }


    }
}
