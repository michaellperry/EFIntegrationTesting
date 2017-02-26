using Globalmantics.DAL;
using Microsoft.EntityFrameworkCore;
using System.Data.SqlClient;

namespace Globalmantics.IntegrationTests
{
    public class IntegrationTestBase
    {
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