

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

    //TODO: add populating the items on this--will need a getListItemsbyTodoListID like the GetKeepsByVaultId
    //TODO: on login on the front end get the list on the account which also gets the items (on back end)--only if no list yet (first login), create it

    TodoList todoList = _db.Query<TodoList>(sql, new { userId }).FirstOrDefault();

    return todoList;
  }

  internal TodoList GetTodoListById(int todoListId)
  {
    string sql = @"SELECT * FROM todolists WHERE id = @todoListId;";
    //TODO: populate list items?

    TodoList todoList = _db.Query<TodoList>(sql, new { todoListId }).FirstOrDefault();

    return todoList;
  }
}

