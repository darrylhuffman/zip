using System.Text.Json.Serialization;

namespace api.Models;

public class WeatherResponse
{
    [JsonPropertyName("coord")]
    public Coordinates? Coord { get; set; }

    [JsonPropertyName("weather")]
    public List<WeatherCondition>? Weather { get; set; }

    [JsonPropertyName("base")]
    public string? Base { get; set; }

    [JsonPropertyName("main")]
    public MainWeatherMetrics? Main { get; set; }

    [JsonPropertyName("visibility")]
    public int? Visibility { get; set; }

    [JsonPropertyName("wind")]
    public Wind? Wind { get; set; }

    [JsonPropertyName("clouds")]
    public Clouds? Clouds { get; set; }

    [JsonPropertyName("rain")]
    public Precipitation? Rain { get; set; }

    [JsonPropertyName("snow")]
    public Precipitation? Snow { get; set; }

    [JsonPropertyName("dt")]
    public long? Timestamp { get; set; }

    [JsonPropertyName("sys")]
    public SysInfo? Sys { get; set; }

    [JsonPropertyName("timezone")]
    public int? Timezone { get; set; }

    [JsonPropertyName("id")]
    public int? CityId { get; set; }

    [JsonPropertyName("name")]
    public string? CityName { get; set; }

    [JsonPropertyName("cod")]
    public int? Code { get; set; }
}

public class Coordinates
{
    [JsonPropertyName("lon")]
    public double? Longitude { get; set; }

    [JsonPropertyName("lat")]
    public double? Latitude { get; set; }
}

public class WeatherCondition
{
    [JsonPropertyName("id")]
    public int? Id { get; set; }

    [JsonPropertyName("main")]
    public string? Main { get; set; }

    [JsonPropertyName("description")]
    public string? Description { get; set; }

    [JsonPropertyName("icon")]
    public string? Icon { get; set; }
}

public class MainWeatherMetrics
{
    [JsonPropertyName("temp")]
    public double? Temperature { get; set; }

    [JsonPropertyName("feels_like")]
    public double? FeelsLike { get; set; }

    [JsonPropertyName("pressure")]
    public int? Pressure { get; set; }

    [JsonPropertyName("humidity")]
    public int? Humidity { get; set; }

    [JsonPropertyName("temp_min")]
    public double? TempMin { get; set; }

    [JsonPropertyName("temp_max")]
    public double? TempMax { get; set; }

    [JsonPropertyName("sea_level")]
    public int? SeaLevel { get; set; }

    [JsonPropertyName("grnd_level")]
    public int? GroundLevel { get; set; }
}

public class Wind
{
    [JsonPropertyName("speed")]
    public double? Speed { get; set; }

    [JsonPropertyName("deg")]
    public int? Degrees { get; set; }

    [JsonPropertyName("gust")]
    public double? Gust { get; set; }
}

public class Clouds
{
    [JsonPropertyName("all")]
    public int? All { get; set; }
}

public class Precipitation
{
    [JsonPropertyName("1h")]
    public double? OneHour { get; set; }
}

public class SysInfo
{
    [JsonPropertyName("type")]
    public int? Type { get; set; }

    [JsonPropertyName("id")]
    public int? Id { get; set; }

    [JsonPropertyName("message")]
    public double? Message { get; set; }

    [JsonPropertyName("country")]
    public string? Country { get; set; }

    [JsonPropertyName("sunrise")]
    public long? Sunrise { get; set; }

    [JsonPropertyName("sunset")]
    public long? Sunset { get; set; }
}

