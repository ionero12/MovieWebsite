using MovieWebsite_Backend.Models;
using MovieWebsite_Backend.Models.Domain;

namespace MovieWebsite_Backend.Data.Repositories.Interfaces;

public interface IUserRepository
{
    Task<User> GetByUsernameAsync(string username);
    Task<User> GetByEmailAsync(string email);
    Task<User> AddAsync(User user);
}