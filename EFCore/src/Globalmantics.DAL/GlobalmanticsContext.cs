using System;
using Globalmantics.DAL.Entities;
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
            CatalogItem.AddOrUpdate(x => x.Sku, new CatalogItemEqualityComparer(), new CatalogItem
            {
                Sku = "CAFE-314",
                Description = "1 Pound Guatemalan Coffee Beans",
                UnitPrice = 18.80m
            }, new CatalogItem
            {
                Sku = "CAFE-272",
                Description = "1 Pound Etheopian Coffee Beans",
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
