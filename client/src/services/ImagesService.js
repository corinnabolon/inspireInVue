import { logger } from '../utils/Logger.js'
import { sandboxApi } from "./AxiosService.js"
import { AppState } from "../AppState.js"
import { Image } from "../models/Image.js";

class ImagesService {

  async getImage() {
    const res = await sandboxApi.get("api/images")
    logger.log("image", res.data)
    AppState.image = new Image(res.data)
  }

}

export const imagesService = new ImagesService()