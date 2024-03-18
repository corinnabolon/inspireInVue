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

  // [HttpGet]

  // public ActionResult<JournalEntry> GetJournalEntry()
  // {
  //   try
  //   {
  //     JournalEntry journalEntry = _journalEntrysService.GetJournalEntry();
  //     return Ok(journalEntry);
  //   }
  //   catch (Exception exception)
  //   {
  //     return BadRequest(exception.Message);
  //   }
  // }

  //TODO: add get and delete methods

}