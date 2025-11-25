using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace api.Models;

public class WeatherRequest
{
    [Required]
    [JsonPropertyName("zip")]
    public string ZipCode { get; set; } = string.Empty;

    [Required]
    [JsonPropertyName("countryCode")]
    public string CountryCode { get; set; } = string.Empty;
}

