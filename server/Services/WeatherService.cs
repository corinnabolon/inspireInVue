namespace inspireVue.Services;
using System;
using System.Net.Http;
using System.Threading.Tasks;
using Microsoft.Extensions.Configuration;

public class WeatherService
{
  private readonly IConfiguration _configuration;

  public WeatherService(IConfiguration configuration)
  {
    _configuration = configuration;
  }

  public async Task<string> GetCurrentWeather()
  {
    string apiKey = _configuration["WeatherApiSettings:WeatherApiKey"];
    string apiUrl = $"https://api.tomorrow.io/v4/weather/realtime?location=corvallis&apikey={apiKey}";

    using (HttpClient httpClient = new HttpClient())
    {
      HttpResponseMessage response = await httpClient.GetAsync(apiUrl);

      if (response.IsSuccessStatusCode)
      {
        return await response.Content.ReadAsStringAsync();
      }
      else
      {
        Console.WriteLine($"Error: {response.StatusCode}");
        return null;
      }
    }

  }

}