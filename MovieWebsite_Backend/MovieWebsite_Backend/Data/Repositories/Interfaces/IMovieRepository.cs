using MovieWebsite_Backend.Models;

namespace MovieWebsite_Backend.Data;

public interface IMovieRepository
{
    Task<IEnumerable<Movie>> GetAllMoviesAsync();
    Task<Movie> AddAsync(Movie movie);
    Task AddGenreToMovieAsync(int movieId, int genreId);
    Task<bool> ExistsByExternalApiIdAsync(string externalApiId);

    Task<IEnumerable<Movie>> GetAllMoviesByGenreAsync(string genre);
}