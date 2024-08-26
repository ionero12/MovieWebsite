using MovieWebsite_Backend.Models;

namespace MovieWebsite_Backend.Data;

public interface IUserMovieRepository
{
    void RemoveFromList(UserMovie userMovie);
    Task<IEnumerable<Movie>> GetUserListAsync(int userId, Status status);
    Task<bool> IsInListAsync(int userId, int movieId, Status status);
    Task<UserMovie> GetUserMovieByIdsAsync(int userId, int movieId);
    Task<UserMovie> GetUserMovieByIdsAndStatusAsync(int userId, int movieId, Status status);
    Task AddAsync(UserMovie userMovie);
    void Update(UserMovie userMovie);
    Task SaveChangesAsync();
}