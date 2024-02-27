namespace inspireVue.Models;

public class Account
{
  public string Id { get; set; }
  public string Name { get; set; }
  public string Email { get; set; }
  public string Picture { get; set; }

  public bool? WantsCelsius { get; set; }

  public bool? WantsTwentyFourClock { get; set; }

  public string[] PreferredImageTypes { get; set; } = new string[0];
}


// testing testing again