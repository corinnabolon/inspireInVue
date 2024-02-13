import { logger } from '../utils/Logger.js'
import { weatherApi } from "./AxiosService.js"

class WeatherService {

  async getWeather() {
    let location = "corvallis"
    const res = await weatherApi.get(`api/realtime?location=${location}`)
    logger.log(res)
  }

}

export const weatherService = new WeatherService()