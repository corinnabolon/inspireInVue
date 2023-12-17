


namespace inspireVue.Repositories;

public class TodoListItemsRepository
{
  private readonly IDbConnection _db;

  public TodoListItemsRepository(IDbConnection db)
  {
    _db = db;
  }

  internal TodoListItem CreateTodoListItem(TodoListItem todoListItemData)
  {
    string sql = @"INSERT INTO todolistitems (description, creatorId, todoListId) VALUES (@Description, @CreatorId, @TodoListId);

      SELECT * FROM todolistitems WHERE id = LAST_INSERT_ID();";

    TodoListItem todoListItem = _db.Query<TodoListItem>(sql, todoListItemData).FirstOrDefault();

    return todoListItem;
  }

  internal List<TodoListItem> GetTodoListItems()
  {
    string sql = "SELECT * FROM todolistitems;";

    List<TodoListItem> todoListItems = _db.Query<TodoListItem>(sql).ToList();

    return todoListItems;
  }
}