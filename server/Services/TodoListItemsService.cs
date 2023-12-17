



namespace inspireVue.Services;

public class TodoListItemsService
{
  private readonly TodoListItemsRepository _repository;

  private readonly TodoListsService _todoListsService;

  public TodoListItemsService(TodoListItemsRepository repository, TodoListsService todoListsService)
  {
    _repository = repository;
    _todoListsService = todoListsService;
  }

  internal TodoListItem CreateTodoListItem(TodoListItem todoListItemData, string userId)
  {
    TodoList todoList = _todoListsService.GetMyTodoList(userId);
    if (todoList.AccountId != userId)
    {
      throw new Exception("Not your list to add to");
    }
    todoListItemData.TodoListId = todoList.Id;
    TodoListItem todoListItem = _repository.CreateTodoListItem(todoListItemData);
    return todoListItem;
  }



  internal List<TodoListItem> GetTodoListItems()
  {
    List<TodoListItem> todoListItems = _repository.GetTodoListItems();
    return todoListItems;
  }

  internal List<TodoListItem> getTodoListItemsByTodoListId(int todoListId, string userId)
  {
    TodoList todoList = _todoListsService.GetMyTodoList(userId);
    if (todoListId != todoList.Id)
    {
      throw new Exception("Not your list items to get");
    }
    List<TodoListItem> todoListItems = _repository.getTodoListItemsByTodoListId(todoListId);
    return todoListItems;
  }
}