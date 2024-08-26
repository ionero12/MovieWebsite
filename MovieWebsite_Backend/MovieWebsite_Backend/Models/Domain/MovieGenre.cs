using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace MovieWebsite_Backend.Models.Domain;

[Table("movies_has_genres")]
public class MovieGenre
{
    public MovieGenre()
    {
    }

    public MovieGenre(int genreId, Genre genre, int movieId, Movie movie)
    {
        GenreId = genreId;
        Genre = genre;
        MovieId = movieId;
        Movie = movie;
    }

    [Column("genres_genre_id")] public int GenreId { get; set; }
    [JsonIgnore] public Genre Genre { get; set; }
    [Column("movies_movie_id")] public int MovieId { get; set; }
    [JsonIgnore] public Movie Movie { get; set; }
}