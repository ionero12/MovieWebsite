using System.Security.Claims;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MovieWebsite_Backend.Models;
using MovieWebsite_Backend.Models.Domain;
using MovieWebsite_Backend.Services;
using MovieWebsite_Backend.Services.Interfaces;

namespace MovieWebsite_Backend.Controllers;

[Authorize]
[ApiController]
[Route("api/[controller]")]
public class UserMovieController : ControllerBase
{
    private readonly IUserMovieService _userMovieService;

    public UserMovieController(IUserMovieService userMovieService)
    {
        _userMovieService = userMovieService;
    }

    [HttpPost("add")]
    public async Task<IActionResult> AddToList([FromQuery ]int movieId, [FromQuery] Status status)
    {
        var userId = int.Parse(User.FindFirst(ClaimTypes.NameIdentifier)?.Value);
        Console.WriteLine(status);
        await _userMovieService.AddToListAsync(userId, movieId, status);
        return Ok();
    }

    [HttpDelete("remove")]
    public async Task<IActionResult> RemoveFromList([FromQuery ] int movieId, [FromQuery ] Status status)
    {
        var userId = int.Parse(User.FindFirst(ClaimTypes.NameIdentifier)?.Value);
        Console.WriteLine(userId);
        Console.WriteLine(movieId);
        Console.WriteLine(status);
        await _userMovieService.RemoveFromListAsync(userId, movieId, status);
        return Ok();
    }

    [HttpGet]
    public async Task<ActionResult<IEnumerable<UserMovie>>> GetUserMovies(int movieId)
    {
        var userId = int.Parse(User.FindFirst(ClaimTypes.NameIdentifier)?.Value);
        var movies = await _userMovieService.GetUserMoviesAsync(userId, movieId);
        return Ok(movies);
    }

    [HttpGet("rated")]
    public async Task<ActionResult<IEnumerable<Movie>>> GetRatedUserMovies()
    {
        var userId = int.Parse(User.FindFirst(ClaimTypes.NameIdentifier)?.Value);
        var movies = await _userMovieService.GetRatedUserMoviesAsync(userId);
        return Ok(movies);
    }
    
    [HttpGet("list")]
    public async Task<ActionResult<IEnumerable<Movie>>> GetUserList(Status status)
    {
        var userId = int.Parse(User.FindFirst(ClaimTypes.NameIdentifier)?.Value);
        var movies = await _userMovieService.GetUserListAsync(userId, status);
        return Ok(movies);
    }

    [HttpGet("isinlist")]
    public async Task<IActionResult> IsInList(int movieId, Status status)
    {
        var userId = int.Parse(User.FindFirst(ClaimTypes.NameIdentifier)?.Value);
        var isInList = await _userMovieService.IsInListAsync(userId, movieId, status);
        return Ok(isInList);
    }

    [HttpPost("score")]
    public async Task<IActionResult> AddScoreToMovie([FromQuery] int movieId, [FromQuery] float score)
    {
        var userIdClaim = User.FindFirst(ClaimTypes.NameIdentifier);
        if (userIdClaim == null) return Unauthorized("User ID claim not found in token");

        if (!int.TryParse(userIdClaim.Value, out var userId)) return BadRequest("Invalid user ID in token");

        try
        {
            await _userMovieService.AddScoreToMovie(userId, movieId, score);
            return Ok();
        }
        catch (Exception ex)
        {
            return StatusCode(500, "An error occurred while processing your request");
        }
    }
}