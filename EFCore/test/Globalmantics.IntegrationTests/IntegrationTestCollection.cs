using System;
using System.Collections.Generic;
using System.Text;
using Xunit;

namespace Globalmantics.IntegrationTests
{
    [CollectionDefinition("Integration test collection")]
    public class IntegrationTestCollection : ICollectionFixture<TestSetup>
    {
    }
}
