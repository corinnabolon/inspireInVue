<template>
  <div class="container-fluid fredoka-font">
    <section class="row">
      <div v-if="wantsListOfEntries" class="my-2">
        <p class="fs-5 mb-0" v-for="journalEntry in journalEntrys" :key="journalEntry.id"
          @click="showJournalContent(journalEntry)" role="button">
          <i class="mdi mdi-arrow-right"></i>
          {{ journalEntry.createdAtInLocalTime }}
        </p>
      </div>
      <div v-else>
        <div v-if="selectedEntry">
          <JournalContentComponent :journalEntryProp="selectedEntry" />
          <div class="text-end">
            <button class="btn btn-success mb-3" @click="toggleWantsList()">List of Entries</button>
          </div>
        </div>
      </div>
    </section>
  </div>
</template>
<!-- //TODO: make the description displayed when clicked on -->
<!-- //TODO: fix the date to really be PST, it said: Thu Apr 18 2024 05:08:56 GMT-0700 (Pacific Daylight Time) for Wed Apr 17 at 22:08 PM -->

<script>
import { AppState } from '../AppState';
import { computed, reactive, onMounted, ref } from 'vue';
import JournalContentComponent from "../components/JournalContentComponent.vue";
import { logger } from "../utils/Logger.js";

export default {

  setup() {
    let wantsListOfEntries = ref(true)


    return {
      journalEntrys: computed(() => AppState.journalEntrys.reverse()),
      //TODO: make today's entry not show up on list
      selectedEntry: null,
      wantsListOfEntries,

      toggleWantsList() {
        this.wantsListOfEntries = !this.wantsListOfEntries
      },

      showJournalContent(entry) {
        logger.log(entry)
        this.selectedEntry = entry;
        this.toggleWantsList()
      },

      // convertToLocalTime(utcDateTime) {
      //   const detectedTimezone = Intl.DateTimeFormat().resolvedOptions().timeZone
      //   let options = { timeZone: detectedTimezone };
      //   const localDateTime = new Date(utcDateTime);
      //   let convertedTime = localDateTime.toLocaleString("en-US", options)
      //   return convertedTime
      // },
      // closeModal() {
      //   this.selectedObject = null;
      // },


    }
  },
  components: { JournalContentComponent }
};
</script>


<style lang="scss" scoped></style>