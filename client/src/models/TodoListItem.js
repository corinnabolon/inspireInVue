export class TodoListItem {
  constructor(data) {
    this.id = data.id
    this.createdAt = data.createdAt
    this.updatedAt = data.updatedAt
    this.completed = data.completed
    this.description = data.description
    this.creatorId = data.creatorId
    this.todoListId = data.todoListId
  }
}
