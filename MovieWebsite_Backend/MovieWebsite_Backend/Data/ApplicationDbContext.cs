using Microsoft.EntityFrameworkCore;
using MovieWebsite_Backend.Models;
using MovieWebsite_Backend.Models.Domain;

namespace MovieWebsite_Backend.Data;

public class ApplicationDbContext : DbContext
{
    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
    {
    }

    public DbSet<Genre> Genres { get; set; }
    public DbSet<Movie> Movies { get; set; }
    public DbSet<User> Users { get; set; }
    public DbSet<MovieGenre> MovieGenres { get; set; }
    public DbSet<UserMovie> UserMovies { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Genre>().HasKey(g => g.GenreId);
        modelBuilder.Entity<Movie>().HasKey(m => m.MovieId);
        modelBuilder.Entity<User>().HasKey(u => u.UserId);

        modelBuilder.Entity<MovieGenre>().HasKey(mg => new { mg.MovieId, mg.GenreId });
        modelBuilder.Entity<UserMovie>().HasKey(um => new { um.UserId, um.MovieId });

        modelBuilder.Entity<UserMovie>()
            .Property(um => um.Status)
            .HasConversion<string>();
        
        base.OnModelCreating(modelBuilder);
    }
}