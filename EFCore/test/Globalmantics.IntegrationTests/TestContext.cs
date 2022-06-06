using Globalmantics.DAL;
using System.Data.SqlClient;
using Microsoft.EntityFrameworkCore;

namespace Globalmantics.IntegrationTests
{
    public abstract class TestContext
    {
        public GlobalmanticsContext Context { get; }

        protected TestContext()
        {
            Context = GivenGlobalmanticsContext();
        }

        protected static GlobalmanticsContext GivenGlobalmanticsContext(bool beginTransaction = true)
        {
            var context = new GlobalmanticsContext(new DbContextOptionsBuilder()
                .UseSqlServer(Globalmantics.ConnectionString)
                .Options);
            if (beginTransaction)
                context.Database.BeginTransaction();
            return context;
        }

        private static SqlConnectionStringBuilder Globalmantics =>
            new SqlConnectionStringBuilder
            {
                DataSource = @"(LocalDB)\MSSQLLocalDB",
                InitialCatalog = "Globalmantics",
                IntegratedSecurity = true
            };
    }
}
