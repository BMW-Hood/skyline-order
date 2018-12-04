using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace UnitTest.Controllers
{
   public class PaymentControllerTest:ControllerTestBase
    {
        public PaymentControllerTest(StartupFixture fixture) : base(fixture)
        {

        }
        [Fact(DisplayName = "获取产品列表")]
        public async Task TestProductionList()
        {
            var response = await Client.GetAsync("api/paymnet");
            Assert.True(response.IsSuccessStatusCode);
        }


    }
}
