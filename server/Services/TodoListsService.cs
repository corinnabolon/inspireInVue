


namespace inspireVue.Services;

public class TodoListsService
{
  private readonly TodoListsRepository _repository;

  public TodoListsService(TodoListsRepository repository)
  {
    _repository = repository;
  }


  internal TodoList CreateTodoList(TodoList todoListData)
  {
    TodoList todoList = _repository.CreateTodoList(todoListData);
    return todoList;
  }

  internal TodoList GetTodoListById(int todoListId, string userId)
  {
    TodoList todoList = _repository.GetTodoListById(todoListId);
    if (todoList.AccountId != userId)
    {
      throw new Exception("Not your list to get");
    }
    if (todoList == null)
    {
      throw new Exception($"No list with the ID {todoListId}");
    }
    return todoList;
  }

  internal TodoList GetMyTodoList(string userId)
  {
    TodoList todoList = _repository.GetMyTodoList(userId);
    if (todoList == null)
    {
      throw new Exception("You don't have a todo list");
    }
    return todoList;
  }

}
