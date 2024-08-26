using Microsoft.AspNetCore.Mvc;
using MovieWebsite_Backend.DTO;
using MovieWebsite_Backend.Services;

namespace MovieWebsite_Backend.Controllers;

[ApiController]
[Route("api/[controller]")]
public class UserController : ControllerBase
{
    private readonly IUserService _userService;
    private readonly IJwtService _jwtService;

    public UserController(IUserService userService, IJwtService jwtService)
    {
        _userService = userService;
        _jwtService = jwtService;
    }
    
    [HttpPost("login")]
    public async Task<IActionResult> Login([FromBody] LoginDTO loginDTO)
    {
        try
        {
            var token = await _userService.LoginAsync(loginDTO.Username, loginDTO.Password);
            return Ok(new { Token = token });
        }
        catch (UnauthorizedAccessException)
        {
            return Unauthorized();
        }
    }
    
    [HttpPost("register")]
    public async Task<IActionResult> Register([FromBody] RegisterDTO registerDTO)
    {
        try
        {
            var user = await _userService.RegisterAsync(registerDTO);
            var token = _jwtService.GenerateToken(user);
            return Ok(new { Token = token });
        }
        catch (InvalidOperationException ex)
        {
            return BadRequest(ex.Message);
        }
    }
}
