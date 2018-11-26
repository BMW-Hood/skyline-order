using Polly;
using Polly.Registry;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;

namespace WebAPI.Policies
{
    public static class PolicyRegistryExtensions
    {

        public static IPolicyRegistry<string> AddBasicRetryPolicy(this IPolicyRegistry<string> policyRegistry)
        {
            var PolicyName = PolicyNames.RetryPolicy;
            var retryPolicy = Policy
                .Handle<Exception>()
                .OrResult<HttpResponseMessage>(r => !r.IsSuccessStatusCode)
                .WaitAndRetryAsync(3, retryCount => TimeSpan.FromSeconds(10), (result, timeSpan, retryCount, context) =>
                {
                    if (result.Exception != null)
                    {
                       
                    }
                    else
                    {
                      
                            
                    }
                }).WithPolicyKey(PolicyName);
                

            policyRegistry.Add(PolicyName, retryPolicy);

            return policyRegistry;
        }

    }
}
