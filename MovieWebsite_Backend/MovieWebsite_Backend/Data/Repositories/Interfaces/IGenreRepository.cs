using MovieWebsite_Backend.Models;

namespace MovieWebsite_Backend.Data;

public interface IGenreRepository
{
    Task<Genre> AddAsync(Genre genre);
    Task<Genre> GetByNameAsync(string name);
}