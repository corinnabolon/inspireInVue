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
  public ActionResult<List<TodoListItem>> GetTodoListItems()
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
  //TODO get rid of the above function, change to get todolistitems by todolist ID

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



}