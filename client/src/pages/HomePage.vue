<template>
  <div v-if="image" class="container-fluid">
    <Cover :coverProp="image" />
  </div>
  <div v-else class="continer-fluid">
    <section class="row flex-column justify-content-center align-items-center">
      <div class="col-12">
        <p class="fs-1">Loading...<i class="mdi mdi-loading mdi-spin"></i></p>
      </div>
    </section>
  </div>
</template>

<script>
import { onMounted, watch, computed } from "vue"
import { todoListsService } from "../services/TodoListsService.js"
import Pop from "../utils/Pop.js"
import { AppState } from "../AppState.js"
import { quotesService } from "../services/QuotesService.js"
import { weatherService } from "../services/WeatherService.js"
import { imagesService } from "../services/ImagesService.js"
import Cover from "../components/Cover.vue";
import { logger } from "../utils/Logger.js"
import { clocksService } from "../services/ClocksService.js"

export default {
  setup() {
    const account = computed(() => AppState.account)
    const accoundId = computed(() => AppState.accountId)

    onMounted(() => {
      getQuote();
      getImage();
      getWeather();
      calcClock();
    })

    watch(account, () => {
      getMyList();
    })

    function calcClock() {
      clocksService.calcClock();
    }

    async function getMyList() {
      try {
        logger.log("GetMyList triggered")
        await todoListsService.getMyList()
      } catch (error) {
        Pop.error(error)
      }
    }

    async function getQuote() {
      try {
        await quotesService.getQuote()
      } catch (error) {
        Pop.error(error)
      }
    }

    async function getWeather() {
      try {
        await weatherService.getWeather()
      } catch (error) {
        Pop.error(error)
      }
    }

    async function getImage() {
      try {
        await imagesService.getImage()
      } catch (error) {
        Pop.error(error)
      }
    }

    setInterval(calcClock, 5000)


    return {
      account,
      todoList: computed(() => AppState.todoList),
      todoListItems: computed(() => AppState.todoListItems),
      quote: computed(() => AppState.quote),
      image: computed(() => AppState.image),
      currentWeather: computed(() => AppState.currentWeather),

    }
  },
  components: { Cover },
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
