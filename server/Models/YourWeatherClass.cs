namespace inspireVue.Models;
using System;
using System.Net.Http;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using inspireVue.Models;
using inspireVue.Services;

public class YourWeatherClass
{
  public WeatherData Data { get; set; }
}