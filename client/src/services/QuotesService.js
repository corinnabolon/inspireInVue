import { AppState } from "../AppState.js"
import { Quote } from "../models/Quote.js"
import { logger } from "../utils/Logger.js"
import { sandboxApi } from "./AxiosService.js"

class QuotesService {

  async getQuote() {
    const res = await sandboxApi.get("api/quotes")
    AppState.quote = new Quote(res.data)
  }

}

export const quotesService = new QuotesService()