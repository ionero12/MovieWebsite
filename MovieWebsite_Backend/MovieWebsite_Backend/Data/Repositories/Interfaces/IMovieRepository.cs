using MovieWebsite_Backend.Models;
using MovieWebsite_Backend.Models.Domain;

namespace MovieWebsite_Backend.Data.Repositories.Interfaces;

public interface IMovieRepository
{
    Task<IEnumerable<Movie>> GetAllMoviesAsync();
    Task<Movie> GetMovieById(int movieId);
    Task<Movie> AddAsync(Movie movie);
    Task AddGenreToMovieAsync(int movieId, int genreId);
    Task<bool> ExistsByExternalApiIdAsync(string externalApiId);

    Task<IEnumerable<Movie>> GetAllMoviesByGenreAsync(string genre);
}