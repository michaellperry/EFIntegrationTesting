using Globalmantics.DAL.Entities;
using System.Data.Entity;
using System.Data.Entity.ModelConfiguration.Conventions;

namespace Globalmantics.DAL
{
    public class GlobalmanticsContext : DbContext
    {
        public DbSet<Cart> Carts { get; set; }
        public DbSet<CartLine> CartLines { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Conventions.Remove<PluralizingTableNameConvention>();

            modelBuilder.Entity<CartLine>()
                .Property(x => x.Description)
                .HasMaxLength(50)
                .IsRequired();
        }
    }
}
