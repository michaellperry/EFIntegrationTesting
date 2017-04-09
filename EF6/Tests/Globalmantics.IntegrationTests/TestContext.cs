using Globalmantics.DAL;
using Highway.Data;
using System;

namespace Globalmantics.IntegrationTests
{
    public abstract class TestContext
    {
        public DataContext Context { get; }
        public Repository Repository { get; }

        protected TestContext()
        {
            var configuration = new GlobalmanticsMappingConfiguration();
            Context = new DataContext("GlobalmanticsContext", configuration);
            Repository = new Repository(Context);
        }
    }
}
