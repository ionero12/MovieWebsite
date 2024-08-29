using System.ComponentModel.DataAnnotations.Schema;

namespace MovieWebsite_Backend.Models.Domain;

[Table("users_has_movies")]
public class UserMovie
{
    public UserMovie()
    {
    }

    public UserMovie(int movieId, Movie movie, int userId, User user, Status status, float score)
    {
        MovieId = movieId;
        Movie = movie;
        UserId = userId;
        User = user;
        Status = status;
        Score = score;
    }

    [Column("movies_movie_id")] public int MovieId { get; set; }
    public Movie Movie { get; set; }
    [Column("users_user_id")] public int UserId { get; set; }
    public User User { get; set; }

    [Column("status")] public Status Status { get; set; }

    [Column("score")] public float Score { get; set; }
}

public enum Status
{
    seen,
    next
}