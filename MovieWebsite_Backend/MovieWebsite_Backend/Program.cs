using System.Text;
using DotNetEnv;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using MovieWebsite_Backend.Auth.Models;
using MovieWebsite_Backend.Extensions;

var builder = WebApplication.CreateBuilder(args);

ConfigureEnvironment(builder);
ConfigureServices(builder);

var app = builder.Build();

ConfigureMiddleware(app);
ConfigureEndpoints(app);

app.Run();

void ConfigureEnvironment(WebApplicationBuilder builder)
{
    Env.Load();
    builder.Configuration
        .AddJsonFile("appsettings.json", false, true)
        .AddEnvironmentVariables().Build();
}

void ConfigureServices(WebApplicationBuilder builder)
{
    builder.Services.Configure<JwtSettings>(options =>
    {
        options.Secret = builder.Configuration["JWT_SECRET"];
        options.ExpirationInMinutes = builder.Configuration.GetValue<int>("JwtSettings:ExpirationInMinutes");
        options.Issuer = builder.Configuration["JwtSettings:Issuer"];
        options.Audience = builder.Configuration["JwtSettings:Audience"];
    });

    builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
        .AddJwtBearer(options =>
        {
            var key = Encoding.ASCII.GetBytes(builder.Configuration["JWT_SECRET"]);
            options.TokenValidationParameters = new TokenValidationParameters
            {
                ValidateIssuerSigningKey = true,
                IssuerSigningKey = new SymmetricSecurityKey(key),
                ValidateIssuer = true,
                ValidIssuer = builder.Configuration["JwtSettings:Issuer"],
                ValidateAudience = true,
                ValidAudience = builder.Configuration["JwtSettings:Audience"],
                ValidateLifetime = true,
                ClockSkew = TimeSpan.Zero
            };
        });

    builder.Services.AddControllers();
    builder.Services.AddApplicationServices(builder.Configuration);
    builder.Services.AddAuthorization();
}

void ConfigureMiddleware(WebApplication app)
{
    app.UseHttpsRedirection();
    app.UseAuthentication();
    app.UseAuthorization();
}

void ConfigureEndpoints(WebApplication app)
{
    app.MapControllers();
}