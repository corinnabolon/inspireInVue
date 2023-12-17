

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

  internal TodoList GetMyTodoList(string userId)
  {
    TodoList todoList = _repository.GetMyTodoList(userId);
    return todoList;
  }
}
