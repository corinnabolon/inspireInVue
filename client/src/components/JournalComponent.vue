<template>
  <div class="container-fluid fredoka-font">
    <section v-if="!alreadyWritten" class="row">
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
    <section v-else class="row justify-content-end">
      <div class="col-12 mt-4 pe-5 pb-2">
        <div v-if="!editingEntry">
          <p class="fredoka-font fs-5 border rounded p-3">{{ todaysEntry.description }}</p>
        </div>
        <div v-else>
          <textarea v-model="todaysEditableEntry" minlength="1" maxlength="1000"></textarea>
        </div>
      </div>
      <div class="col-3 me-5 mb-3">
        <button v-if="!editingEntry" @click="toggleEditingEntry" class="btn btn-success">Edit Entry</button>
        <button v-else @click="editJournalEntry" class="btn btn-success">Submit Changes</button>
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
import { logger } from "../utils/Logger.js"
import { journalEntrysService } from "../services/JournalEntrysService.js";
import Pop from "../utils/Pop.js";
import { Modal } from "bootstrap";

export default {
  setup() {
    let wantsToAdd = ref(false);
    let editable = ref("");
    let editingEntry = ref(false);
    let todaysEditableEntry = ref("")

    onMounted(() => {
      let journalModalElem = document.getElementById('journalModal')
      journalModalElem.addEventListener("show.bs.modal", function (event) {
        checkIfJournalAlreadyWritten()
      })
      journalModalElem.addEventListener("hide.bs.modal", function (event) {
        checkIfJournalAlreadyWritten()
      })
    })

    async function checkIfJournalAlreadyWritten() {
      logger.log("checkIfJournalWritten triggered")
      let UTCString = journalEntrysService.makeNowDate()
      if (AppState.journal) {
        try {
          await journalEntrysService.getMyJournalEntrys(AppState.journal.id)
        } catch (error) {
          Pop.error(error)
        }
      }
      AppState.journalEntrys.forEach((journalEntry) => {
        if (journalEntry.createdAt.toDateString() == UTCString) {
          AppState.alreadyWritten = true;
          logger.log("todaysEditableEntry.value", todaysEditableEntry.value)
          AppState.todaysEntry = journalEntry
          todaysEditableEntry.value = AppState.todaysEntry.description
          logger.log("todaysEditableEntry.value", todaysEditableEntry.value)
        }
      })
    }

    return {
      wantsToAdd,
      editable,
      todaysEditableEntry,
      editingEntry,
      date: computed(() => AppState.date.toLocaleDateString()),
      alreadyWritten: computed(() => AppState.alreadyWritten),
      todaysEntry: computed(() => AppState.todaysEntry),

      // displayPropsINeedToKnow() {
      //   let UTCString = journalEntrysService.makeNowDate()
      //   let JournalEntryCreatedAt = AppState.journalEntrys[20].createdAt.toDateString()
      //   logger.log("UTCString", UTCString)
      //   logger.log("JournalEntryCreatedAt", JournalEntryCreatedAt)
      // },

      toggleWantsToAdd() {
        wantsToAdd.value = !wantsToAdd.value;
      },

      toggleEditingEntry() {
        editingEntry.value = !editingEntry.value;
      },

      async submitJournal() {
        try {
          const journalEntryData = {}
          journalEntryData.description = editable.value
          await journalEntrysService.submitJournal(journalEntryData)
          editable.value = ""
          Pop.success("Journal entry submitted")
          Modal.getOrCreateInstance("#journalModal").hide()
        } catch (error) {
          Pop.error(error)
        }
      },

      async editJournalEntry() {
        try {
          let UTCString = journalEntrysService.makeNowDate()
          const journalData = AppState.journalEntrys.find((journalEntry) => journalEntry.createdAt.toDateString() == UTCString)
          journalData.description = todaysEditableEntry.value
          await journalEntrysService.editJournal(journalData)
          todaysEditableEntry.value = ""
          Pop.success("Journal edited!")
          Modal.getOrCreateInstance("#journalModal").hide()
          this.toggleEditingEntry()
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
  width: 27rem;
  height: 50dvh;
}

.margin-align {
  margin-top: .6rem;
}
</style>