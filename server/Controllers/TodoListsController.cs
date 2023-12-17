namespace inspireVue.Controllers;

[ApiController]
[Route("api/[controller]")]

public class TodoListsController : ControllerBase
{
  private readonly Auth0Provider _auth0Provider;

  private readonly TodoListsService _todolistsService;

  public TodoListsController(Auth0Provider auth0Provider, TodoListsService todoListsService)
  {
    _auth0Provider = auth0Provider;
    _todolistsService = todoListsService;
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


}