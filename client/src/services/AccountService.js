import { AppState } from '../AppState';
import { Account } from '../models/Account.js'
import { logger } from '../utils/Logger'
import { api } from './AxiosService'
import { imagesService } from "./ImagesService.js";

class AccountService {
  async getAccount() {
    try {
      const res = await api.get('/account')
      AppState.account = new Account(res.data)
    } catch (err) {
      logger.error('HAVE YOU STARTED YOUR SERVER YET???', err)
    }
  }

  async toggleWantsCorF(tempBool) {
    // const wantsCelsius = { "wantsCelsius": tempBool }
    AppState.account.wantsCelsius = tempBool
    // const res = await api.put('account', wantsCelsius)
    const res = await api.put('account', AppState.account)
    AppState.account.wantsCelsius = tempBool
  }

  async toggleWants12or24(clockBool) {
    // const wantsTwentyFourClock = { "wantsTwentyFourClock": clockBool }
    AppState.account.wantsTwentyFourClock = clockBool
    // const res = await api.put('account', wantsCelsius)
    const res = await api.put('account', AppState.account)
    AppState.account.wantsTwentyFourClock = clockBool
  }

  async changeLocation(newLocation) {
    // const preferredLocation = { "preferredLocation": newLocation }
    AppState.account.preferredLocation = newLocation
    const res = await api.put('account', AppState.account)
    AppState.account.preferredLocation = newLocation
    logger.log(AppState.account.preferredLocation)
  }

  async addImageQuery(newQuery) {
    logger.log(AppState.account.preferredImageTypes)
    logger.log(newQuery)
    AppState.account.preferredImageTypes = newQuery
    logger.log(AppState.account.preferredImageTypes)
    const res = await api.put('account', AppState.account)
    logger.log(res.data)
    await imagesService.getImage(AppState.account.preferredImageTypes)
    // if (AppState.account.preferredImageTypes[0] != '"') {
    //   AppState.account.preferredImageTypes = '"' + AppState.account.preferredImageTypes + '"'
    // }
    logger.log("AppState.account.preferredImageTypes", AppState.account.preferredImageTypes)
  }
}

export const accountService = new AccountService()
