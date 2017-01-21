using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Globalmantics.DAL;

namespace Globalmantics.DAL.Migrations
{
    [DbContext(typeof(GlobalmanticsContext))]
    partial class GlobalmanticsContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
            modelBuilder
                .HasAnnotation("ProductVersion", "1.1.0-rtm-22752")
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("Globalmantics.DAL.Entities.Cart", b =>
                {
                    b.Property<int>("CartId")
                        .ValueGeneratedOnAdd();

                    b.Property<DateTime>("CreatedDateTime");

                    b.HasKey("CartId");

                    b.ToTable("Cart");
                });

            modelBuilder.Entity("Globalmantics.DAL.Entities.CartLine", b =>
                {
                    b.Property<int>("CartLineId")
                        .ValueGeneratedOnAdd();

                    b.Property<int>("CartId");

                    b.Property<string>("Description")
                        .IsRequired()
                        .HasMaxLength(50);

                    b.Property<decimal>("Quantity");

                    b.Property<decimal>("UnitPrice");

                    b.HasKey("CartLineId");

                    b.HasIndex("CartId");

                    b.ToTable("CartLine");
                });

            modelBuilder.Entity("Globalmantics.DAL.Entities.CartLine", b =>
                {
                    b.HasOne("Globalmantics.DAL.Entities.Cart", "Cart")
                        .WithMany("CartLines")
                        .HasForeignKey("CartId")
                        .OnDelete(DeleteBehavior.Cascade);
                });
        }
    }
}
