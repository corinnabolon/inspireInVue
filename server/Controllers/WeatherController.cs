namespace inspireVue.Controllers;
using System;
using System.Net.Http;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using inspireVue.Models;
using inspireVue.Services;
using Newtonsoft.Json;

[Controller]
public class WeatherController : Controller
{
  private readonly WeatherService _weatherService;

  public WeatherController(WeatherService weatherService)
  {
    _weatherService = weatherService;
  }


  [HttpGet]
  public async Task<IActionResult> Index()
  {
    var currentWeatherJson = await _weatherService.GetCurrentWeather();

    if (currentWeatherJson == null)
    {
      Console.WriteLine("API request failed or returned null.");
      return View("Error");
    }

    var currentWeather = JsonConvert.DeserializeObject<YourWeatherClass>(currentWeatherJson);

    if (currentWeather == null)
    {
      Console.WriteLine("Deserialization failed.");
      return View("Error");
    }

    if (currentWeather?.Data?.Values == null)
    {
      return View("Error");
    }


    // return View(currentWeather);
    //The above would just return it as is; the below code maps it to my model

    var weatherViewModel = new WeatherViewModel
    {
      Humidity = currentWeather.Data.Values.Humidity,
      PrecipitationProbability = currentWeather.Data.Values.PrecipitationProbability,
      Temperature = currentWeather.Data.Values.Temperature,
      // Map other properties as needed
    };

    // Console.WriteLine($"Humidity: {currentWeather.Values.Humidity}");
    // Console.WriteLine($"Precipitation Probability: {currentWeather.Values.PrecipitationProbability}");
    // Console.WriteLine($"Temperature: {currentWeather.Values.Temperature}");

    // Pass the mapped data to the view
    return View(weatherViewModel);
  }

}