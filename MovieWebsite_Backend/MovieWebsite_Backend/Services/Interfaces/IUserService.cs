using MovieWebsite_Backend.Auth.Models;
using MovieWebsite_Backend.Models.Domain;

namespace MovieWebsite_Backend.Services.Interfaces;

public interface IUserService
{
    Task<User> LoginAsync(string username, string password);
    Task<User> RegisterAsync(RegisterDto registerDto);
}