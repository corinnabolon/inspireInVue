<template>
  <div v-if="wantsMainPage"
    class="container-fluid vh-100 d-flex flex-column justify-content-between py-3 coverImageStyle">
    <section class="row justify-content-between">
      <div class="col-1">
        <div class="text-bg fredoka-font mx-1 rounded text-center">
          <p class="mt-2 mx-1 mb-0 pt-2">Image by</p>
          <p class="mb-1 mx-1 mb-2 pb-2">{{ coverProp.author }}</p>
        </div>
      </div>
      <div class="col-1">
        <div class="text-bg fredoka-font mx-1 rounded text-center">
          <div v-if="currentWeather">
            <!-- TODO: Add account check here -->
            <p v-if="!wantsCelsius" @click="toggleWantsCorF" role="button" class="mt-2 mx-1 mb-1 py-2">{{
              tempInF }}°F</p>
            <p v-else @click="toggleWantsCorF" role="button" class="mt-2 mx-1 mb-1 py-2">{{ tempInC }}°C</p>
          </div>
          <div v-else>
            <p class="mt-2 mx-1 mb-1 py-2">Weather not available</p>
          </div>
        </div>
        <div v-if="account.id" class="text-bg fredoka-font m-1 rounded text-center">
          <p class="mb-0 fs-1" title="To-do List" role="button" data-bs-toggle="modal" data-bs-target="#todoListModal"><i
              class="mdi mdi-notebook"></i></p>
        </div>
      </div>
    </section>
    <section class="row justify-content-center text-center">
      <div class="col-2 text-bg rounded-pill my-5">
        <p @click="toggleWants12or24" role="button" class="mb-0 fs-1 fredoka-font">{{ nowFormatted }}</p>
      </div>
    </section>
    <section v-if="quote" class="row justify-content-between align-items-center">
      <div class="col-1"></div>
      <div class="col-5 text-bg fredoka-font fs-5 d-flex flex-column align-items-center rounded-pill">
        <p class="mb-0 me-4 ms-3 mt-2 show-author">{{ quote.content }}</p>
        <p class="mb-2 me-4 align-self-end visible-on-hover">{{ quote.author }}</p>
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
        <div class="d-flex">
          <form @submit.prevent="addImageQuery">
            <label for="query" class="form-label">Image keyword: (currently <span class="fst-italic">{{
              account.preferredImageTypes
            }}</span>
              )</label>
            <input v-model="editableQuery" type="text" if="query" class="form-conrol" placeholder="More image types..."
              required maxLength="255" />
            <button class="ms-2 btn btn-info" type="submit">Change</button>
          </form>
        </div>
        <div class="d-flex flex-column align-items-center">
          <div v-if="!wantsTwentyFourClock" class="d-flex">
            <button class="btn btn-selected me-3" title="Switch preference to 12-hour clock">
              12
            </button>
            <button @click="toggleWants12or24" class="btn btn-unselected">
              24
            </button>
          </div>
          <div v-else class="d-flex">
            <button @click="toggleWants12or24" class="btn btn-unselected me-3">
              12
            </button>
            <button class="btn btn-selected" title="Switch preference to 24-hour clock">
              24
            </button>
          </div>
        </div>
        <div class="d-flex flex-column align-items-center mt-2">
          <div v-if="!wantsCelsius" class="d-flex">
            <button @click="toggleWantsCorF" class="btn btn-unselected me-3" title="Switch preference to °C">
              °C
            </button>
            <button class=" btn btn-selected">
              °F
            </button>
          </div>
          <div v-else class="d-flex">
            <button class="btn btn-selected me-3">
              °C
            </button>
            <button @click="toggleWantsCorF" class="btn btn-unselected" title="Switch preference to °F">
              °F
            </button>
          </div>
        </div>
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
import Pop from "../utils/Pop.js";
import { accountService } from "../services/AccountService.js";
import { logger } from "../utils/Logger.js";

export default {

  props: { coverProp: { type: Image, required: true } },

  setup(props) {
    // const now = new Date();
    // const nowFormatted = now.toLocaleTimeString();
    // let wantsCelsius = ref(AppState.wantsCelsius);
    let wantsMainPage = ref(true);
    let editableQuery = ref("")

    return {
      editableQuery,
      account: computed(() => AppState.account),
      wantsCelsius: computed(() => AppState.account.wantsCelsius),
      coverImage: computed(() => `url(${props.coverProp.imgUrl})`),
      account: computed(() => AppState.account),
      user: computed(() => AppState.user),
      todoList: computed(() => AppState.todoList),
      todoListItems: computed(() => AppState.todoListItems),
      quote: computed(() => AppState.quote),
      image: computed(() => AppState.image),
      currentWeather: computed(() => AppState.currentWeather),
      tempInC: computed(() => Math.round(AppState.currentWeather.temperature)),
      tempInF: computed(() => Math.round(AppState.currentWeather.temperature * 1.8 + 32)),
      wantsMainPage,
      wantsTwentyFourClock: computed(() => AppState.account.wantsTwentyFourClock),
      nowFormatted: computed(() => {
        if (AppState.account.wantsTwentyFourClock) {
          return AppState.now.toLocaleTimeString('en-US', { hour12: false, hour: '2-digit', minute: '2-digit' });
        } else {
          let twelveHour = AppState.now.toLocaleTimeString([], { hour: '2-digit', minute: '2-digit' });
          return twelveHour.replace(/AM|PM/, '')
        }
      }),

      toggleWantsMainPage() {
        wantsMainPage.value = !wantsMainPage.value;
      },

      async toggleWantsCorF() {
        try {
          let tempBool = AppState.account.wantsCelsius
          tempBool = !tempBool
          await accountService.toggleWantsCorF(tempBool)
        } catch (error) {
          Pop.error(error)
        }
      },

      async toggleWants12or24() {
        try {
          let tempBool = AppState.account.wantsTwentyFourClock
          tempBool = !tempBool
          await accountService.toggleWants12or24(tempBool)
        } catch (error) {
          Pop.error(error)
        }
      },

      async addImageQuery() {
        try {
          let newQuery = editableQuery.value
          await accountService.addImageQuery(newQuery)
        } catch (error) {
          Pop.error(error)
        }
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

.btn-selected {
  background-color: white;
  color: black;
  cursor: default;
}

.btn-selected:focus {
  background-color: white;
  color: black;
  cursor: default;
  border: none;
}


.btn-unselected {
  color: lightslategray;
  background-color: rgba(0, 0, 0, 0.363);
  backdrop-filter: blur(15px);
  overflow: hidden;
}

.text-bg {
  color: white;
  background-color: rgba(0, 0, 0, 0.363);
  backdrop-filter: blur(15px);
  overflow: hidden;
}

.visible-on-hover {
  opacity: 0.0;
  transition: all 300ms ease-in-out;
}

.show-author:hover+.visible-on-hover,
.visible-on-hover:hover {
  opacity: 1.0;
}
</style>