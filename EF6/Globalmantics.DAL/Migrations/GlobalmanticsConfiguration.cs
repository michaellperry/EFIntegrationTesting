using System.Data.Entity.Migrations;

namespace Globalmantics.DAL.Migrations
{
    public sealed class GlobalmanticsConfiguration : DbMigrationsConfiguration<GlobalmanticsContext>
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
