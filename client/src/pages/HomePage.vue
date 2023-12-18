<template>
  <div class="container-fluid">
    <section v-if="account" class="row">
      {{ todoList }}
      <div v-for="listItem in todoListItems" class="col-12">
        <p>{{ listItem.description }}</p>
      </div>
    </section>
  </div>
</template>

<script>
import { onMounted, watch, computed } from "vue"
import { todoListsService } from "../services/TodoListsService.js"
import Pop from "../utils/Pop.js"
import { AppState } from "../AppState.js"

export default {
  setup() {
    const account = computed(() => AppState.account)
    const accoundId = computed(() => AppState.accountId)

    watch(account, () => {
      getMyList();
    })

    // function getOrCreateList() {
    //   try {
    //     this.getMyList()
    //   } catch (error) {
    //     this.createMyList()
    //   }
    // }

    async function getMyList() {
      try {
        await todoListsService.getMyList()
      } catch (error) {
        Pop.error(error)
      }
    }

    // async function createMyList(accountId) {
    //   try {
    //     await todoListsService.createMyList(accountId)
    //   } catch (error) {
    //     Pop.error(error)
    //   }
    // }

    return {
      account,
      todoList: computed(() => AppState.todoList),
      todoListItems: computed(() => AppState.todoListItems)

    }
  }
}
</script>

<style scoped lang="scss">
.home {
  display: grid;
  height: 80vh;
  place-content: center;
  text-align: center;
  user-select: none;

  .home-card {
    width: clamp(500px, 50vw, 100%);

    >img {
      height: 200px;
      max-width: 200px;
      width: 100%;
      object-fit: contain;
      object-position: center;
    }
  }
}
</style>
