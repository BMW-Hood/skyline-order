using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using Xunit;

namespace UnitTest.Controllers
{
    [Collection("ControllerCollection")]
    public class ControllerTestBase
    {
        protected readonly HttpClient Client;
        protected readonly IServiceProvider ServiceProvider;

        public ControllerTestBase(StartupFixture fixture)
        {
            Client = fixture.Client;
            ServiceProvider = fixture.Services;
        }

    }
}
