<template>
  <p class="fredoka-font fs-5 border rounded p-3">
    {{ journalEntryProp.description }}
  </p>
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