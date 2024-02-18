<template>
  <div class="col-10 d-flex my-2">
    <input @change="toggleComplete(`${itemProp.id}`)" v-if="itemProp" class="me-2" type="checkbox" :checked="isChecked">
    <p class="mb-0" :class="itemProp.completed ? 'strikethrough' : ''">{{
      itemProp.description }}</p>
  </div>
  <div @click="removeListItem(`${itemProp.id}`)" class="col-1">
    <p class="mb-0 fs-3" role="button"><i class="mdi mdi-delete"></i></p>
  </div>
</template>

<script>
import { AppState } from '../AppState';
import { computed, reactive, onMounted } from 'vue';
import { TodoListItem } from "../models/TodoListItem.js";
import { logger } from "../utils/Logger.js";
import Pop from "../utils/Pop.js";
import { todoListItemsService } from "../services/TodoListItemsService.js";

export default {
  props: { itemProp: { type: TodoListItem, required: true } },

  setup(props) {
    let isChecked = computed(() => props.itemProp.completed)

    return {
      isChecked,

      async toggleComplete(itemId) {
        try {
          await todoListItemsService.editListItem(itemId);
        } catch (error) {
          Pop.error(error)
        }
      },

      async removeListItem(itemId) {
        try {
          let wantsToDelete = await Pop.confirm("Are you sure you want to delete this todo list item?")
          if (!wantsToDelete) {
            return
          }
          await todoListItemsService.removeListItem(itemId)
        } catch (error) {
          Pop.error(error)
        }
      }


    }
  }
};
</script>


<style lang="scss" scoped>
.strikethrough {
  text-decoration: line-through;
}
</style>