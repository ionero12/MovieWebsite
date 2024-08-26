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

    public async Task<UserMovie> GetByIds (int userId, int movieId)
    {
        return await _context.UserMovies.FirstOrDefaultAsync(uml => uml.UserId == userId && uml.MovieId == movieId);
    }
    
    public async Task AddAsync(UserMovie userMovie)
    {
        await _context.UserMovies.AddAsync(userMovie);
    }
    
    public void Update(UserMovie userMovie)
    {
        _context.UserMovies.Update(userMovie);
    }

    public async Task SaveChangesAsync()
    {
        await _context.SaveChangesAsync();
    }
}