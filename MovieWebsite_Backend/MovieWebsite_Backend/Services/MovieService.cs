using MovieWebsite_Backend.Data;
using MovieWebsite_Backend.DTO;
using MovieWebsite_Backend.Models;

namespace MovieWebsite_Backend.Services;

public class MovieService : IMovieService
{
    private readonly IGenreRepository _genreRepository;
    private readonly MovieApiService _movieApiService;
    private readonly IMovieRepository _movieRepository;


    public MovieService(IMovieRepository movieRepository, IGenreRepository genreRepository,
        MovieApiService movieApiService)
    {
        _movieRepository = movieRepository;
        _genreRepository = genreRepository;
        _movieApiService = movieApiService;
    }

    public Task<IEnumerable<Movie>> GetAllMoviesAsync()
    {
        return _movieRepository.GetAllMoviesAsync();
    }

    public Task<IEnumerable<Movie>> GetAllMoviesByGenreAsync(string genre)
    {
        return _movieRepository.GetAllMoviesByGenreAsync(genre);
    }

    public async Task ImportMoviesFromApiAsync(string genre, int limit)
    {
        var apiMovies = await _movieApiService.GetMoviesAsync(genre, limit);

        foreach (var apiMovie in apiMovies)
        {
            if (await _movieRepository.ExistsByExternalApiIdAsync(apiMovie.id
                    .ToString())) continue; 

            var movie = MovieMapper.MapToMovie(apiMovie);
            var genres = MovieMapper.MapToGenres(apiMovie);

            await _movieRepository.AddAsync(movie);

            foreach (var genreModel in genres)
            {
                var existingGenre = await _genreRepository.GetByNameAsync(genreModel.Name);
                if (existingGenre == null) existingGenre = await _genreRepository.AddAsync(genreModel);

                await _movieRepository.AddGenreToMovieAsync(movie.MovieId, existingGenre.GenreId);
            }
        }
    }
}