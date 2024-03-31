namespace inspireVue.Controllers;

[ApiController]
[Route("api/[controller]")]

public class JournalEntrysController : ControllerBase
{
  private readonly Auth0Provider _auth0Provider;

  private readonly JournalEntrysService _journalEntrysService;
  public JournalEntrysController(Auth0Provider auth0Provider, JournalEntrysService journalEntrysService)
  {
    _auth0Provider = auth0Provider;
    _journalEntrysService = journalEntrysService;
  }

  [Authorize]
  [HttpPost]

  public async Task<ActionResult<JournalEntry>> CreateJournalEntry([FromBody] JournalEntry journalEntryData)
  {
    try
    {
      Account userInfo = await _auth0Provider.GetUserInfoAsync<Account>(HttpContext);
      journalEntryData.CreatorId = userInfo.Id;
      string userId = userInfo.Id;
      JournalEntry journalEntry = _journalEntrysService.CreateJournalEntry(journalEntryData, userId);
      return Ok(journalEntryData);
    }
    catch (Exception exception)
    {
      return BadRequest(exception.Message);
    }
  }

  [Authorize]
  [HttpPut("{journalEntryId}")]

  public async Task<ActionResult<JournalEntry>> EditJournalEntry(int journalEntryId, [FromBody] JournalEntry journalEntryData)
  {
    try
    {
      Account userInfo = await _auth0Provider.GetUserInfoAsync<Account>(HttpContext);
      string userId = userInfo.Id;
      JournalEntry journalEntry = _journalEntrysService.EditJournalEntry(journalEntryId, journalEntryData, userId);
      return Ok(journalEntry);
    }
    catch (Exception exception)
    {
      return BadRequest(exception.Message);
    }
  }

  [Authorize]
  [HttpDelete("{journalEntryId}")]

  public async Task<ActionResult<string>> RemoveJournalEntry(int journalEntryId)
  {
    try
    {
      Account userInfo = await _auth0Provider.GetUserInfoAsync<Account>(HttpContext);
      string userId = userInfo.Id;
      string message = _journalEntrysService.RemoveJournalEntry(journalEntryId, userId);
      return Ok(message);
    }
    catch (Exception exception)
    {
      return BadRequest(exception.Message);
    }
  }



  //TODO: add get and delete methods

}