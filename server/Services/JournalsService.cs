
namespace inspireVue.Services;

public class JournalsService
{
  private readonly JournalsRepository _repository;

  public JournalsService(JournalsRepository repository)
  {
    _repository = repository;
  }


  internal Journal CreateJournal(Journal journalData)
  {
    Journal journal = _repository.CreateJournal(journalData);
    return journal;
  }

  internal Journal GetJournalById(int journalId, string userId)
  {
    Journal journal = _repository.GetJournalById(journalId);

    if (journal.AccountId != userId)
    {
      throw new Exception("Not your journal to get");
    }
    if (journal == null)
    {
      throw new Exception($"No list with the ID {journalId}");
    }
    return journal;
  }

  internal Journal GetMyJournal(string userId)
  {
    Journal journal = _repository.GetMyJournal(userId);
    if (journal == null)
    {
      throw new Exception("You don't have a journal");
    }
    return journal;
  }
}
