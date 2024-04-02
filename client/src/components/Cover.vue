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
            <p class="mt-2 mx-1 mb-1 py-2">Weather in {{ preferredLocation }}</p>
            <div>
              <img v-if="weatherIcon != ''" :src="weatherIcon" title="Powered by Tomorrow.io" class="weather-icon" />
            </div>
            <p v-if="!wantsCelsius" @click="toggleWantsCorF" role="button" class="mt-2 mx-1 mb-1 py-2">{{
    tempInF }}°F</p>
            <p v-else @click="toggleWantsCorF" role="button" class="mt-2 mx-1 mb-1 py-2">{{ tempInC }}°C</p>
          </div>
          <div v-else>
            <p v-if="!preferredLocation" class="mt-2 mx-1 mb-1 py-2" @click="toggleWantsMainPage" role="button">Set your
              location</p>
            <p v-else class="mt-2 mx-1 mb-1 py-2">Weather not available</p>
          </div>
        </div>
        <div v-if="account.id" class="fredoka-font m-1 rounded text-center text-bg">
          <p class="mb-0 fs-1" title="To-do List" role="button" data-bs-toggle="modal" data-bs-target="#todoListModal">
            <i class="mdi mdi-notebook"></i>
          </p>
        </div>
        <div v-if="account.id" class="fredoka-font m-1 rounded text-center text-bg mt-1">
          <p class="mb-0 fs-1" title="Journal" role="button" data-bs-toggle="modal" data-bs-target="#journalModal">
            <i class="mdi mdi-notebook-multiple"></i>
          </p>
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
      <div @click="toggleWantsMainPage" class="col-1 fredoka-font rounded mx-1 text-center">
        <Login />
      </div>
    </section>
  </div>
  <div v-else class="container-fluid vh-100 d-flex flex-column justify-content-between coverImageStyle">
    <section class="row justify-content-between">
      <div class="col-2">
        <p @click="toggleWantsMainPage" class="text-bg fredoka-font rounded py-2 px-2 mt-2" role="button"><i
            class="mdi mdi-arrow-left"></i>Go Back</p>
      </div>
      <div class="col-1 py-2 px-2 mt-2">
        <Login />
      </div>
    </section>
    <section class="row justify-content-center text-center position-relative fredoka-font">
      <img v-if="account.picture || user.picture" :src="account.picture || user.picture" alt="Profile picture"
        class="rounded-circle user-picture">
      <div class="col-6 text-bg fredoka-font rounded my-5 py-5">
        <p class="fs-2">{{ greeting }}, {{ account.name || user.name }}</p>
        <section class="row">
          <div class="col-5 text-start ms-3">
            <form id="weatherLocationForm" @submit.prevent="addLocationQuery">
              <p class="form-label fs-5 mb-0">Weather Location:</p>
              <p class="form-label fs-5">(currently
                <span v-if="account.preferredLocation" class="fst-italic">{{
    account.preferredLocation
  }}</span>
                <span v-else>not specified</span>
                )
              </p>
            </form>
          </div>
          <div class="col-6">
            <input v-model="editableLocation" type="text" id="location" class="form-conrol" placeholder="Corvallis"
              required maxLength="30" pattern="^[A-Za-z]+$"
              title="Only alphabetical characters are allowed in the location." />
            <button form="weatherLocationForm" class="ms-2 btn btn-info" type="submit">Change</button>
          </div>
          <div class="col-5 text-start ms-3">
            <form id="imageKeywordForm" @submit.prevent="addImageQuery">
              <p class="form-label fs-5 mb-0">Image keyword:</p>
              <p class="form-label fs-5">(currently <span class="fst-italic">{{
    preferredImageTypes
  }}</span>
                )</p>
            </form>
          </div>
          <div class="col-6 mt-3">
            <input v-model="editableQuery" type="text" id="query" class="form-conrol" placeholder="More image types..."
              maxLength="255" pattern="^[A-Za-z]+$" title="Only alphabetical characters are allowed in the keyword." />
            <button form="imageKeywordForm" class="ms-2 btn btn-info" type="submit">Change</button>
          </div>
          <div class="col-5 text-start ms-3 mt-3">
            <p class="mb-0 fs-5">Clock Type:</p>
          </div>
          <div class="col-6 d-flex flex-column align-items-start ms-2 mt-3">
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
          <div class="col-5 text-start ms-3 mt-3">
            <p class="mb-0 fs-5">Temperature Type:</p>
          </div>
          <div class="col-6 d-flex flex-column mt-3 ms-2 align-items-start">
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
        </section>
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

  <ModalComponent :modalId="'journalModal'">
    <template #modalTitle>
      <p class="fredoka-font fs-3 mb-0">Journal for {{date}}</p>
      <button class="btn btn-success" data-bs-toggle="modal" data-bs-target="#oldJournalsModal">Past
        Entries</button>
    </template>

    <template #modalBody>
      <JournalComponent />
    </template>
  </ModalComponent>

  <ModalComponent :modalId="'oldJournalsModal'">
    <template #modalTitle>
      <p class="fredoka-font fs-3">Journal for {{date}}</p>
    </template>

    <template #modalBody>
      <OldJournalsComponent />
    </template>
  </ModalComponent>
</template>


<script>
import { AppState } from '../AppState';
import { computed, reactive, onMounted, watch, ref } from 'vue';
import { Image } from "../models/Image.js";
import TodoListComponent from "./TodoListComponent.vue";
import OldJournalsComponent from "./OldJournalsComponent.vue";
import ModalComponent from "./ModalComponent.vue";
import JournalComponent from "./JournalComponent.vue";
import Login from "./Login.vue";
import Pop from "../utils/Pop.js";
import { accountService } from "../services/AccountService.js";
import { weatherService } from "../services/WeatherService.js";
import { logger } from "../utils/Logger.js";

export default {

  props: { coverProp: { type: Image, required: true } },

  setup(props) {
    let wantsMainPage = ref(true);
    let editableQuery = ref("")
    let editableLocation = ref("")
    const currentWeather = ref(AppState.currentWeather)
    const baseURL1 = "https://raw.githubusercontent.com/Tomorrow-IO-API/tomorrow-weather-codes/master/V2_icons/small/png/"
    const baseURL2 = "https://raw.githubusercontent.com/Tomorrow-IO-API/tomorrow-weather-codes/master/V1_icons/color/"
    const weatherIcons = {
      "1000": `${baseURL1}10000_clear_small.png`,
      "1100": `${baseURL1}11000_mostly_clear_small.png`,
      "1101": `${baseURL1}11010_partly_cloudy_small.png`,
      "1102": `${baseURL1}11020_mostly_cloudy_small.png`,
      "1001": `${baseURL2}cloudy.svg`,
      "2000": `${baseURL2}fog.svg`,
      "2100": `${baseURL1}21000_fog_light_small.png`,
      "4000": `${baseURL1}40000_drizzle_small.png`,
      "4001": `${baseURL2}rain.svg`,
      "4200": `${baseURL1}42000_rain_light_small.png`,
      "4201": `${baseURL1}42010_rain_heavy_small.png`,
      "5000": `${baseURL2}snow.svg`,
      "5001": `${baseURL1}50010_flurries_small.png`,
      "5100": `${baseURL1}51000_snow_light_small.png`,
      "5101": `${baseURL1}51010_snow_heavy_small.png`,
      "6000": `${baseURL1}60000_freezing_rain_drizzle_small.png`,
      "6001": `${baseURL2}freezing_rain.svg`,
      "6200": `${baseURL1}62000_freezing_rain_light_small.png`,
      "6201": `${baseURL1}62010_freezing_rain_heavy_small.png`,
      "7000": `${baseURL2}ice_pellets.svg`,
      "7101": `${baseURL1}71010_ice_pellets_heavy_small.png`,
      "7102": `${baseURL1}71020_ice_pellets_light_small.png`,
      "8000": `${baseURL1}80000_tstorm_small.png`
    };

    //TODO: after login get weather and new image (if needed) automatically

    return {
      weatherIcon: computed(() => {
        if (AppState.currentWeather) {
          logger.log("This is the findWeatherIcon function", AppState.currentWeather.weatherCode)
          for (let key in weatherIcons) {
            if (key == AppState.currentWeather.weatherCode) {
              return weatherIcons[key]
            }
          }
        } else {
          return
        }
      }),
      editableQuery,
      editableLocation,
      account: computed(() => AppState.account),
      wantsCelsius: computed(() => AppState.account.wantsCelsius),
      coverImage: computed(() => `url(${props.coverProp.imgUrl})`),
      account: computed(() => AppState.account),
      user: computed(() => AppState.user),
      todoList: computed(() => AppState.todoList),
      todoListItems: computed(() => AppState.todoListItems),
      journal: computed(() => AppState.journal),
      journalEntrys: computed(() => AppState.journalEntrys),
      quote: computed(() => AppState.quote),
      image: computed(() => AppState.image),
      currentWeather: computed(() => AppState.currentWeather),
      preferredLocation: computed(() => AppState.account.preferredLocation),
      preferredImageTypes: computed(() => {
        if (AppState.account.preferredImageTypes == "" || "''") {
          return "none"
        } else {
          return AppState.account.preferredImageTypes
        }
      }),
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
      greeting: computed(() => {
        let time = AppState.now.toLocaleTimeString('en-US', { hour12: false, hour: '2-digit' });
        if (time > 3 && time < 12) {
          return "Good morning"
        } else if (time > 12 && time < 17) {
          return "Good afternoon"
        } else {
          return "Good evening"
        }
      }),
      date: computed(() => AppState.date.toLocaleDateString()),
      todaysJournalEntry: computed(() => AppState.journalEntrys.find((journalEntry) => journalEntry.createdAt == AppState.date)),


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
          let clockBool = AppState.account.wantsTwentyFourClock
          clockBool = !clockBool
          await accountService.toggleWants12or24(clockBool)
        } catch (error) {
          Pop.error(error)
        }
      },

      async addImageQuery() {
        try {
          let newQuery = editableQuery.value
          newQuery = encodeURIComponent(newQuery)
          await accountService.addImageQuery(newQuery)
          editableQuery.value = ""
          //TODO Figure out how to fix the above so it erases what's in there but also doesn't add quotes to the property
        } catch (error) {
          Pop.error(error)
        }
      },

      async addLocationQuery() {
        try {
          let newLocation = editableLocation.value
          await accountService.changeLocation(newLocation)
          editableLocation.value = ""
          await weatherService.getWeather(newLocation)
        } catch (error) {
          Pop.error(error)
        }
      }

    }
  },
  components: { ModalComponent, TodoListComponent, Login, JournalComponent, OldJournalsComponent }
};
</script>


<style lang="scss" scoped>
.user-picture {
  position: absolute;
  width: 9dvw;
  object-fit: cover;
  left: 45%;
  bottom: 81%;
  z-index: 1;
}

.weather-icon {
  max-height: 5dvh;
  width: auto;
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

.visible-on-hover {
  opacity: 0.0;
  transition: all 300ms ease-in-out;
}

.show-author:hover+.visible-on-hover,
.visible-on-hover:hover {
  opacity: 1.0;
}
</style>