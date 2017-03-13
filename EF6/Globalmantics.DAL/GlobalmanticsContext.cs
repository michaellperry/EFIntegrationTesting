using Globalmantics.Domain;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity;
using System.Data.Entity.Infrastructure.Annotations;
using System.Data.Entity.ModelConfiguration.Conventions;

namespace Globalmantics.DAL
{
    public class GlobalmanticsContext : DbContext
    {
        public GlobalmanticsContext() :
            base("GlobalmanticsContext")
        { }

        public DbSet<User> Users { get; set; }
        public DbSet<Cart> Carts { get; set; }
        public DbSet<CatalogItem> CatalogItems { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            new GlobalmanticsMappingConfiguration()
                .ConfigureModelBuilder(modelBuilder);
        }
    }
}
