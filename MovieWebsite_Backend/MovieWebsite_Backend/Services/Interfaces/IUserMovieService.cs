using MovieWebsite_Backend.Models.Domain;

namespace MovieWebsite_Backend.Services.Interfaces;

public interface IUserMovieService
{
    Task AddToListAsync(int userId, int movieId, Status status);
    Task RemoveFromListAsync(int userId, int movieId, Status status);
    Task<IEnumerable<UserMovie>> GetUserMoviesAsync(int userId, int movieId);
    Task<IEnumerable<Movie>> GetRatedUserMoviesAsync(int userId);
    Task<IEnumerable<Movie>> GetUserListAsync(int userId, Status status);
    Task<bool> IsInListAsync(int userId, int movieId, Status status);
    Task AddScoreToMovie(int userId, int movieId, float score);
}