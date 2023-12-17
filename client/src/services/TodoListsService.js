import { AppState } from "../AppState.js"
import { TodoList } from "../models/TodoList.js"
import { logger } from "../utils/Logger.js"
import { api } from "./AxiosService.js"
import { todoListItemsService } from "./TodoListItemsService.js"

class TodoListsService {

  async getMyList() {
    const res = await api.get("account/todolists")
    logger.log("mylist", res.data)
    const myListData = new TodoList(res.data)
    logger.log(myListData)
    AppState.TodoList = myListData
    todoListItemsService.getMyListItems(myListData.id)
  }

}

export const todoListsService = new TodoListsService()