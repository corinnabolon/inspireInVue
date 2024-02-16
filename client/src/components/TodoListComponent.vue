<template>
  <div class="container-fluid fredoka-font">
    <section class="row">
      <div class="col-1">
        <p @click="toggleWantsToAdd" class="fs-1" title="Add To-do" role="button"><i class="mdi mdi-plus"></i></p>
      </div>
      <div class="col-3">
        <form v-if="wantsToAdd" class="margin-align">
          <input>
        </form>
      </div>
    </section>
    TodoList

  </div>
</template>


<script>
import { AppState } from '../AppState';
import { computed, reactive, onMounted, ref } from 'vue';
export default {
  setup() {
    let wantsToAdd = ref(false)

    return {
      wantsToAdd,

      toggleWantsToAdd() {
        wantsToAdd.value = !wantsToAdd.value;
      }
    }
  }
};
</script>


<style lang="scss" scoped>
.margin-align {
  margin-top: .7rem;
}
</style>