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
    public async Task<IActionResult> AddToList(int movieId, Status status)
    {
        var userId = int.Parse(User.FindFirst(ClaimTypes.NameIdentifier)?.Value);
        await _userMovieService.AddToListAsync(userId, movieId, status);
        return Ok();
    }

    [HttpPost("remove")]
    public async Task<IActionResult> RemoveFromList(int movieId, Status status)
    {
        var userId = int.Parse(User.FindFirst(ClaimTypes.NameIdentifier)?.Value);
        await _userMovieService.RemoveFromListAsync(userId, movieId, status);
        return Ok();
    }

    [HttpGet("list")]
    public async Task<IActionResult> GetUserList(Status status)
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
    public async Task<IActionResult> AddScoreToMovie(int movieId, int score)
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