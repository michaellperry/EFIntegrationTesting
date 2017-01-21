using System.Data.Entity.Migrations;

namespace Globalmantics.DAL.Migrations
{
    internal sealed class Configuration : DbMigrationsConfiguration<GlobalmanticsContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = false;
        }

        protected override void Seed(GlobalmanticsContext context)
        {
        }
    }
}
