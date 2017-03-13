using Highway.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.Entity;
using System.Data.Entity.ModelConfiguration.Conventions;
using Globalmantics.Domain;
using System.Data.Entity.Infrastructure.Annotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Globalmantics.DAL
{
    public class GlobalmanticsMappingConfiguration : IMappingConfiguration
    {
        public void ConfigureModelBuilder(DbModelBuilder modelBuilder)
        {
            modelBuilder.Conventions.Remove<PluralizingTableNameConvention>();

            modelBuilder.Entity<User>()
                .Property(x => x.Email)
                .HasMaxLength(100)
                .IsRequired()
                .HasColumnAnnotation(IndexAnnotation.AnnotationName, new IndexAnnotation(
                    new IndexAttribute("IX_U_Email") { IsUnique = true }));

            modelBuilder.Entity<CatalogItem>()
                .Property(x => x.Description)
                .HasMaxLength(100)
                .IsRequired();
            modelBuilder.Entity<CatalogItem>()
                .Property(x => x.UnitPrice)
                .HasPrecision(18, 2);
            modelBuilder.Entity<CatalogItem>()
                .Property(x => x.Sku)
                .HasMaxLength(20)
                .IsRequired()
                .HasColumnAnnotation(IndexAnnotation.AnnotationName, new IndexAnnotation(
                    new IndexAttribute("IX_U_Sku") { IsUnique = true }));

            modelBuilder.Entity<CartItem>()
                .HasRequired(x => x.CatalogItem)
                .WithMany()
                .WillCascadeOnDelete(false);
        }
    }
}
