<template>
  <div class="container-fluid vh-100 d-flex flex-column justify-content-between py-3 coverImageStyle">



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
      <div v-if="currentWeather" class="col-1">
        <div class="text-bg fredoka-font mx-1 rounded text-center">
          <p class="mt-2 mx-1 mb-0 py-2">{{ currentWeather.temperature }}°C</p>
        </div>
      </div>
    </section>
    <section class="row justify-content-center text-center">
      <div class="col-2 text-bg rounded-pill my-5">
        <p class="mb-0 fs-1 fredoka-font">{{ nowFormatted }}</p>
      </div>
    </section>
    <section v-if="quote" class="row justify-content-center">
      <div class="col-5 text-bg fredoka-font fs-5 d-flex flex-column align-items-center rounded-pill">
        <p class="mb-0 me-4 ms-3 mt-2">{{ quote.content }}</p>
        <p class="mb-2 me-4 align-self-end">{{ quote.author }}</p>
      </div>
    </section>
  </div>
</template>


<script>
import { AppState } from '../AppState';
import { computed, reactive, onMounted } from 'vue';
import { Image } from "../models/Image.js";

export default {

  props: { coverProp: { type: Image, required: true } },

  setup(props) {
    const now = new Date();
    const nowFormatted = now.toLocaleTimeString();

    return {
      nowFormatted,
      coverImage: computed(() => `url(${props.coverProp.imgUrl})`),
      account: computed(() => AppState.account),
      todoList: computed(() => AppState.todoList),
      todoListItems: computed(() => AppState.todoListItems),
      quote: computed(() => AppState.quote),
      image: computed(() => AppState.image),
      currentWeather: computed(() => AppState.currentWeather),
    }
  }
};
</script>


<style lang="scss" scoped>
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