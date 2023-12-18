import { AppState } from "../AppState.js"
import { TodoList } from "../models/TodoList.js"
import { logger } from "../utils/Logger.js"
import { api } from "./AxiosService.js"
import { todoListItemsService } from "./TodoListItemsService.js"

class TodoListsService {

  async getMyList() {
    try {
      const res = await api.get("account/todolists")
      const myListData = new TodoList(res.data)
      logger.log("myListData", myListData)
      AppState.todoList = myListData
      todoListItemsService.getMyListItems(myListData.id)
    } catch (error) {
      this.createMyList(AppState.account.id)
    }
  }

  async createMyList(accountId) {
    const res = await api.post("api/todolists", accountId)
    const myListData = new TodoList(res.data)
    logger.log("myListData", myListData)
    AppState.todoList = myListData
    todoListItemsService.getMyListItems(myListData.id)
  }

}

export const todoListsService = new TodoListsService()