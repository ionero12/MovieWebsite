using MovieWebsite_Backend.Models;

namespace MovieWebsite_Backend.Data;

public interface IUserRepository
{
    Task<User> GetByUsernameAsync(string username);
    Task<User> GetByEmailAsync(string email);
    Task<User> AddAsync(User user);

}