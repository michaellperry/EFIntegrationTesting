using Highway.Data;
using Highway.Data.Contexts;

namespace Globalmantics.UnitTests
{
    public abstract class TestContext
    {
        public InMemoryDataContext Context { get; }
        public Repository Repository { get; }

        protected TestContext()
        {
            Context = new InMemoryDataContext();
            Repository = new Repository(Context);
        }
    }
}
