import { AppState } from "../AppState.js"
import { JournalEntry } from "../models/JournalEntry.js"
import { logger } from "../utils/Logger.js"
import { api } from "./AxiosService.js"

class JournalEntrysService {

  async getMyJournalEntrys(journalId) {
    const res = await api.get(`api/journals/${journalId}/journalentrys`)
    logger.log("Journal entrys", res.data)
    AppState.journalEntrys = res.data.map((pojo) => new JournalEntry(pojo))
  }

  async submitJournal(journalData) {
    let cancel = false
    let newDate = new Date().toUTCString()
    let newDateString = newDate.substring(0, 16)
    let dayOfWeek = newDateString.substring(0, 3)
    let dateNum = newDateString.substring(5, 7)
    let month = newDateString.substring(8, 11)
    let year = newDateString.substring(12, 16)
    let rearrangedDateString = dayOfWeek + " " + month + " " + dateNum + " " + year
    AppState.journalEntrys.forEach((journalEntry) => {
      if (journalEntry.createdAt == rearrangedDateString) {
        logger.log("Same date")
        cancel = true
        return
      }
    })
    if (!cancel) {
      const res = await api.post(`api/journalentrys`, journalData)
      logger.log("Created journal entry", res.data)
      AppState.journalEntrys.push(new JournalEntry(res.data))
      logger.log("Journal entrys", AppState.journalEntrys)
    }
  }

}

export const journalEntrysService = new JournalEntrysService()