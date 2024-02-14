export class Weather {
  constructor(data) {
    this.humidity = data.humidity
    this.precipitationProbability = data.precipitationProbability
    this.temperature = data.temperature
  }
}