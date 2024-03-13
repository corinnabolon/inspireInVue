<template>
  <header>
  </header>
  <main>
    <router-view />
  </main>
</template>

<script>
import { computed } from 'vue'
import { AppState } from './AppState'
import Navbar from './components/Navbar.vue'

export default {
  setup() {
    return {
      appState: computed(() => AppState)
    }
  },
  components: { Navbar }
}
</script>
<style lang="scss">
@import "./assets/scss/main.scss";

:root {
  --main-height: calc(100vh - 32px - 64px);
}

.fredoka-font {
  font-family: "Fredoka", sans-serif;
  font-optical-sizing: auto;
  font-weight: 400;
  font-style: normal;
  font-variation-settings:
    "wdth" 100;
}

.text-bg {
  color: white;
  background-color: rgba(0, 0, 0, 0.363);
  backdrop-filter: blur(15px);
  overflow: hidden;
}

footer {
  display: grid;
  place-content: center;
  height: 32px;
}
</style>
