using MovieWebsite_Backend.Models;

namespace MovieWebsite_Backend.Services;

public interface IMovieService
{
    Task<IEnumerable<Movie>> GetAllMoviesAsync();
    Task ImportMoviesFromApiAsync(string genre, int limit);
    Task<IEnumerable<Movie>> GetAllMoviesByGenreAsync(string genre);
}