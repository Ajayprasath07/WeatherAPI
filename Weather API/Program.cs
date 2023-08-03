using Newtonsoft.Json;
using Newtonsoft.Json.Linq;


public class OpenWeatherMap
{
    public string? apiResponse { get; set; }

    public Dictionary<string, string>? cities { get; set; }

}

public class Coord
{
    public double lon { get; set; }
    public double lat { get; set; }
}

public class Weather
{
    public int id { get; set; }
    public string? main { get; set; }
    public string? description { get; set; }
    public string? icon { get; set; }
}

public class Main
{
    public double temp { get; set; }
    public int pressure { get; set; }
    public int humidity { get; set; }
    public double temp_min { get; set; }
    public double temp_max { get; set; }
}

public class Wind
{
    public double speed { get; set; }
    public int deg { get; set; }
}

public class Clouds
{
    public int all { get; set; }
}

public class Sys
{
    public int type { get; set; }
    public int id { get; set; }
    public double message { get; set; }
    public string? country { get; set; }
    public int sunrise { get; set; }
    public int sunset { get; set; }
}

public class ResponseWeather
{
    public Coord? coord { get; set; }
    public List<Weather>? weather { get; set; }
    public string? @base { get; set; }
    public Main? main { get; set; }
    public int visibility { get; set; }
    public Wind? wind { get; set; }
    public Clouds? clouds { get; set; }
    public int dt { get; set; }
    public Sys? sys { get; set; }
    public int id { get; set; }
    public string? name { get; set; }
    public int cod { get; set; }

}


class Program{
    public static async Task Main(string[] args){

        using var client = new HttpClient();
        string apiKey = "2596fc1ccddddd3fd713f4ec20fc4a11";
        string query = "Washington DC";
        var content = await client.GetStringAsync(
            $"http://api.openweathermap.org/data/2.5/weather?appid={apiKey}&q={query}"
        ).ConfigureAwait(false);

        var parsedObject = JObject.Parse(content);
        var weather = parsedObject?["weather"]?[0]?.ToString();
        var mainContent = parsedObject?["main"]?.ToString();

        Weather? weatherCondition = null;
        Main? mainTemp = null;

        if (weather is not null)
            weatherCondition = JsonConvert.DeserializeObject<Weather>(weather);
        if (mainContent is not null)
            mainTemp = JsonConvert.DeserializeObject<Main>(mainContent); 

        Console.WriteLine($"Weather:{weatherCondition?.main}");
        Console.WriteLine($"Temperature in Kelvin:{mainTemp?.temp}");

        client.Dispose();
    }
}

