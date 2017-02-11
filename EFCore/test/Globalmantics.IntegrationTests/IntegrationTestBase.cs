using Globalmantics.DAL;
using Microsoft.EntityFrameworkCore;
using System.Data.SqlClient;

namespace Globalmantics.IntegrationTests
{
    public class IntegrationTestBase
    {
        protected static GlobalmanticsContext GivenGlobalmanticsContext()
        {
            return new GlobalmanticsContext(new DbContextOptionsBuilder()
                .UseSqlServer(Globalmantics.ConnectionString)
                .Options);
        }

        private static SqlConnectionStringBuilder Globalmantics =>
            new SqlConnectionStringBuilder
            {
                DataSource = @"(LocalDB)\MSSQLLocalDB",
                InitialCatalog = "GlobalmanticsCore",
                IntegratedSecurity = true
            };
    }
}