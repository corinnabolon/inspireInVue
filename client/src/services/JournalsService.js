import { AppState } from "../AppState.js"
import { Journal } from "../models/Journal.js"
import { logger } from "../utils/Logger.js"
import { api } from "./AxiosService.js"
import { journalEntrysService } from "./JournalEntrysService.js"

class JournalsService {

  async getMyJournal() {
    try {
      const res = await api.get("account/journals");
      const myJournalData = new Journal(res.data);
      logger.log("myJournalData", myJournalData);
      AppState.journal = myJournalData;
      await journalEntrysService.getMyJournalEntrys(myJournalData.id);
    } catch (error) {
      if (error.response && error.response.status === 400) {
        const errorMessage = error.response.data;
        if (errorMessage === "You don't have a journal") {
          const newJournal = { accountId: AppState.account.id }
          await this.createMyJournal(newJournal);
        } else {
          logger.error(`Server error: ${errorMessage}`);
        }
      } else {
        logger.error(error);
      }
    }
  }

  async createMyJournal(accountId) {
    const res = await api.post("api/journals", accountId)
    const myJournalData = new Journal(res.data)
    logger.log("myJournalData", myJournalData)
    AppState.journal = myJournalData
    journalEntrysService.getMyJournalEntrys(myJournalData.id)
  }

}

export const journalsService = new JournalsService()