using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Globalmantics.DAL;

namespace Globalmantics.DAL.Migrations
{
    [DbContext(typeof(GlobalmanticsContext))]
    [Migration("20170226172506_AddedCartAndCatalogItems")]
    partial class AddedCartAndCatalogItems
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
            modelBuilder
                .HasAnnotation("ProductVersion", "1.1.0-rtm-22752")
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("Globalmantics.DAL.Entities.Cart", b =>
                {
                    b.Property<int>("CartId")
                        .ValueGeneratedOnAdd();

                    b.Property<DateTime>("CreatedAt");

                    b.Property<int>("UserId");

                    b.HasKey("CartId");

                    b.HasIndex("UserId");

                    b.ToTable("Cart");
                });

            modelBuilder.Entity("Globalmantics.DAL.Entities.CartItem", b =>
                {
                    b.Property<int>("CartItemId")
                        .ValueGeneratedOnAdd();

                    b.Property<int>("CartId");

                    b.Property<int>("CatalogItemId");

                    b.Property<int>("Quantity");

                    b.HasKey("CartItemId");

                    b.HasIndex("CartId");

                    b.HasIndex("CatalogItemId");

                    b.ToTable("CartItem");
                });

            modelBuilder.Entity("Globalmantics.DAL.Entities.CatalogItem", b =>
                {
                    b.Property<int>("CatalogItemId")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("Description")
                        .IsRequired()
                        .HasMaxLength(100);

                    b.Property<string>("Sku")
                        .IsRequired()
                        .HasMaxLength(20);

                    b.Property<decimal>("UnitPrice")
                        .HasColumnType("decimal(18,2)");

                    b.HasKey("CatalogItemId");

                    b.HasIndex("Sku")
                        .IsUnique();

                    b.ToTable("CatalogItem");
                });

            modelBuilder.Entity("Globalmantics.DAL.Entities.User", b =>
                {
                    b.Property<int>("UserId")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("Email")
                        .IsRequired()
                        .HasMaxLength(100);

                    b.HasKey("UserId");

                    b.HasIndex("Email")
                        .IsUnique();

                    b.ToTable("User");
                });

            modelBuilder.Entity("Globalmantics.DAL.Entities.Cart", b =>
                {
                    b.HasOne("Globalmantics.DAL.Entities.User", "User")
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("Globalmantics.DAL.Entities.CartItem", b =>
                {
                    b.HasOne("Globalmantics.DAL.Entities.Cart", "Cart")
                        .WithMany("CartItems")
                        .HasForeignKey("CartId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("Globalmantics.DAL.Entities.CatalogItem", "CatalogItem")
                        .WithMany()
                        .HasForeignKey("CatalogItemId");
                });
        }
    }
}
