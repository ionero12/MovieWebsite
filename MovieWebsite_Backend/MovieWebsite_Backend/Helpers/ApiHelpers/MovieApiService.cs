using MovieWebsite_Backend.DTO;
using Newtonsoft.Json;

namespace MovieWebsite_Backend.Services;

public class MovieApiService
{
    private const string ApiKey = "c72dc6a168msh55b5327e3e6bb43p12a948jsn31f0b081daf0";
    private const string ApiHost = "moviedatabase8.p.rapidapi.com";
    private readonly HttpClient _httpClient;

    public MovieApiService()
    {
        _httpClient = new HttpClient();
        _httpClient.DefaultRequestHeaders.Add("x-rapidapi-key", ApiKey);
        _httpClient.DefaultRequestHeaders.Add("x-rapidapi-host", ApiHost);
    }

    public async Task<List<MovieDTO>> GetMoviesAsync(string genre, int limit)
    {
        var request = new HttpRequestMessage
        {
            Method = HttpMethod.Get,
            RequestUri = new Uri($"https://{ApiHost}/Filter?Genre={genre}&Limit={limit}")
        };

        using var response = await _httpClient.SendAsync(request);
        response.EnsureSuccessStatusCode();
        var body = await response.Content.ReadAsStringAsync();
        return JsonConvert.DeserializeObject<List<MovieDTO>>(body);
    }
}