using MovieWebsite_Backend.Models;
using MovieWebsite_Backend.Models.Domain;

namespace MovieWebsite_Backend.Auth.Services;

public interface IJwtService
{
    string GenerateToken(User user);
}