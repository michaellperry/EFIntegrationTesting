using Globalmantics.DAL.Entities;
using Microsoft.EntityFrameworkCore;

namespace Globalmantics.DAL
{
    public class GlobalmanticsContext : DbContext
    {
        public GlobalmanticsContext(DbContextOptions options) :
            base(options)
        {
        }

        public DbSet<Cart> Cart { get; set; }
        public DbSet<CartLine> CartLine { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<CartLine>()
                .Property(x => x.Description)
                .HasMaxLength(50)
                .IsRequired();
        }
    }
}
