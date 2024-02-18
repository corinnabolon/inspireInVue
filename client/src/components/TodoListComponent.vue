<template>
  <div class="container-fluid fredoka-font">
    <section class="row">
      <div class="col-1">
        <p @click="toggleWantsToAdd" class="fs-1" title="Add To-do" role="button"><i class="mdi mdi-plus"></i></p>
      </div>
      <div class="col-3">
        <form @submit.prevent="addTodoListItem()" v-if="wantsToAdd" class="margin-align d-flex">
          <div>
            <input v-model="editable" type="text" class="form-control" id="todoDescription"
              aria-describedby="todoDescription" maxlength="1000" minlength="1">
          </div>
          <div>
            <button class="btn btn-success" type="submit" role="button" title="Add this item to todo list"><i
                class="mdi mdi-arrow-right"></i></button>
          </div>
        </form>
      </div>
    </section>
    <section class="row">
      <div v-for="todoListItem in todoListItems" class="d-flex">
        <TodoListItemComponent :itemProp="todoListItem" />
      </div>
    </section>

  </div>
</template>


<script>
import { AppState } from '../AppState';
import { computed, reactive, onMounted, ref, nextTick } from 'vue';
import { todoListsService } from "../services/TodoListsService.js";
import { todoListItemsService } from "../services/TodoListItemsService.js";
import Pop from "../utils/Pop.js";
import { logger } from "../utils/Logger.js";
import TodoListItemComponent from "./TodoListItemComponent.vue";

export default {
  setup() {
    let wantsToAdd = ref(false)
    let editable = ref("")

    return {
      wantsToAdd,
      editable,
      todoListItems: computed(() => AppState.todoListItems),

      toggleWantsToAdd() {
        wantsToAdd.value = !wantsToAdd.value;
      },

      async addTodoListItem() {
        try {
          debugger
          const todoListData = {}
          todoListData.description = editable.value
          await todoListItemsService.createListItem(todoListData)
          editable.value = {}
          Pop.success("Item added to to-do list")
        } catch (error) {
          Pop.error(error)
        }
      }


    }
  },
  components: { TodoListItemComponent }
};
</script>


<style lang="scss" scoped>
.margin-align {
  margin-top: .7rem;
}
</style>