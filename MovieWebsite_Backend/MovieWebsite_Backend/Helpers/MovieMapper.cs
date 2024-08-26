using MovieWebsite_Backend.Models;

namespace MovieWebsite_Backend.DTO;

public class MovieMapper
{
    public static Movie MapToMovie(MovieDTO dto)
    {
        return new Movie
        {
            Title = dto.title,
            Description = dto.overview,
            ReleaseDate = DateOnly.FromDateTime(dto.release_date),
            Duration = dto.runtime,
            ExternalApiId = dto.id.ToString()
        };
    }

    public static List<Genre> MapToGenres(MovieDTO dto)
    {
        return dto.genres.Split(", ")
            .Select(g => new Genre { Name = g })
            .ToList();
    }
}