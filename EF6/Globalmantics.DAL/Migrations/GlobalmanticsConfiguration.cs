using Globalmantics.Domain;
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
            context.CatalogItems.AddOrUpdate(x => x.Sku, new CatalogItem
            {
                Sku = "CAFE-314",
                Description = "1 Pound Guatemalan Coffee Beans",
                UnitPrice = 18.80m
            }, new CatalogItem
            {
                Sku = "CAFE-272",
                Description = "1 Pound Ethiopian Coffee Beans",
                UnitPrice = 6.60m
            }, new CatalogItem
            {
                Sku = "DR-4142",
                Description = "Drum roasting kit",
                UnitPrice = 425.00m
            });
        }
    }
}
