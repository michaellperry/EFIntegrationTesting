using Globalmantics.DAL.Entities;
using Microsoft.EntityFrameworkCore;

namespace Globalmantics.DAL
{
    public class GlobalmanticsContext : DbContext
    {
        public DbSet<Cart> Cart { get; set; }
        public DbSet<CartLine> CartLine { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer("GolbalmanticsContext");
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<CartLine>()
                .Property(x => x.Description)
                .HasMaxLength(50)
                .IsRequired();
        }
    }
}
