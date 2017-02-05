using Globalmantics.DAL;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Globalmantics.DAL.Migrations;
using System.Configuration;
using System.IO;
using System.Reflection;

namespace Globalmantics.IntegrationTests
{
    [SetUpFixture]
    public class TestSetup
    {
        [OneTimeSetUp]
        public void InitializeDatabase()
        {
            var master = new SqlConnectionStringBuilder
            {
                DataSource = @"(LocalDB)\MSSQLLocalDB",
                InitialCatalog = "master",
                IntegratedSecurity = true
            };
            var codeBase = new Uri(Assembly.GetAssembly(typeof(GlobalmanticsContext)).CodeBase);
            var filename = Path.Combine(Path.GetDirectoryName(codeBase.AbsolutePath), "Globalmantics.mdf");

            using (var connection = new SqlConnection(master.ConnectionString))
            {
                connection.Open();
                using (var command = connection.CreateCommand())
                {
                    command.CommandText = $@"CREATE DATABASE [Globalmantics]
                        ON (NAME = 'Globalmantics',
                        FILENAME = '{filename}')";
                    command.ExecuteNonQuery();
                }
            }

            var migration = new MigrateDatabaseToLatestVersion<GlobalmanticsContext, GlobalmanticsConfiguration>();
            migration.InitializeDatabase(new GlobalmanticsContext());
        }
    }
}
