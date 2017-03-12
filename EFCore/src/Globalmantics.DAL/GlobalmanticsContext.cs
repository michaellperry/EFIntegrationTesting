using System;
using Globalmantics.Domain;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;
using System.Linq;

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
        public DbSet<CatalogItem> CatalogItem { get; set; }

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

        public void Seed()
        {
            CatalogItem.AddOrUpdate(x => x.Sku, new CatalogItemEqualityComparer(), Domain.CatalogItem.Create
            (
                sku: "CAFE-314",
                description: "1 Pound Guatemalan Coffee Beans",
                unitPrice: 18.80m
            ), Domain.CatalogItem.Create
            (
                sku: "CAFE-272",
                description: "1 Pound Ethiopian Coffee Beans",
                unitPrice: 6.60m
            ), Domain.CatalogItem.Create
            (
                sku: "DR-4142",
                description: "Drum roasting kit",
                unitPrice: 425.00m
            ));
        }
    }
}
