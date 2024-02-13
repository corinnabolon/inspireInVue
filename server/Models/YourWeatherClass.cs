namespace inspireVue.Models
{
  public class YourWeatherClass
  {
    public WeatherValues Values { get; set; }
  }

  public class WeatherValues
  {
    public decimal Humidity { get; set; }
    public decimal PrecipitationProbability { get; set; }
    public decimal Temperature { get; set; }
  }
}