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

  async editListItem(itemId) {
    let foundItem = AppState.todoListItems.find((item) => item.id == itemId)
    foundItem.completed = !foundItem.completed
    const res = await api.put(`api/todolistitems/${itemId}`, foundItem)
    logger.log("FoundItem after its change", foundItem)
    let foundIndex = AppState.todoListItems.findIndex((item) => item.id == itemId)
    AppState.todoListItems.splice(foundIndex, 1, new TodoListItem(res.data))
  }

  async removeListItem(itemId) {
    const res = await api.delete(`api/todolistitems/${itemId}`)
    let foundIndex = AppState.todoListItems.findIndex((item) => item.id == itemId)
    AppState.todoListItems.splice(foundIndex, 1)
  }

}

export const todoListItemsService = new TodoListItemsService()