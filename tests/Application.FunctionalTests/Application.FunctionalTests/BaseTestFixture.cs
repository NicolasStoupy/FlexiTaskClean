using System;
using System.Collections.Generic;
using System.Text;

namespace Application.FunctionalTests
{
    [TestFixture]
    public abstract class BaseTestFixture
    {
        [SetUp]
        public async Task TestSetUp()
        {
            await Testing.ResetState();
        }
    }
}
