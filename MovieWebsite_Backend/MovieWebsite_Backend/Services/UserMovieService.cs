using MovieWebsite_Backend.Data;
using MovieWebsite_Backend.Models;

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
        var UserMovie = new UserMovie
        {
            UserId = userId,
            MovieId = movieId,
            Status = status
        };

        await _userMovieRepository.AddAsync(UserMovie);
        await _userMovieRepository.SaveChangesAsync();
    }

    public async Task RemoveFromListAsync(int userId, int movieId, Status status)
    {
        var UserMovie = await _userMovieRepository.GetUserMovieByIdsAndStatusAsync(userId, movieId, status);
        if (UserMovie != null) _userMovieRepository.RemoveFromList(UserMovie);

        await _userMovieRepository.SaveChangesAsync();
    }

    public async Task<IEnumerable<Movie>> GetUserListAsync(int userId, Status status)
    {
        return await _userMovieRepository.GetUserListAsync(userId, status);
    }

    public async Task<bool> IsInListAsync(int userId, int movieId, Status status)
    {
        return await _userMovieRepository.IsInListAsync(userId, movieId, status);
    }

    public async Task AddScoreToMovie(int userId, int movieId, int score)
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
            _userMovieRepository.Update(userMovie);
        }

        await _userMovieRepository.SaveChangesAsync();
    }
}