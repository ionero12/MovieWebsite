using MovieWebsite_Backend.Models.Domain;

namespace MovieWebsite_Backend.Services.Interfaces;

public interface IMovieService
{
    Task<IEnumerable<Movie>> GetAllMoviesAsync();
    Task<Movie> GetMovieById(int movieId);
    Task ImportMoviesFromApiAsync(string genre, int limit);
    Task<IEnumerable<Movie>> GetAllMoviesByGenreAsync(string genre);
}