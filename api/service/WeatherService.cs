using System.Net.Http.Json;
using api.Models;

namespace api.Services;

public class WeatherService : IWeatherService
{
    private readonly HttpClient _httpClient;
    private readonly IConfiguration _configuration;
    private readonly ILogger<WeatherService> _logger;

    public WeatherService(HttpClient httpClient, IConfiguration configuration, ILogger<WeatherService> logger)
    {
        _httpClient = httpClient;
        _configuration = configuration;
        _logger = logger;
    }

    public async Task<WeatherResponse?> GetWeatherAsync(string zipCode, string countryCode, CancellationToken cancellationToken = default)
    {
        if (string.IsNullOrWhiteSpace(zipCode))
        {
            throw new ArgumentException("Zip code must be provided.", nameof(zipCode));
        }

        if (string.IsNullOrWhiteSpace(countryCode))
        {
            throw new ArgumentException("Country code must be provided.", nameof(countryCode));
        }

        var apiKey = _configuration["OpenWeatherMapKey"];
        if (string.IsNullOrWhiteSpace(apiKey))
        {
            throw new InvalidOperationException("The OpenWeatherMapKey environment variable is not set.");
        }

        var requestUri = $"weather?zip={zipCode.Trim()},{countryCode.Trim()}&appid={apiKey}&units=imperial";
        using var response = await _httpClient.GetAsync(requestUri, cancellationToken);

        if (!response.IsSuccessStatusCode)
        {
            var errorBody = await response.Content.ReadAsStringAsync(cancellationToken);
            _logger.LogWarning("OpenWeatherMap request failed ({StatusCode}): {Body}", response.StatusCode, errorBody);
            throw new HttpRequestException("Unable to retrieve weather data from OpenWeatherMap.");
        }

        return await response.Content.ReadFromJsonAsync<WeatherResponse>(cancellationToken: cancellationToken);
    }
}

