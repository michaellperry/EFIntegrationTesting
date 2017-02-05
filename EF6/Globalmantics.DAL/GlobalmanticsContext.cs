using Globalmantics.DAL.Entities;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity;
using System.Data.Entity.Infrastructure.Annotations;
using System.Data.Entity.ModelConfiguration.Conventions;

namespace Globalmantics.DAL
{
    internal class GlobalmanticsContext : DbContext
    {
        public GlobalmanticsContext() :
            base("GlobalmanticsContext")
        { }

        public DbSet<User> Users { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Conventions.Remove<PluralizingTableNameConvention>();

            modelBuilder.Entity<User>()
                .Property(x => x.Email)
                .HasMaxLength(100)
                .IsRequired()
                .HasColumnAnnotation(IndexAnnotation.AnnotationName, new IndexAnnotation(
                    new IndexAttribute("IX_U_Email") { IsUnique = true }));
        }
    }
}
