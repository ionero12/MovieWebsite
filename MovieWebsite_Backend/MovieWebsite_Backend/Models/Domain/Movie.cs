using System.ComponentModel.DataAnnotations.Schema;

namespace MovieWebsite_Backend.Models.Domain;

[Table("movies")]
public class Movie
{
    public Movie()
    {
    }

    public Movie(int movieId, string title, string? description, DateOnly releaseDate, int duration,
        string externalApiId, List<MovieGenre> movieGenres, List<UserMovie> userMovies, string posterPath)
    {
        MovieId = movieId;
        Title = title;
        Description = description;
        ReleaseDate = releaseDate;
        Duration = duration;
        ExternalApiId = externalApiId;
        MovieGenres = movieGenres;
        UserMovies = userMovies;
        PosterPath = posterPath;
    }

    [Column("movie_id")] public int MovieId { get; set; }
    [Column("title")] public string Title { get; set; }
    [Column("description")] public string? Description { get; set; }
    [Column("release_date")] public DateOnly ReleaseDate { get; set; }
    [Column("duration")] public int Duration { get; set; }
    [Column("external_api_id")] public string ExternalApiId { get; set; }
    [Column("poster_path")] public string PosterPath { get; set; }
    public List<MovieGenre> MovieGenres { get; set; }
    public List<UserMovie> UserMovies { get; set; }
}