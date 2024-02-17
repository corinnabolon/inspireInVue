import { AppState } from "../AppState.js"
import { TodoListItem } from "../models/TodoListItem.js"
import { logger } from "../utils/Logger.js"
import { api } from "./AxiosService.js"


class TodoListItemsService {

  async getMyListItems(todoListId) {
    const res = await api.get(`api/todolists/${todoListId}/todolistitems`)
    logger.log("Todo list items", res.data)
    AppState.todoListItems = res.data.map(pojo => new TodoListItem(pojo))
  }

  async createListItem(todoListData) {
    const res = await api.post(`api/todolistitems`, todoListData)
    logger.log("Created Item", res.data)
    AppState.todoListItems.push(new TodoListItem(res.data))
    logger.log("Todo list items", AppState.todoListItems)
  }

}

export const todoListItemsService = new TodoListItemsService()