

namespace inspireVue.Repositories;

public class JournalsRepository
{
  private readonly IDbConnection _db;

  public JournalsRepository(IDbConnection db)
  {
    _db = db;
  }

  internal Journal CreateJournal(Journal journalData)
  {
    string sql = @"INSERT INTO journals (accountId) VALUES (@AccountId)
  
  SELECT * FROM journals WHERE id = LAST_INSERT_ID();";

    Journal journal = _db.Query<Journal>(sql, journalData).FirstOrDefault();

    return journal;
  }

  internal Journal GetJournalById(int journalId)
  {
    string sql = @"SELECT * from journals WHERE id = @journalId;";

    Journal journal = _db.Query<Journal>(sql, new { journalId }).FirstOrDefault();

    return journal;
  }

  internal Journal GetMyJournal(string userId)
  {
    string sql = @"SELECT * from journals WHERE accountId = @userId;";

    Journal journal = _db.Query<Journal>(sql, new { userId }).FirstOrDefault();

    return journal;
  }
}