using Microsoft.AspNetCore.Mvc;
using MovieWebsite_Backend.Auth.Models;
using MovieWebsite_Backend.Auth.Services;
using MovieWebsite_Backend.Models.Domain;
using MovieWebsite_Backend.Services;
using MovieWebsite_Backend.Services.Interfaces;

namespace MovieWebsite_Backend.Controllers;

[ApiController]
[Route("api/[controller]")]
public class UserController : ControllerBase
{
    private readonly IJwtService _jwtService;
    private readonly IUserService _userService;

    public UserController(IUserService userService, IJwtService jwtService)
    {
        _userService = userService;
        _jwtService = jwtService;
    }

    [HttpPost("login")]
    public async Task<ActionResult<User>> Login([FromBody] LoginDto loginDto)
    {
        try
        {
            var user = await _userService.LoginAsync(loginDto.Username, loginDto.Password);
            _jwtService.SetJwtCookie(user);
            return Ok(user);
        }
        catch (UnauthorizedAccessException)
        {
            return Unauthorized();
        }
    }

    [HttpPost("register")]
    public async Task<ActionResult<User>> Register([FromBody] RegisterDto registerDto)
    {
        try
        {
            var user = await _userService.RegisterAsync(registerDto);
            _jwtService.SetJwtCookie(user);
            return Ok(user);
        }
        catch (InvalidOperationException ex)
        {
            return BadRequest(ex.Message);
        }
    }
    
    [HttpPost("logout")]
    public IActionResult Logout()
    {
        _jwtService.ClearJwtCookie();
        return Ok(new { Message = "Logged out successfully" });
    }
}