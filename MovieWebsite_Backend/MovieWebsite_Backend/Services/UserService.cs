using MovieWebsite_Backend.Auth.Models;
using MovieWebsite_Backend.Auth.Services;
using MovieWebsite_Backend.Data.Repositories.Interfaces;
using MovieWebsite_Backend.Models.Domain;
using MovieWebsite_Backend.Services.Interfaces;

namespace MovieWebsite_Backend.Services;

public class UserService : IUserService
{
    private readonly IJwtService _jwtService;
    private readonly IUserRepository _userRepository;


    public UserService(IUserRepository userRepository, IJwtService jwtService)
    {
        _userRepository = userRepository;
        _jwtService = jwtService;
    }

    public async Task<User> LoginAsync(string username, string password)
    {
        var user = await _userRepository.GetByUsernameAsync(username);

        if (user == null) throw new UnauthorizedAccessException("User does not exist");
        if (!VerifyPassword(password, user.Password)) throw new UnauthorizedAccessException("Password does not match");

        return user;
    }

    public async Task<User> RegisterAsync(RegisterDto registerDto)
    {
        if (await _userRepository.GetByUsernameAsync(registerDto.Username) != null)
            throw new InvalidOperationException("Username already exists");

        if (await _userRepository.GetByEmailAsync(registerDto.Email) != null)
            throw new InvalidOperationException("Email already exists");

        var user = new User
        {
            Username = registerDto.Username,
            Email = registerDto.Email,
            Password = HashPassword(registerDto.Password)
        };

        await _userRepository.AddAsync(user);

        return user;
    }

    private bool VerifyPassword(string password, string passwordHash)
    {
        return BCrypt.Net.BCrypt.Verify(password, passwordHash);
    }

    private string HashPassword(string password)
    {
        return BCrypt.Net.BCrypt.HashPassword(password);
    }
}