using Microsoft.AspNetCore.Mvc;
using MovieWebsite_Backend.Auth.Models;
using MovieWebsite_Backend.Auth.Services;
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
    public async Task<IActionResult> Login([FromBody] LoginDto loginDto)
    {
        try
        {
            var token = await _userService.LoginAsync(loginDto.Username, loginDto.Password);
            return Ok(new { Token = token });
        }
        catch (UnauthorizedAccessException)
        {
            return Unauthorized();
        }
    }

    [HttpPost("register")]
    public async Task<IActionResult> Register([FromBody] RegisterDto registerDto)
    {
        try
        {
            var user = await _userService.RegisterAsync(registerDto);
            var token = _jwtService.GenerateToken(user);
            return Ok(new { Token = token });
        }
        catch (InvalidOperationException ex)
        {
            return BadRequest(ex.Message);
        }
    }
}