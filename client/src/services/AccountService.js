import { AppState } from '../AppState';
import { Account } from '../models/Account.js'
import { logger } from '../utils/Logger'
import { api } from './AxiosService'

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
    const wantsCelsius = { "wantsCelsius": tempBool }
    logger.log(wantsCelsius)
    AppState.account.wantsCelsius = tempBool
    // const res = await api.put('account', wantsCelsius)
    const res = await api.put('account', AppState.account)
    logger.log(res.data)
    AppState.account.wantsCelsius = tempBool
    logger.log("AppState.account.wantsCelsius", AppState.account.wantsCelsius)
  }
}

export const accountService = new AccountService()
