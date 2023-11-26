using BlazorMovie.Models;
using System.Net.Http.Json;

namespace BlazorMovie.Services;

public class TMDBClient
{
    private readonly HttpClient _httpClient;

    public TMDBClient(HttpClient httpClient, IConfiguration configuration)
    {
        _httpClient = httpClient;

        _httpClient.BaseAddress = new Uri("https://api.themoviedb.org/3/");
        _httpClient.DefaultRequestHeaders.Accept.Add(new("application/json"));

        string apiKey = configuration["TMDBKey"] ?? throw new ArgumentNullException(configuration["TMDBKey"]);
        _httpClient.DefaultRequestHeaders.Authorization = new("Bearer", apiKey);
    }

    public Task<PopularMoviePagedResponse?> GetPopularMoviesAsync(int page = 1)
    {
        if (page < 1) page = 1;
        if (page > 500) page = 500;

        return _httpClient.GetFromJsonAsync<PopularMoviePagedResponse>($"movie/popular?page={page}");
    }

    public Task<MovieDetailsResponse?> GetMovieDetailsAsync(int id)
    {
        return _httpClient.GetFromJsonAsync<MovieDetailsResponse>($"movie/{id}");
    }
}
