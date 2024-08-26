using MovieWebsite_Backend.Models;

namespace MovieWebsite_Backend.DTO;

public interface IJwtService
{
    string GenerateToken(User user);
}