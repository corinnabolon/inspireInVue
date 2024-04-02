<template>
  <div class="container-fluid fredoka-font">
    <section class="row">
      <div class="col-1">
        <p @click="toggleWantsToAdd" class="fs-1" :title="wantsToAdd ? `Don't write` : 'Write Journal'" role="button"><i
            class="mdi" :class="wantsToAdd ? 'mdi-minus' : 'mdi-plus'"></i></p>
      </div>
      <div class="col-8">
        <form @submit.prevent="submitJournal()" v-if="wantsToAdd" class="margin-align d-flex">
          <div>
            <textarea v-model="editable" type="text" class="form-control mb-3" id="journalDescription"
              aria-describedby="journalDescription" maxlength="1000" minlength="1"></textarea>
          </div>
          <div class="d-flex flex-column justify-content-end mb-3 ms-1">
            <button class="btn btn-success ms-2" type="submit" role="button" title="Submit today's journal entry"><i
                class="mdi mdi-arrow-right"></i></button>
          </div>
        </form>
      </div>
    </section>
    <!-- <section class="row ms-1 mb-2">
      <div v-for="todoListItem in todoListItems" class="d-flex">
        <TodoListItemComponent :itemProp="todoListItem" />
      </div>
    </section> -->

  </div>
</template>


<script>
import { AppState } from '../AppState';
import { computed, reactive, onMounted, ref } from 'vue';
import { journalsService } from "../services/JournalsService.js";
import { journalEntrysService } from "../services/JournalEntrysService.js";
import Pop from "../utils/Pop.js";

export default {
  setup() {
    let wantsToAdd = ref(false);
    let editable = ref("");

    return {
      wantsToAdd,
      editable,
      date: computed(() => AppState.date.toLocaleDateString()),

      toggleWantsToAdd() {
        wantsToAdd.value = !wantsToAdd.value;
      },

      async submitJournal() {
        try {
          const journalData = {}
          journalData.description = editable.value
          await journalEntrysService.submitJournal(journalData)
          editable.value = ""
          Pop.success("Journal entry submitted")
        } catch (error) {
          Pop.error(error)
        }
      }
    }
  }
};
</script>


<style lang="scss" scoped>
textarea {
  width: 20rem;
  height: 50dvh;
}

.margin-align {
  margin-top: .6rem;
}
</style>