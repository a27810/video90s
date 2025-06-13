using Microsoft.EntityFrameworkCore;
using Video90s.API.Models;

namespace Video90s.API.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions opts) : base(opts) { }

        public DbSet<User> Users { get; set; }
        public DbSet<Movie> Movies { get; set; }
        public DbSet<Rental> Rentals { get; set; }

        protected override void OnModelCreating(ModelBuilder mb)
        {
            mb.Entity<Rental>()
              .HasOne(r => r.User)
              .WithMany()
              .HasForeignKey(r => r.UserId);

            mb.Entity<Rental>()
              .HasOne(r => r.Movie)
              .WithMany()
              .HasForeignKey(r => r.MovieId);
        }
    }
}
