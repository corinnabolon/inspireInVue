import { AppState } from "../AppState.js"

class ClocksService {

  calcClock() {
    AppState.now = new Date()
  }

}

export const clocksService = new ClocksService()