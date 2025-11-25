using api.Models;
using api.Services;
using Microsoft.AspNetCore.Mvc;

namespace api.Controllers;

[ApiController]
[Route("api/[controller]")]
public class WeatherController : ControllerBase
{
    private readonly IWeatherService _weatherService;

    public WeatherController(IWeatherService weatherService)
    {
        _weatherService = weatherService;
    }

    [HttpPost]
    public async Task<ActionResult<WeatherResponse>> GetWeather([FromBody] WeatherRequest request, CancellationToken cancellationToken)
    {
        var weatherData = await _weatherService.GetWeatherAsync(request.ZipCode, request.CountryCode, cancellationToken);
        return Ok(weatherData);
    }
}

