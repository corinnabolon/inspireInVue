



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

  internal TodoListItem GetTodoListItemById(int todolistitemId)
  {
    TodoListItem todolistItem = _repository.GetTodoListItemById(todolistitemId);
    if (todolistItem == null)
    {
      throw new Exception($"No list item with the id {todolistitemId}");
    }
    return todolistItem;
  }

  internal List<TodoListItem> GetTodoListItemsByTodoListId(int todoListId, string userId)
  {
    TodoList todoList = _todoListsService.GetMyTodoList(userId);
    if (todoListId != todoList.Id)
    {
      throw new Exception("Not your list items to get");
    }
    List<TodoListItem> todoListItems = _repository.GetTodoListItemsByTodoListId(todoListId);
    return todoListItems;
  }

  internal string RemoveTodoListItem(int todolistitemId, string userId)
  {
    TodoListItem todolistItem = GetTodoListItemById(todolistitemId);
    if (todolistItem.CreatorId != userId)
    {
      throw new Exception("Not your to-do list item to delete!");
    }
    _repository.RemoveTodoListItem(todolistitemId);
    return "To-do list item deleted!";
  }

  internal TodoListItem EditTodoListItem(int todolistitemId, TodoListItem todolistitemData, string userId)
  {
    TodoListItem originaltodolistItem = GetTodoListItemById(todolistitemId);

    if (originaltodolistItem.CreatorId != userId)
    {
      throw new Exception("Not your to-do list item to edit!");
    }
    originaltodolistItem.Description = todolistitemData.Description ?? originaltodolistItem.Description;
    originaltodolistItem.Completed = todolistitemData.Completed ?? originaltodolistItem.Completed;
    _repository.EditTodoListItem(originaltodolistItem);
    return originaltodolistItem;
  }
}