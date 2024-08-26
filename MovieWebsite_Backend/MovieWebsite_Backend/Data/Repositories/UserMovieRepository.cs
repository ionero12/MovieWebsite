using Microsoft.EntityFrameworkCore;
using MovieWebsite_Backend.Models;

namespace MovieWebsite_Backend.Data.Repositories;

public class UserMovieRepository : IUserMovieRepository
{
    private readonly ApplicationDbContext _context;

    public UserMovieRepository(ApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<UserMovie> GetUserMovieByIdsAndStatusAsync(int userId, int movieId, Status status)
    {
        var userMovie = await _context.UserMovies
            .FirstOrDefaultAsync(uml => uml.UserId == userId && uml.MovieId == movieId && uml.Status == status);

        return userMovie;
    }
    
    public async Task<UserMovie> GetUserMovieByIdsAsync(int userId, int movieId)
    {
        return await _context.UserMovies.FirstOrDefaultAsync(uml => uml.UserId == userId && uml.MovieId == movieId);
    }
    
    public async Task<IEnumerable<Movie>> GetUserListAsync(int userId, Status status)
    {
        return await _context.UserMovies
            .Where(uml => uml.UserId == userId && uml.Status == status)
            .Select(uml => uml.Movie)
            .ToListAsync();
    }
    
    public async Task<bool> IsInListAsync(int userId, int movieId, Status status)
    {
        return await _context.UserMovies
            .AnyAsync(uml => uml.UserId == userId && uml.MovieId == movieId && uml.Status == status);
    }

    public async Task AddAsync(UserMovie userMovie)
    {
        await _context.UserMovies.AddAsync(userMovie);
    }

    public void Update(UserMovie userMovie)
    {
        _context.UserMovies.Update(userMovie);
    }
    
    public void RemoveFromList(UserMovie userMovie)
    {
        _context.UserMovies.Remove(userMovie);
    }

    public async Task SaveChangesAsync()
    {
        await _context.SaveChangesAsync();
    }
}