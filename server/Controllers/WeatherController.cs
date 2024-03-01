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
  private readonly Auth0Provider _auth0Provider;
  private readonly WeatherService _weatherService;

  public WeatherController(WeatherService weatherService, Auth0Provider auth0Provider)
  {
    _weatherService = weatherService;
    _auth0Provider = auth0Provider;
  }


  // [HttpGet]
  // public async Task<IActionResult> Index()
  // {
  //   Account userInfo = await _auth0Provider.GetUserInfoAsync<Account>(HttpContext);
  //   var preferredLocation = userInfo.PreferredLocation;
  //   var currentWeatherJson = await _weatherService.GetCurrentWeather(preferredLocation);

  //   if (currentWeatherJson == null)
  //   {
  //     Console.WriteLine("API request failed or returned null.");
  //     return View("Error");
  //   }

  //   var currentWeather = JsonConvert.DeserializeObject<YourWeatherClass>(currentWeatherJson);

  //   if (currentWeather == null)
  //   {
  //     Console.WriteLine("Deserialization failed.");
  //     return BadRequest("Deserialization failed.");
  //   }

  //   if (currentWeather?.Data?.Values == null)
  //   {
  //     return BadRequest("No weather API response?");
  //   }


  [HttpPost]
  [Route("api/weather")]
  public async Task<IActionResult> GetWeatherData([FromBody] WeatherRequestModel model)
  {
    var preferredLocation = model.PreferredLocation;

    // Use preferredLocation to fetch weather data
    var currentWeatherJson = await _weatherService.GetCurrentWeather(preferredLocation);

    if (currentWeatherJson == null)
    {
      Console.WriteLine("API request failed or returned null.");
      return View("Error");
    }

    var currentWeather = JsonConvert.DeserializeObject<YourWeatherClass>(currentWeatherJson);

    if (currentWeather == null)
    {
      Console.WriteLine("Deserialization failed.");
      return BadRequest("Deserialization failed.");
    }

    if (currentWeather?.Data?.Values == null)
    {
      return BadRequest("No weather API response?");
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
    // return View(weatherViewModel);

    //The above would have returned the view in my Views/Index.cshtml

    return Ok(weatherViewModel);
  }

  public class WeatherRequestModel
  {
    public string PreferredLocation { get; set; }
  }

}