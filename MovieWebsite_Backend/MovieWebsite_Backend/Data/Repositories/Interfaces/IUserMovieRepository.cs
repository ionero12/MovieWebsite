using MovieWebsite_Backend.Models;

namespace MovieWebsite_Backend.Data;

public interface IUserMovieRepository
{
    Task<UserMovie> AddToListAsync(int userId, int movieId, Status status);
    Task RemoveFromListAsync(int userId, int movieId, Status status);
    Task<IEnumerable<Movie>> GetUserListAsync(int userId, Status status);
    Task<bool> IsInListAsync(int userId, int movieId, Status status);
}