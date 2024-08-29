using Microsoft.EntityFrameworkCore;
using MovieWebsite_Backend.Data.Repositories.Interfaces;
using MovieWebsite_Backend.Models;
using MovieWebsite_Backend.Models.Domain;

namespace MovieWebsite_Backend.Data.Repositories;

public class MovieRepository : IMovieRepository
{
    private readonly ApplicationDbContext _context;

    public MovieRepository(ApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<IEnumerable<Movie>> GetAllMoviesAsync()
    {
        return await _context.Movies.ToListAsync();
    }

    public async Task<Movie> GetMovieById(int movieId)
    {
        return await _context.Movies.FirstOrDefaultAsync(m => m.MovieId == movieId);
    }

    public async Task<IEnumerable<Movie>> GetAllMoviesByGenreAsync(string genre)
    {
        return await _context.Movies
            .Where(m => m.MovieGenres.Any(mg => mg.Genre.Name.ToLower() == genre.ToLower()))
            .Include(m => m.MovieGenres)
            .ThenInclude(mg => mg.Genre)
            .ToListAsync();
    }

    public async Task<Movie> AddAsync(Movie movie)
    {
        await _context.Movies.AddAsync(movie);
        await _context.SaveChangesAsync();
        return movie;
    }

    public async Task AddGenreToMovieAsync(int movieId, int genreId)
    {
        var movieGenre = new MovieGenre
        {
            MovieId = movieId,
            GenreId = genreId
        };

        await _context.MovieGenres.AddAsync(movieGenre);
        await _context.SaveChangesAsync();
    }

    public async Task<bool> ExistsByExternalApiIdAsync(string externalApiId)
    {
        return await _context.Movies.AnyAsync(m => m.ExternalApiId == externalApiId);
    }
}