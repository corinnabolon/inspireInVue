namespace inspireVue.Models;
using System;
using System.Net.Http;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using inspireVue.Models;
using inspireVue.Services;

public class WeatherViewModel
{
  public decimal Humidity { get; set; }
  public decimal PrecipitationProbability { get; set; }
  public decimal Temperature { get; set; }
}

// {"data":{"time":"2024-02-13T04:47:00Z","values":{"cloudBase":null,"cloudCeiling":null,"cloudCover":0,"dewPoint":2.88,"freezingRainIntensity":0,"humidity":88,"precipitationProbability":0,"pressureSurfaceLevel":1008.65,"rainIntensity":0,"sleetIntensity":0,"snowIntensity":0,"temperature":4.69,"temperatureApparent":4.69,"uvHealthConcern":0,"uvIndex":0,"visibility":16,"weatherCode":1000,"windDirection":321.88,"windGust":0.38,"windSpeed":0}},"location":{"lat":44.56456756591797,"lon":-123.26204681396484,"name":"Corvallis, Benton County, Oregon, United States","type":"administrative"}}