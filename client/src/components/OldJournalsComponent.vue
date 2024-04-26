<template>
  <div class="component">
    <!-- <div v-if="wantsListOfEntries"> -->
    <p v-for="journalEntry in journalEntrys" :key="journalEntry.id" @click="showEntry(journalEntry)">
      {{ journalEntry }}
    </p>
    <!-- </div> -->
    <!-- <div v-else> -->
    <!-- <JournalContentComponent v-if="selectedEntry" :journalEntryProp="selectedEntry" /> -->
    <!-- </div> -->
  </div>
</template>
<!-- //TODO: make the description displayed when clicked on -->
<!-- //TODO: fix the date to really be PST, it said: Thu Apr 18 2024 05:08:56 GMT-0700 (Pacific Daylight Time) for Wed Apr 17 at 22:08 PM -->

<script>
import { AppState } from '../AppState';
import { computed, reactive, onMounted } from 'vue';
import JournalContentComponent from "../components/JournalContentComponent.vue";
import { logger } from "../utils/Logger.js";

export default {

  setup() {


    return {
      journalEntrys: computed(() => AppState.journalEntrys),
      //TODO: make today's entry not show up on list
      selectedEntry: null,

      showEntry(entry) {
        logger.log(entry)
        this.selectedEntry = entry;
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