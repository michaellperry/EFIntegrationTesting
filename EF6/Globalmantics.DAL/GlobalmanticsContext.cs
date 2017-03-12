using Globalmantics.Domain;
using System.Data.Entity;

namespace Globalmantics.DAL
{
    public class GlobalmanticsContext : DbContext
    {
        public GlobalmanticsContext() :
            base("GlobalmanticsContext")
        { }

        public DbSet<CatalogItem> CatalogItems { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            var mappingConfiguration = new GlobalmanticsMappingConfiguration();
            mappingConfiguration.ConfigureModelBuilder(modelBuilder);
        }
    }
}
