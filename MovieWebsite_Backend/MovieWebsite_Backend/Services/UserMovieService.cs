using MovieWebsite_Backend.Data.Repositories.Interfaces;
using MovieWebsite_Backend.Models.Domain;
using MovieWebsite_Backend.Services.Interfaces;

namespace MovieWebsite_Backend.Services;

public class UserMovieService : IUserMovieService
{
    private readonly IUserMovieRepository _userMovieRepository;

    public UserMovieService(IUserMovieRepository userMovieRepository)
    {
        _userMovieRepository = userMovieRepository;
    }

    public async Task AddToListAsync(int userId, int movieId, Status status)
    {
        var userMovie = await _userMovieRepository.GetUserMovieByIdsAsync(userId, movieId);
        if (userMovie == null)
        {
            userMovie = new UserMovie
            {
                UserId = userId,
                MovieId = movieId,
                Status = status
            };
            await _userMovieRepository.AddAsync(userMovie);
        }
        else
        {
            userMovie.Status = status;
            await _userMovieRepository.Update(userMovie);
        }
    }

    public async Task RemoveFromListAsync(int userId, int movieId, Status status)
    {
        var userMovie = await _userMovieRepository.GetUserMovieByIdsAndStatusAsync(userId, movieId, status);
        Console.WriteLine(userMovie);
        if (userMovie != null) await _userMovieRepository.RemoveFromList(userMovie);
    }

    public async Task<IEnumerable<UserMovie>> GetUserMoviesAsync(int userId, int movieId)
    {
        return await _userMovieRepository.GetUserMovieByIdAsync(userId, movieId);
    }

    public async Task<IEnumerable<Movie>> GetRatedUserMoviesAsync(int userId)
    {
        return await _userMovieRepository.GetRatedUserMoviesAsync(userId);
    }
    
    public async Task<IEnumerable<Movie>> GetUserListAsync(int userId, Status status)
    {
        return await _userMovieRepository.GetUserListAsync(userId, status);
    }

    public async Task<bool> IsInListAsync(int userId, int movieId, Status status)
    {
        return await _userMovieRepository.IsInListAsync(userId, movieId, status);
    }

    public async Task AddScoreToMovie(int userId, int movieId, float score)
    {
        var userMovie = await _userMovieRepository.GetUserMovieByIdsAsync(userId, movieId);
        if (userMovie == null)
        {
            userMovie = new UserMovie
            {
                UserId = userId,
                MovieId = movieId,
                Score = score
            };
            await _userMovieRepository.AddAsync(userMovie);
        }
        else
        {
            userMovie.Score = score;
            await _userMovieRepository.Update(userMovie);
        }
    }
}