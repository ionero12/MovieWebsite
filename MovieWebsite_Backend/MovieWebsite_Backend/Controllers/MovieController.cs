using Microsoft.AspNetCore.Mvc;
using MovieWebsite_Backend.Models;
using MovieWebsite_Backend.Models.Domain;
using MovieWebsite_Backend.Services;
using MovieWebsite_Backend.Services.Interfaces;

namespace MovieWebsite_Backend.Controllers;

[Route("api/[controller]")]
[ApiController]
public class MovieController : ControllerBase
{
    private readonly IMovieService _movieService;

    public MovieController(IMovieService movieService)
    {
        _movieService = movieService;
    }

    [HttpGet]
    public async Task<ActionResult<IEnumerable<Movie>>> GetAllMovies()
    {
        var movies = await _movieService.GetAllMoviesAsync();
        return Ok(movies);
    }

    [HttpGet("{movieId:int}")]
    public async Task<ActionResult<Movie>> GetMovieById(int movieId)
    {
        var movie = await _movieService.GetMovieById(movieId);
        return Ok(movie);
    }

    [HttpGet("filterByGenre")]
    public async Task<ActionResult<IEnumerable<Movie>>> GetAllMoviesByGenre([FromQuery] string genre)
    {
        if (string.IsNullOrWhiteSpace(genre))
            return BadRequest("Genre param is required");

        var movies = await _movieService.GetAllMoviesByGenreAsync(genre);
        return Ok(movies);
    }

    [HttpPost("import")]
    public async Task<IActionResult> ImportMovies(string genre, int limit)
    {
        await _movieService.ImportMoviesFromApiAsync(genre, limit);
        return Ok("Movies imported successfully");
    }
}