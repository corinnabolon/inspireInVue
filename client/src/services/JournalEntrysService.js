import { AppState } from "../AppState.js"
import { JournalEntry } from "../models/JournalEntry.js"
import { logger } from "../utils/Logger.js"
import { api } from "./AxiosService.js"

class JournalEntrysService {

  async getMyJournalEntrys(journalId) {
    const res = await api.get(`api/journals/${journalId}/journalentrys`)
    logger.log("Journal entrys", res.data)
    AppState.journalEntrys = res.data.map(pojo => new JournalEntry(pojo))
  }

}

export const journalEntrysService = new JournalEntrysService()