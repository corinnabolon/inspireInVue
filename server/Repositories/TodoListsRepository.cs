
namespace inspireVue.Repositories;

public class TodoListsRepository
{
  private readonly IDbConnection _db;

  public TodoListsRepository(IDbConnection db)
  {
    _db = db;
  }

  internal TodoList CreateTodoList(TodoList todoListData)
  {
    string sql = @"INSERT INTO todolists (accountId) VALUES (@AccountId);
      
      SELECT * FROM todolists WHERE id = LAST_INSERT_ID();";

    TodoList todoList = _db.Query<TodoList>(sql, todoListData).FirstOrDefault();

    return todoList;
  }

  internal TodoList GetMyTodoList(string userId)
  {
    string sql = @"SELECT * from todolists WHERE accountId = @userId;";

    TodoList todoList = _db.Query<TodoList>(sql, new { userId }).FirstOrDefault();

    return todoList;
  }
}

