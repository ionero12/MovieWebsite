using MovieWebsite_Backend.Models;
using MovieWebsite_Backend.Models.Domain;

namespace MovieWebsite_Backend.Data.Repositories.Interfaces;

public interface IUserMovieRepository
{
    Task RemoveFromList(UserMovie userMovie);
    Task<IEnumerable<Movie>> GetUserListAsync(int userId, Status status);
    Task<bool> IsInListAsync(int userId, int movieId, Status status);
    Task<UserMovie> GetUserMovieByIdsAsync(int userId, int movieId);
    Task<UserMovie> GetUserMovieByIdsAndStatusAsync(int userId, int movieId, Status status);
    Task AddAsync(UserMovie userMovie);
    Task Update(UserMovie userMovie);
    Task SaveChangesAsync();
}