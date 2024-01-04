namespace inspireVue.Controllers;

[ApiController]
[Route("api/[controller]")]

public class TodoListItemsController : ControllerBase
{
  private readonly Auth0Provider _auth0Provider;

  private readonly TodoListItemsService _todolistItemsService;

  public TodoListItemsController(Auth0Provider auth0Provider, TodoListItemsService todoListItemsService)
  {
    _auth0Provider = auth0Provider;
    _todolistItemsService = todoListItemsService;
  }

  [HttpGet]
  public ActionResult<List<TodoListItem>> GetTodoListItem()
  {
    try
    {
      List<TodoListItem> todoListItems = _todolistItemsService.GetTodoListItems();
      return Ok(todoListItems);
    }
    catch (Exception exception)
    {
      return BadRequest(exception.Message);
    }
  }

  [Authorize]
  [HttpPost]
  public async Task<ActionResult<TodoListItem>> CreateTodoListItem([FromBody] TodoListItem todoListItemData)
  {
    try
    {
      Account userInfo = await _auth0Provider.GetUserInfoAsync<Account>(HttpContext);
      todoListItemData.CreatorId = userInfo.Id;
      string userId = userInfo.Id;
      TodoListItem todoListItem = _todolistItemsService.CreateTodoListItem(todoListItemData, userId);
      return Ok(todoListItemData);
    }
    catch (Exception exception)
    {
      return BadRequest(exception.Message);
    }
  }

  [Authorize]
  [HttpPut("{todolistitemId}")]

  public async Task<ActionResult<TodoListItem>> EditTodoListItem(int todolistitemId, [FromBody] TodoListItem todolistitemData)
  {
    try
    {
      Account userInfo = await _auth0Provider.GetUserInfoAsync<Account>(HttpContext);
      string userId = userInfo.Id;
      TodoListItem todolistItem = _todolistItemsService.EditTodoListItem(todolistitemId, todolistitemData, userId);
      return Ok(todolistItem);
    }
    catch (Exception exception)
    {
      return BadRequest(exception.Message);
    }

  }





  [Authorize]
  [HttpDelete("{todolistitemId}")]

  public async Task<ActionResult<string>> RemoveTodoListItem(int todolistitemId)
  {
    try
    {
      Account userInfo = await _auth0Provider.GetUserInfoAsync<Account>(HttpContext);
      string userId = userInfo.Id;
      string message = _todolistItemsService.RemoveTodoListItem(todolistitemId, userId);
      return Ok(message);
    }
    catch (Exception exception)
    {
      return BadRequest(exception.Message);
    }
  }



}