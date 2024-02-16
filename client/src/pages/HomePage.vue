<template>
  <div v-if="image" class="container-fluid">
    <Cover :coverProp="image" />
    <section v-if="account" class="row">
      {{ todoList }}
      <div v-for="listItem in todoListItems" class="col-12">
        <p>{{ listItem.description }}</p>
      </div>
      <div v-if="currentWeather">
        <p>Weather:</p>
        <p>{{ currentWeather.humidity }}</p>
        <p>{{ currentWeather.precipitationProbability }}</p>
        <p>{{ currentWeather.temperature }}</p>
      </div>
    </section>
    <section v-if="quote" class="row">
      <div class="col-12">
        {{ quote }}
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

export default {
  setup() {
    const account = computed(() => AppState.account)
    const accoundId = computed(() => AppState.accountId)

    onMounted(() => {
      getQuote();
      getImage();
      getWeather();
    })

    watch(account, () => {
      getMyList();
    })

    async function getMyList() {
      try {
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
