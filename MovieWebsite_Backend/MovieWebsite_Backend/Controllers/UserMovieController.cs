using System.Security.Claims;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MovieWebsite_Backend.Models;
using MovieWebsite_Backend.Services;

namespace MovieWebsite_Backend.Controllers;

[Authorize]
[ApiController]
[Route("api/[controller]")]
public class UserMovieController : ControllerBase
{
    private readonly IUserMovieService _userMovieListService;

    public UserMovieController(IUserMovieService userMovieService)
    {
        _userMovieListService = userMovieService;
    }

    [HttpPost("add")]
    public async Task<IActionResult> AddToList(int movieId, Status status)
    {
        var userId = int.Parse(User.FindFirst(ClaimTypes.NameIdentifier)?.Value);
        await _userMovieListService.AddToListAsync(userId, movieId, status);
        return Ok();
    }

    [HttpPost("remove")]
    public async Task<IActionResult> RemoveFromList(int movieId, Status status)
    {
        var userId = int.Parse(User.FindFirst(ClaimTypes.NameIdentifier)?.Value);
        await _userMovieListService.RemoveFromListAsync(userId, movieId, status);
        return Ok();
    }

    [HttpGet("list")]
    public async Task<IActionResult> GetUserList(Status status)
    {
        var userId = int.Parse(User.FindFirst(ClaimTypes.NameIdentifier)?.Value);
        var movies = await _userMovieListService.GetUserListAsync(userId, status);
        return Ok(movies);
    }

    [HttpGet("isinlist")]
    public async Task<IActionResult> IsInList(int movieId, Status status)
    {
        var userId = int.Parse(User.FindFirst(ClaimTypes.NameIdentifier)?.Value);
        var isInList = await _userMovieListService.IsInListAsync(userId, movieId, status);
        return Ok(isInList);
    }
}