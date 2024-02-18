<template>
  <div v-if="wantsMainPage"
    class="container-fluid vh-100 d-flex flex-column justify-content-between py-3 coverImageStyle">

    <!-- <section v-if="account" class="row">
      {{ todoList }}
      <div v-for="listItem in todoListItems" class="col-12">
        <p>{{ listItem.description }}</p>
      </div>
    </section> -->
    <section class="row justify-content-between">
      <div class="col-1">
        <div class="text-bg fredoka-font mx-1 rounded text-center">
          <p class="mt-2 mx-1 mb-0 pt-2">Image by</p>
          <p class="mb-1 mx-1 mb-2 pb-2">{{ coverProp.author }}</p>
        </div>
      </div>
      <div class="col-1">
        <div class="text-bg fredoka-font mx-1 rounded text-center">
          <p v-if="currentWeather" class="mt-2 mx-1 mb-1 py-2">{{ currentWeather.temperature }}°C</p>
          <p v-else class="mt-2 mx-1 mb-1 py-2">Weather not available</p>
        </div>
        <div v-if="account.id" class="text-bg fredoka-font m-1 rounded text-center">
          <p class="mb-0 fs-1" title="To-do List" role="button" data-bs-toggle="modal" data-bs-target="#todoListModal"><i
              class="mdi mdi-notebook"></i></p>
        </div>
      </div>
    </section>
    <section class="row justify-content-center text-center">
      <div class="col-2 text-bg rounded-pill my-5">
        <p class="mb-0 fs-1 fredoka-font">{{ nowFormatted }}</p>
      </div>
    </section>
    <section v-if="quote" class="row justify-content-between align-items-center">
      <div class="col-1"></div>
      <div class="col-5 text-bg fredoka-font fs-5 d-flex flex-column align-items-center rounded-pill">
        <p class="mb-0 me-4 ms-3 mt-2">{{ quote.content }}</p>
        <p class="mb-2 me-4 align-self-end">{{ quote.author }}</p>
      </div>
      <div @click="toggleWantsMainPage" class="col-1 text-bg fredoka-font rounded mx-1 text-center">
        <Login />
      </div>
    </section>
  </div>
  <div v-else class="container-fluid vh-100 d-flex flex-column justify-content-between coverImageStyle">
    <section class="row">
      <div class="col-2">
        <p @click="toggleWantsMainPage" class="text-bg fredoka-font rounded py-2 px-2 mt-2" role="button"><i
            class="mdi mdi-arrow-left"></i>Go Back</p>
      </div>
    </section>
    <section class="row justify-content-center text-center position-relative">
      <img v-if="account.picture || user.picture" :src="account.picture || user.picture" alt="Profile picture"
        class="rounded-circle user-picture">
      <div class="col-6 text-bg fredoka-font rounded">
        <p class="fs-2">Bonjour, {{ account.name || user.name }}</p>
      </div>
      <form>
        <!-- TODO: Insert user preferences here -->
      </form>
    </section>
    <section class="row"> </section>
  </div>

  <ModalComponent :modalId="'todoListModal'">
    <template #modalTitle>
      <p class="fredoka-font fs-3">To-Do List</p>
    </template>
    <template #modalBody>
      <TodoListComponent />
    </template>
  </ModalComponent>
</template>


<script>
import { AppState } from '../AppState';
import { computed, reactive, onMounted, ref } from 'vue';
import { Image } from "../models/Image.js";
import TodoListComponent from "./TodoListComponent.vue";
import ModalComponent from "./ModalComponent.vue";
import Login from "./Login.vue";

export default {

  props: { coverProp: { type: Image, required: true } },

  setup(props) {
    const now = new Date();
    const nowFormatted = now.toLocaleTimeString();
    let wantsMainPage = ref(true);

    return {
      nowFormatted,
      coverImage: computed(() => `url(${props.coverProp.imgUrl})`),
      account: computed(() => AppState.account),
      user: computed(() => AppState.user),
      todoList: computed(() => AppState.todoList),
      todoListItems: computed(() => AppState.todoListItems),
      quote: computed(() => AppState.quote),
      image: computed(() => AppState.image),
      currentWeather: computed(() => AppState.currentWeather),
      wantsMainPage,

      toggleWantsMainPage() {
        wantsMainPage.value = !wantsMainPage.value;
      }
    }
  },
  components: { ModalComponent, TodoListComponent, Login }
};
</script>


<style lang="scss" scoped>
.user-picture {
  position: absolute;
  width: 10dvw;
  object-fit: cover;
  left: 45%;
  bottom: 53%;
}

.coverImageStyle {
  background-image: v-bind(coverImage);
  background-size: cover;
  position: fixed;
  top: 0;
  left: 0;
  min-width: 100%;
  min-height: 100vh;
}

.row-height {
  height: 30vh;
}



.text-bg {
  color: white;
  background-color: rgba(0, 0, 0, 0.363);
  backdrop-filter: blur(15px);
  overflow: hidden;
}
</style>