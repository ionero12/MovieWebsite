using Microsoft.EntityFrameworkCore;
using MovieWebsite_Backend.Data;
using MovieWebsite_Backend.DTO;
using MovieWebsite_Backend.Services;

namespace MovieWebsite_Backend.Extensions;

public static class ServiceCollectionExtensions
{
    public static IServiceCollection AddApplicationServices(this IServiceCollection services,
        IConfiguration configuration)
    {
        services.AddDbContext<ApplicationDbContext>(options =>
            options.UseMySql(configuration.GetConnectionString("DefaultConnection"),
                new MySqlServerVersion(new Version(8, 0, 35))));

        services.AddScoped<IMovieRepository, MovieRepository>();
        services.AddScoped<IGenreRepository, GenreRepository>();
        services.AddScoped<IUserRepository, UserRepository>();
        services.AddScoped<IMovieService, MovieService>();
        services.AddScoped<IGenreService, GenreService>();
        services.AddScoped<IUserService, UserService>();
        services.AddScoped<IUserMovieRepository, UserMovieRepository>();
        services.AddScoped<IUserMovieService, UserMovieService>();
        services.AddScoped<MovieApiService>();
        services.AddScoped<IJwtService, JwtService>();

        return services;
    }
}