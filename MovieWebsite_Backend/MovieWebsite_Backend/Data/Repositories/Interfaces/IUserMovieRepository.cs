using MovieWebsite_Backend.Models;

namespace MovieWebsite_Backend.Data;

public interface IUserMovieRepository
{
    Task AddToListAsync(UserMovie userMovie);
    Task RemoveFromListAsync(int userId, int movieId, Status status);
    Task<IEnumerable<Movie>> GetUserListAsync(int userId, Status status);
    Task<bool> IsInListAsync(int userId, int movieId, Status status);
    Task<UserMovie> GetByIds(int userId, int movieId);
    Task AddAsync(UserMovie userMovie);
    void Update(UserMovie userMovie);
    Task SaveChangesAsync();
}