import { AppState } from "../AppState.js"
import { TodoList } from "../models/TodoList.js"
import { logger } from "../utils/Logger.js"
import { api } from "./AxiosService.js"
import { todoListItemsService } from "./TodoListItemsService.js"

class TodoListsService {

  // async getMyList() {
  //   try {
  //     const res = await api.get("account/todolists")
  //     const myListData = new TodoList(res.data)
  //     logger.log("myListData", myListData)
  //     AppState.todoList = myListData
  //     todoListItemsService.getMyListItems(myListData.id)
  //   } catch (error) {
  //     if (error.message == "You don't have a todo list") {
  //       await this.createMyList(AppState.account.id)
  //     } else {
  //       logger.log(error)
  //     }
  //   }
  // }

  async getMyList() {
    try {
      const res = await api.get("account/todolists");
      const myListData = new TodoList(res.data);
      logger.log("myListData", myListData);
      AppState.todoList = myListData;
      await todoListItemsService.getMyListItems(myListData.id);
    } catch (error) {
      if (error.response && error.response.status === 400) {
        const errorMessage = error.response.data;
        if (errorMessage === "You don't have a todo list") {
          const newList = { accountId: AppState.account.id }
          await this.createMyList(newList);
        } else {
          logger.error(`Server error: ${errorMessage}`);
        }
      } else {
        logger.error(error);
      }
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