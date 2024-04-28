<template>
  <div class="col-12 mt-4 pe-5 border rounded fredoka-font p-2 mb-3">
    <p>{{ journalEntryProp.createdAtInLocalTime }}:</p>
    <p class="fs-5 rounded">
      {{ journalEntryProp.description }}
    </p>
  </div>
</template>
<!-- //TODO: make this the one that gets displayed when either past entries are clicked on or when today's entry has been written -->


<script>
import { AppState } from '../AppState';
import { computed, reactive, onMounted } from 'vue';
import { JournalEntry } from "../models/JournalEntry.js";


export default {
  props: { journalEntryProp: { type: JournalEntry, required: true } },

  setup() {

    return {}
  }
};
</script>


<style lang="scss" scoped></style>