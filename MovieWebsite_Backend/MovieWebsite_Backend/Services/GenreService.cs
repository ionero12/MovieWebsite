using MovieWebsite_Backend.Data;

namespace MovieWebsite_Backend.Services;

public class GenreService : IGenreService
{
    private readonly IGenreRepository _genreRepository;


    public GenreService(IGenreRepository genreRepository)
    {
        _genreRepository = genreRepository;
    }
}