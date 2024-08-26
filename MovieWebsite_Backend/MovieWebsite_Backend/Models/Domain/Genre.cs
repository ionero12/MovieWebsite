using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace MovieWebsite_Backend.Models;

[Table("genres")]
public class Genre
{
    public Genre()
    {
    }

    public Genre(int genreId, string name, List<MovieGenre> movieGenres)
    {
        GenreId = genreId;
        Name = name;
        MovieGenres = movieGenres;
    }

    [Column("genre_id")] public int GenreId { get; set; }
    [Column("name")] public string Name { get; set; }

    [JsonIgnore] public List<MovieGenre> MovieGenres { get; set; }
}