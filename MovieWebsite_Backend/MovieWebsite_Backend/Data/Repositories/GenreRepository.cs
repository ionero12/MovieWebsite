using Microsoft.EntityFrameworkCore;
using MovieWebsite_Backend.Data.Repositories.Interfaces;
using MovieWebsite_Backend.Models;
using MovieWebsite_Backend.Models.Domain;

namespace MovieWebsite_Backend.Data.Repositories;

public class GenreRepository : IGenreRepository
{
    private readonly ApplicationDbContext _context;

    public GenreRepository(ApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<Genre> AddAsync(Genre genre)
    {
        await _context.Genres.AddAsync(genre);
        await _context.SaveChangesAsync();
        return genre;
    }

    public async Task<Genre> GetByNameAsync(string name)
    {
        return await _context.Genres
            .FirstOrDefaultAsync(g => g.Name.ToLower() == name.ToLower());
    }
}