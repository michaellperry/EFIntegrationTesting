using System.Data.Entity.Migrations;

namespace Globalmantics.DAL.Migrations
{
    internal sealed class GlobalmanticsConfiguration : DbMigrationsConfiguration<GlobalmanticsContext>
    {
        public GlobalmanticsConfiguration()
        {
            AutomaticMigrationsEnabled = false;
        }

        protected override void Seed(GlobalmanticsContext context)
        {
        }
    }
}
