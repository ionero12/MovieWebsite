using MovieWebsite_Backend.Data.Repositories.Interfaces;
using MovieWebsite_Backend.Services.Interfaces;

namespace MovieWebsite_Backend.Services;

public class GenreService : IGenreService
{
    private readonly IGenreRepository _genreRepository;
    
    public GenreService(IGenreRepository genreRepository)
    {
        _genreRepository = genreRepository;
    }
}