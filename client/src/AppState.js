import { reactive } from 'vue'

// NOTE AppState is a reactive object to contain app level data
export const AppState = reactive({
  user: {},
  /** @type {import('./models/Account.js').Account} */
  account: {},
  /** @type {import('./models/TodoList.js').TodoList | null} */
  todoList: null,
  /** @type {import('./models/TodoListItem.js').TodoListItem[]} */
  todoListItems: [],
  /** @type {import('./models/Quote.js').Quote | null} */
  quote: null,
  /** @type {import('./models/Weather.js').Weather | null} */
  currentWeather: null
})
