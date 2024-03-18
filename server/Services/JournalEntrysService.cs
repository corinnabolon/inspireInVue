

namespace inspireVue.Services;

public class JournalEntrysService
{
  private readonly JournalEntrysRepository _repository;

  private readonly JournalsService _journalsService;

  public JournalEntrysService(JournalEntrysRepository repository, JournalsService journalsService)
  {
    _repository = repository;
    _journalsService = journalsService;
  }

  internal JournalEntry CreateJournalEntry(JournalEntry journalEntryData, string userId)
  {
    Journal journal = _journalsService.GetMyJournal(userId);
    if (journal.AccountId != userId)
    {
      throw new Exception("Not your journal to write in");
    }
    journalEntryData.JournalId = journal.Id;
    JournalEntry journalEntry = _repository.CreateJournalEntry(journalEntryData);
    return journalEntry;
  }

  //   internal JournalEntry GetJournalEntry()
  // {
  //   JournalEntry journalEntry = _repository.GetJournalEntry();
  //   return journalEntry;
  // }
}