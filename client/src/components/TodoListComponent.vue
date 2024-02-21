<template>
  <div class="container-fluid fredoka-font">
    <section class="row">
      <div class="col-1">
        <p @click="toggleWantsToAdd" class="fs-1" :title="wantsToAdd ? `Don't add` : 'Add To-do'" role="button"><i
            class="mdi" :class="wantsToAdd ? 'mdi-minus' : 'mdi-plus'"></i></p>
      </div>
      <div class="col-8">
        <form @submit.prevent="addTodoListItem()" v-if="wantsToAdd" class="margin-align d-flex">
          <div>
            <input v-model="editable" type="text" class="form-control" id="todoDescription"
              aria-describedby="todoDescription" maxlength="1000" minlength="1">
          </div>
          <div>
            <button class="btn btn-success ms-2" type="submit" role="button" title="Add this item to todo list"><i
                class="mdi mdi-arrow-right"></i></button>
          </div>
        </form>
      </div>
      <div class="col-3">
        <p v-if="uncompletedTasks.length == 1" class="mb-0 mt-3 fs-5">{{ uncompletedTasks.length }} task</p>
        <p v-else class="mb-0 mt-3 fs-5">{{ uncompletedTasks.length }} tasks</p>
      </div>
    </section>
    <section class="row ms-1 mb-2">
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
      uncompletedTasks: computed(() => AppState.todoListItems.filter(item => !item.completed)),

      toggleWantsToAdd() {
        wantsToAdd.value = !wantsToAdd.value;
      },

      async addTodoListItem() {
        try {
          const todoListData = {}
          todoListData.description = editable.value
          await todoListItemsService.createListItem(todoListData)
          editable.value = ""
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
input {
  width: 15rem;
}

.margin-align {
  margin-top: .6rem;
}
</style>