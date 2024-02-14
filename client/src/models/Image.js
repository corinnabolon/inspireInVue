export class Image {
  constructor(data) {
    this.author = data.author
    this.imgUrl = data.largeImgUrl
    this.query = data.query
    this.tags = data.tags
  }
}