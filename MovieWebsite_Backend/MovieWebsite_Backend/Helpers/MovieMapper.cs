using MovieWebsite_Backend.Models;
using MovieWebsite_Backend.Models.Domain;
using MovieWebsite_Backend.Models.DTOs;

namespace MovieWebsite_Backend.Helpers;

public class MovieMapper
{
    public static Movie MapToMovie(MovieDto dto)
    {
        return new Movie
        {
            Title = dto.title,
            Description = dto.overview ?? "No description available.",
            ReleaseDate = DateOnly.FromDateTime(dto.release_date),
            Duration = dto.runtime,
            ExternalApiId = dto.id.ToString(),
            PosterPath = dto.poster_path
        };
    }

    public static List<Genre> MapToGenres(MovieDto dto)
    {
        return dto.genres.Split(", ")
            .Select(g => new Genre { Name = g })
            .ToList();
    }
}