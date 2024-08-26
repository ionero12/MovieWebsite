using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace MovieWebsite_Backend.Models.Domain;

[Table("users")]
public class User
{
    public User()
    {
    }

    public User(int userId, string username, string email, string password, List<UserMovie> userMovies)
    {
        UserId = userId;
        Username = username;
        Email = email;
        Password = password;
        UserMovies = userMovies;
    }

    [Column("user_id")] public int UserId { get; set; }
    [Column("username")] public string Username { get; set; }
    [Column("email")] public string Email { get; set; }
    [Column("password")] public string Password { get; set; }
    [JsonIgnore] public List<UserMovie> UserMovies { get; set; }
}