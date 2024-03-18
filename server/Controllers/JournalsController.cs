using Microsoft.AspNetCore.Mvc.Infrastructure;

namespace inspireVue.Controllers;

[ApiController]
[Route("api/[controller]")]

public class JournalsController : ControllerBase
{
  private readonly
  Auth0Provider _auth0Provider;

  private readonly JournalsService _journalsService;

  private readonly JournalEntrysService _journalentrysService;
  public JournalsController(Auth0Provider auth0Provider, JournalsService journalsService, JournalEntrysService journalEntrysService)
  {
    _auth0Provider = auth0Provider;
    _journalsService = journalsService;
    _journalentrysService = journalEntrysService;
  }

  [Authorize]
  [HttpPost]

  public async Task<ActionResult<Journal>> CreateJournal([FromBody] Journal journalData)
  {
    try
    {
      Account userInfo = await _auth0Provider.GetUserInfoAsync<Account>(HttpContext);
      journalData.AccountId = userInfo.Id;
      Journal journal = _journalsService.CreateJournal(journalData);
      return Ok(journal);
    }
    catch (Exception exception)
    {
      return BadRequest(exception.Message);
    }
  }

  [Authorize]
  [HttpGet("{journalId}")]
  public async Task<ActionResult<Journal>> GetJournalById(int journalId)
  {
    try
    {
      Account userInfo = await _auth0Provider.GetUserInfoAsync<Account>(HttpContext);
      string userId = userInfo.Id;
      Journal journal = _journalsService.GetJournalById(journalId, userId);
      return Ok(journal);
    }
    catch (Exception exception)
    {
      return BadRequest(exception.Message);
    }
  }

  [Authorize]
  [HttpGet("{journalId}/journalEntrys")]

  public async Task<ActionResult<JournalEntry>> GetJournalEntryByJournalId(int journalId)
  {
    try
    {
      Account userInfo = await _auth0Provider.GetUserInfoAsync<Account>(HttpContext);
      string userId = userInfo.Id;
      JournalEntry journalEntry = _journalentrysService.GetJournalEntryByJournalId(journalId, userId);
      return Ok(journalEntry);
    }
    catch (Exception exception)
    {
      return BadRequest(exception.Message);
    }
  }



}