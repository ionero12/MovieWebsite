using MovieWebsite_Backend.Models;
using MovieWebsite_Backend.Models.Domain;

namespace MovieWebsite_Backend.Data.Repositories.Interfaces;

public interface IGenreRepository
{
    Task<Genre> AddAsync(Genre genre);
    Task<Genre> GetByNameAsync(string name);
}