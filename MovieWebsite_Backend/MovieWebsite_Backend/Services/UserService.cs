using MovieWebsite_Backend.Data;
using MovieWebsite_Backend.DTO;
using MovieWebsite_Backend.Models;

namespace MovieWebsite_Backend.Services;

public class UserService : IUserService
{
    private readonly IUserRepository _userRepository;
    private readonly IJwtService _jwtService;


    public UserService(IUserRepository userRepository, IJwtService jwtService)
    {
        _userRepository = userRepository;
        _jwtService = jwtService;
    }
    
    public async Task<string> LoginAsync(string username, string password)
    {
        var user = await _userRepository.GetByUsernameAsync(username);
        
        if (user == null)
        {
            throw new UnauthorizedAccessException("User does not exist");
        }
        if(!VerifyPassword(password, user.Password))
        {
            throw new UnauthorizedAccessException("Password does not match");
        }

        return _jwtService.GenerateToken(user);
    }
    
    public async Task<User> RegisterAsync(RegisterDTO registerDTO)
    {
        if (await _userRepository.GetByUsernameAsync(registerDTO.Username) != null)
        {
            throw new InvalidOperationException("Username already exists");
        }

        if (await _userRepository.GetByEmailAsync(registerDTO.Email) != null)
        {
            throw new InvalidOperationException("Email already exists");
        }

        var user = new User
        {
            Username = registerDTO.Username,
            Email = registerDTO.Email,
            Password= HashPassword(registerDTO.Password)
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