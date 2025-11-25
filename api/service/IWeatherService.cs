using api.Models;

namespace api.Services;

public interface IWeatherService
{
    Task<WeatherResponse?> GetWeatherAsync(string zipCode, string countryCode, CancellationToken cancellationToken = default);
}

