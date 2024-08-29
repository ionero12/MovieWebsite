using MovieWebsite_Backend.Models;
using MovieWebsite_Backend.Models.Domain;

namespace MovieWebsite_Backend.Auth.Services;

public interface IJwtService
{
    void SetJwtCookie(User user);
    void ClearJwtCookie();
    string GenerateToken(User user);
}