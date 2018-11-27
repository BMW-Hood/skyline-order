using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Common;
using Microsoft.Extensions.Http;
using Polly;
using Polly.Registry;

namespace Providers
{
    public class UserServiceProvider
    {
        private IHttpClientFactory _factory;

        private readonly IReadOnlyPolicyRegistry<string> _policyRegistry;
        public UserServiceProvider(IHttpClientFactory factory, IReadOnlyPolicyRegistry<string> policyRegistry)
        {
            _factory = factory;
        }

        public Task<string> GetAllUsers()
        {
            using (var client = _factory.CreateClient("UserService"))
            {
                var response = client.GetStringAsync("/api/health");
                return response;
            }

        }

        public async Task<string> GetUsersPage()
        {
            var policy = _policyRegistry.Get<IAsyncPolicy<HttpResponseMessage>>(PolicyName.RetryPolicy) ?? Policy.NoOpAsync<HttpResponseMessage>();
            using (var client = _factory.CreateClient())
            {
                var context = new Context();
                var response = await policy.ExecuteAsync(ctx => client.GetAsync(""),context);
                return response.IsSuccessStatusCode ? await response.Content.ReadAsStringAsync() : "error";
            }
        }



    }
}
