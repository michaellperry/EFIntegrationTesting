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

        public DbSet<User> User { get; set; }
        //public DbSet<CartLine> CartLine { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<User>()
                .Property(x => x.Email)
                .HasMaxLength(100)
                .IsRequired();
            modelBuilder.Entity<User>()
                .HasIndex(x => x.Email)
                .IsUnique();

            //modelBuilder.Entity<CartLine>()
            //    .Property(x => x.Description)
            //    .HasMaxLength(50)
            //    .IsRequired();
        }
    }
}
