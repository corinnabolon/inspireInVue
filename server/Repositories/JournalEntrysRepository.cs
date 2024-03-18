
namespace inspireVue.Repositories;

public class JournalEntrysRepository
{
  private readonly IDbConnection _db;

  public JournalEntrysRepository(IDbConnection db)
  {
    _db = db;
  }

  internal JournalEntry CreateJournalEntry(JournalEntry journalEntryData)
  {
    string sql = @"INSERT INTO journals (description, creatorId, journalId) VALUES (@Description, @CreatorId, @TodoListId);
      
      SELECT * FROM journalentrys WHERE id = LAST_INSERT_ID();";

    JournalEntry journalEntry = _db.Query<JournalEntry>(sql, journalEntryData).FirstOrDefault();

    return journalEntry;
  }
}