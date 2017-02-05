using Globalmantics.DAL;
using Globalmantics.DAL.Migrations;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.SqlClient;
using System.IO;
using System.Linq;
using System.Reflection;

namespace Globalmantics.IntegrationTests
{
    [SetUpFixture]
    public class TestSetup
    {
        [OneTimeSetUp]
        public void InitializeDatabase()
        {
            Destroy();
            Create();
        }

        [OneTimeTearDown]
        public void TearDownDatabase()
        {
            Destroy();
        }

        private static void Create()
        {
            ExecuteSqlCommand(Master, $@"
                CREATE DATABASE [Globalmantics]
                ON (NAME = 'Globalmantics',
                FILENAME = '{FileName}')");

            var migration = new MigrateDatabaseToLatestVersion<GlobalmanticsContext, GlobalmanticsConfiguration>();
            migration.InitializeDatabase(new GlobalmanticsContext());
        }

        private static void Destroy()
        {
            var fileNames = ExecuteSqlQuery(Master, @"
                SELECT [physical_name] FROM [sys].[master_files]
                WHERE [database_id] = DB_ID('Globalmantics')",
                row => (string)row["physical_name"]);

            if (fileNames.Any())
            {
                ExecuteSqlCommand(Master, @"
                EXEC sp_detach_db 'Globalmantics'");

                fileNames.ForEach(File.Delete);
            }
        }

        private static void ExecuteSqlCommand(SqlConnectionStringBuilder connectionStringBuilder, string commandText)
        {
            using (var connection = new SqlConnection(connectionStringBuilder.ConnectionString))
            {
                connection.Open();
                using (var command = connection.CreateCommand())
                {
                    command.CommandText = commandText;
                    command.ExecuteNonQuery();
                }
            }
        }

        private static List<T> ExecuteSqlQuery<T>(SqlConnectionStringBuilder connectionStringBuilder, string queryText, Func<SqlDataReader, T> read)
        {
            var result = new List<T>();
            using (var connection = new SqlConnection(connectionStringBuilder.ConnectionString))
            {
                connection.Open();
                using (var command = connection.CreateCommand())
                {
                    command.CommandText = queryText;
                    using (var reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            result.Add(read(reader));
                        }
                    }
                }
            }
            return result;
        }

        private static SqlConnectionStringBuilder Master =>
            new SqlConnectionStringBuilder
            {
                DataSource = @"(LocalDB)\MSSQLLocalDB",
                InitialCatalog = "master",
                IntegratedSecurity = true
            };

        private static string FileName => Path.Combine(
            Path.GetDirectoryName(
                Assembly.GetExecutingAssembly().Location),
            "Globalmantics.mdf");
    }
}
