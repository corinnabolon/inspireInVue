namespace inspireVue.Controllers;

[ApiController]
[Route("api/[controller]")]

public class TodoListsController : ControllerBase
{
  private readonly Auth0Provider _auth0Provider;

  private readonly TodoListsService _todolistsService;

  private readonly TodoListItemsService _todolistitemsService;

  public TodoListsController(Auth0Provider auth0Provider, TodoListsService todoListsService, TodoListItemsService todolistitemsService)
  {
    _auth0Provider = auth0Provider;
    _todolistsService = todoListsService;
    _todolistitemsService = todolistitemsService;
  }

  [Authorize]
  [HttpPost]
  public async Task<ActionResult<TodoList>> CreateTodoList([FromBody] TodoList todoListData)
  {
    try
    {
      Account userInfo = await _auth0Provider.GetUserInfoAsync<Account>(HttpContext);
      todoListData.AccountId = userInfo.Id;
      TodoList todoList = _todolistsService.CreateTodoList(todoListData);
      return Ok(todoList);
    }
    catch (Exception exception)
    {
      return BadRequest(exception.Message);
    }
  }

  [Authorize]
  [HttpGet("{todoListId}")]
  public async Task<ActionResult<TodoList>> GetTodoListById(int todoListId)
  {
    try
    {
      Account userInfo = await _auth0Provider.GetUserInfoAsync<Account>(HttpContext);
      string userId = userInfo.Id;
      TodoList todoList = _todolistsService.GetTodoListById(todoListId, userId);
      return Ok(todoList);
    }
    catch (Exception exception)
    {
      return BadRequest(exception.Message);
    }
  }

  [Authorize]
  [HttpGet("{todoListId}/todolistitems")]

  public async Task<ActionResult<List<TodoListItem>>> getTodoListItemsByTodoListId(int todoListId)
  {
    try
    {
      Account userInfo = await _auth0Provider.GetUserInfoAsync<Account>(HttpContext);
      string userId = userInfo.Id;
      List<TodoListItem> todoListItems = _todolistitemsService.GetTodoListItemsByTodoListId(todoListId, userId);
      return Ok(todoListItems);
    }
    catch (Exception exception)
    {
      return BadRequest(exception.Message);
    }
  }


}