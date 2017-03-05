using Globalmantics.DAL;
using Microsoft.EntityFrameworkCore;

namespace Globalmantics.UnitTests
{
    public class UnitTestBase
    {
        protected static GlobalmanticsContext GivenGlobalmanticsContext()
        {
            var options = new DbContextOptionsBuilder()
                .UseInMemoryDatabase()
                .Options;
            var context = new GlobalmanticsContext(options);
            return context;
        }
    }
}
