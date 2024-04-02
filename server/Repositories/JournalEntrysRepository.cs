





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
    string sql = @"INSERT INTO journalentrys (description, creatorId, journalId) VALUES (@Description, @CreatorId, @JournalId);
      
      SELECT * FROM journalentrys WHERE id = LAST_INSERT_ID();";

    JournalEntry journalEntry = _db.Query<JournalEntry>(sql, journalEntryData).FirstOrDefault();

    return journalEntry;
  }


  internal JournalEntry GetJournalEntryById(int journalEntryId)
  {
    string sql = "SELECT * FROM journalentrys WHERE id = @journalEntryId;";

    JournalEntry journalEntry = _db.Query<JournalEntry>(sql, new { journalEntryId }).FirstOrDefault();
    return journalEntry;
  }

  internal List<JournalEntry> GetJournalEntrysByJournalId(int journalId)
  {
    string sql = @"SELECT * FROM journalentrys WHERE journalId = @journalId;";

    List<JournalEntry> journalEntrys = _db.Query<JournalEntry>(sql, new { journalId }).ToList();
    return journalEntrys;
  }


  internal void EditJournalEntry(JournalEntry journalEntryData)
  {
    string sql = @"UPDATE journalentrys SET description = @Description WHERE id = @Id;";

    _db.Execute(sql, journalEntryData);
  }

  internal void RemoveJournalEntry(int journalEntryId)
  {
    string sql = "DELETE FROM journalentrys WHERE id = @journalEntryId LIMIT 1;";

    _db.Execute(sql, new { journalEntryId });
  }
}