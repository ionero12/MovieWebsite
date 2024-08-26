using Microsoft.EntityFrameworkCore;
using MovieWebsite_Backend.Models;

namespace MovieWebsite_Backend.Data;

public class UserMovieRepository : IUserMovieRepository
{
    private readonly ApplicationDbContext _context;

    public UserMovieRepository(ApplicationDbContext context)
    {
        _context = context;
    }
    
    public async Task<UserMovie> AddToListAsync(int userId, int movieId, Status status)
    {
        var UserMovie = new UserMovie
        {
            UserId = userId,
            MovieId = movieId,
            Status = status,
        };

        await _context.UserMovies.AddAsync(UserMovie);
        await _context.SaveChangesAsync();
        return UserMovie;
    }

    public async Task RemoveFromListAsync(int userId, int movieId, Status status)
    {
        var UserMovie = await _context.UserMovies
            .FirstOrDefaultAsync(uml => uml.UserId == userId && uml.MovieId == movieId && uml.Status == status);

        if (UserMovie != null)
        {
            _context.UserMovies.Remove(UserMovie);
            await _context.SaveChangesAsync();
        }
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
}