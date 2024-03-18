namespace inspireVue.Models;

public class JournalEntrys
{
  public int Id { get; set; }
  public DateTime CreatedAt { get; set; }

  public DateTime UpdatedAt { get; set; }
  public bool? Completed { get; set; }
  public string Description { get; set; }

  public string CreatorId { get; set; }

  public int JournalId { get; set; }
}
