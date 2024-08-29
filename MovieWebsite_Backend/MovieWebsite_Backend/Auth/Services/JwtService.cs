using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using MovieWebsite_Backend.Auth.Models;
using MovieWebsite_Backend.Models;
using MovieWebsite_Backend.Models.Domain;

namespace MovieWebsite_Backend.Auth.Services;

public class JwtService : IJwtService
{
    private readonly JwtSettings _jwtSettings;
    private readonly IHttpContextAccessor _httpContextAccessor;

    public JwtService(IOptions<JwtSettings> jwtSettings, IHttpContextAccessor httpContextAccessor)
    {
        _jwtSettings = jwtSettings.Value;
        _httpContextAccessor = httpContextAccessor;
    }
    
    public void SetJwtCookie(User user)
    {
        var token = GenerateToken(user);
        
        var cookieOptions = new CookieOptions
        {
            HttpOnly = true,
            Secure = true, // Use this in production with HTTPS
            SameSite = SameSiteMode.Strict,
            Expires = DateTime.Now.AddMinutes(_jwtSettings.ExpirationInMinutes)
        };

        _httpContextAccessor.HttpContext.Response.Cookies.Append("jwt", token, cookieOptions);
    }
    
    public void ClearJwtCookie()
    {
        _httpContextAccessor.HttpContext.Response.Cookies.Delete("jwt");
    }

    public string GenerateToken(User user)
    {
        var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_jwtSettings.Secret));
        var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);

        var claims = new[]
        {
            new Claim(ClaimTypes.NameIdentifier, user.UserId.ToString()),
            new Claim(ClaimTypes.Name, user.Username),
            new Claim(ClaimTypes.Email, user.Email)
        };

        var token = new JwtSecurityToken(
            _jwtSettings.Issuer,
            _jwtSettings.Audience,
            claims,
            expires: DateTime.Now.AddMinutes(_jwtSettings.ExpirationInMinutes),
            signingCredentials: credentials
        );

        return new JwtSecurityTokenHandler().WriteToken(token);
    }
}