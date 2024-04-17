

using System.Security.Cryptography.X509Certificates;

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
    // List<JournalEntry> testJournalEntries = GetJournalEntrysByJournalId(journal.Id, userId);
    // if (testJournalEntries.Any(entry => entry.CreatedAt.Date == DateTime.Today))
    // {
    //   throw new Exception("You already have a journal entry for today.");
    // }
    journalEntryData.JournalId = journal.Id;
    journalEntryData.CreatedAt = DateTime.Today;
    JournalEntry journalEntry = _repository.CreateJournalEntry(journalEntryData);
    return journalEntry;
  }

  internal JournalEntry GetJournalEntryById(int journalEntryId)
  {
    JournalEntry journalEntry = _repository.GetJournalEntryById(journalEntryId);
    if (journalEntry == null)
    {
      throw new Exception($"No journal entry with the id {journalEntryId}");
    }
    return journalEntry;
  }

  internal List<JournalEntry> GetJournalEntrysByJournalId(int journalId, string userId)
  {
    Journal journal = _journalsService.GetMyJournal(userId);
    if (journalId != journal.Id)
    {
      throw new Exception("Not your journal entry to get");
    }
    List<JournalEntry> journalEntrys = _repository.GetJournalEntrysByJournalId(journalId);
    return journalEntrys;
  }


  internal JournalEntry EditJournalEntry(int journalEntryId, JournalEntry journalEntryData, string userId)
  {
    JournalEntry originalJournalEntry = GetJournalEntryById(journalEntryId);

    if (originalJournalEntry.CreatorId != userId)
    {
      throw new Exception("Not your journal entry to edit!");
    }
    originalJournalEntry.Description = journalEntryData.Description ?? originalJournalEntry.Description;
    _repository.EditJournalEntry(originalJournalEntry);
    return originalJournalEntry;
  }

  internal string RemoveJournalEntry(int journalEntryId, string userId)
  {
    JournalEntry journalEntry = GetJournalEntryById(journalEntryId);
    if (journalEntry.CreatorId != userId)
    {
      throw new Exception("Not your journal entry to delete!");
    }
    _repository.RemoveJournalEntry(journalEntryId);
    return "Journal entry deleted!";
  }
}