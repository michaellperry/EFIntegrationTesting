using Globalmantics.DAL.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

namespace Globalmantics.DAL
{
    public class GlobalmanticsContext : DbContext
    {
        public GlobalmanticsContext(DbContextOptions options) :
            base(options)
        {
        }

        public DbSet<User> User { get; set; }
        public DbSet<Cart> Cart { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<User>()
                .Property(x => x.Email)
                .HasMaxLength(100)
                .IsRequired();
            modelBuilder.Entity<User>()
                .HasIndex(x => x.Email)
                .IsUnique();

            modelBuilder.Entity<CatalogItem>()
                .Property(x => x.Description)
                .HasMaxLength(100)
                .IsRequired();
            modelBuilder.Entity<CatalogItem>()
                .Property(x => x.UnitPrice)
                .HasColumnType("decimal(18,2)");
            modelBuilder.Entity<CatalogItem>()
                .Property(x => x.Sku)
                .HasMaxLength(20)
                .IsRequired();
            modelBuilder.Entity<CatalogItem>()
                .HasIndex(x => x.Sku)
                .IsUnique();

            modelBuilder.Entity<CartItem>()
                .HasOne(x => x.CatalogItem)
                .WithMany()
                .OnDelete(DeleteBehavior.Restrict);
        }
    }
}
