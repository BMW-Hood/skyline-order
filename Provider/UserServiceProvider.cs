using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Extensions.Http;
namespace Providers
{
   public class UserServiceProvider
    {
        private HttpClient _client;
        public UserServiceProvider(IHttpClientFactory factory)
        {
            _client = factory.CreateClient("UserService");
        }

        public Task<string> GetAllUsers()
        {
            var response= _client.GetStringAsync("/api/health");
            return response;
        }
    }
}
