using MovieWebsite_Backend.Data;
using MovieWebsite_Backend.Models;

namespace MovieWebsite_Backend.Services;

public class UserMovieService : IUserMovieService
{
    private readonly IUserMovieRepository _userMovieRepository;

    public UserMovieService(IUserMovieRepository userMovieRepository)
    {
        _userMovieRepository = userMovieRepository;
    }
    
    public async Task AddToListAsync(int userId, int movieId, Status status)
    {
        await _userMovieRepository.AddToListAsync(userId, movieId, status);
    }

    public async Task RemoveFromListAsync(int userId, int movieId, Status status)
    {
        await _userMovieRepository.RemoveFromListAsync(userId, movieId, status);
    }

    public async Task<IEnumerable<Movie>> GetUserListAsync(int userId, Status status)
    {
        return await _userMovieRepository.GetUserListAsync(userId, status);
    }

    public async Task<bool> IsInListAsync(int userId, int movieId, Status status)
    {
        return await _userMovieRepository.IsInListAsync(userId, movieId, status);
    }
}