import { logger } from '../utils/Logger.js'
import { api } from "./AxiosService.js"
import { Weather } from "../models/Weather.js";
import { AppState } from "../AppState.js";

class WeatherService {

  async getWeather() {
    const res = await api.get('/Weather/Index');
    logger.log(res.data)
    AppState.currentWeather = new Weather(res.data)
    logger.log(AppState.currentWeather)
  }

}

export const weatherService = new WeatherService()