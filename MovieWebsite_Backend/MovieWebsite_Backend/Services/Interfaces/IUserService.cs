using MovieWebsite_Backend.DTO;
using MovieWebsite_Backend.Models;

namespace MovieWebsite_Backend.Services;

public interface IUserService
{
    Task<string> LoginAsync(string username, string password);
    Task<User> RegisterAsync(RegisterDTO registerDto);

}