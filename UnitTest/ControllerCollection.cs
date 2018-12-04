using System;
using System.Collections.Generic;
using System.Text;
using Xunit;

namespace UnitTest
{

    [CollectionDefinition("ControllerCollection")]
    public class ControllerCollection : ICollectionFixture<StartupFixture>
    {

    }

}
