






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

  internal TodoListItem GetTodoListItemById(int todolistitemId)
  {
    string sql = "SELECT * FROM todolistitems WHERE id = @todolistitemId;";

    TodoListItem todolistItem = _db.Query<TodoListItem>(sql, new { todolistitemId }).FirstOrDefault();
    return todolistItem;
  }

  internal List<TodoListItem> GetTodoListItems()
  {
    string sql = "SELECT * FROM todolistitems;";

    List<TodoListItem> todoListItems = _db.Query<TodoListItem>(sql).ToList();

    return todoListItems;
  }

  internal List<TodoListItem> GetTodoListItemsByTodoListId(int todoListId)
  {
    string sql = @"SELECT * FROM todolistitems WHERE todoListId = @todoListId;";

    List<TodoListItem> todoListItems = _db.Query<TodoListItem>(sql, new { todoListId }).ToList();

    return todoListItems;
  }

  internal void EditTodoListItem(TodoListItem todolistitemData)
  {
    string sql = @"UPDATE todolistitems
      SET
      description = @Description,
      completed = @Completed
      WHERE id = @Id;";

    _db.Execute(sql, todolistitemData);
  }

  internal void RemoveTodoListItem(int todolistitemId)
  {
    string sql = "DELETE FROM todolistitems WHERE id = @todolistitemId LIMIT 1;";

    _db.Execute(sql, new { todolistitemId });
  }
}