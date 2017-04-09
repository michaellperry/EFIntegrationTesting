using Globalmantics.DAL;
using Globalmantics.Logic;
using Highway.Data;

namespace Globalmantics.IntegrationTests
{
    public class TestContext
    {
        public DataContext DataContext { get; }
        public IRepository Repository { get; }
        public ILog Log { get; }

        protected TestContext()
        {
            var configuration = new GlobalmanticsMappingConfiguration();

            DataContext = new DataContext("GlobalmanticsContext", configuration);
            Repository = new Repository(DataContext);
            Log = new MockLog();
        }
    }
}
