export class JournalEntry {
  constructor(data) {
    this.id = data.id
    this.createdAt = new Date(data.createdAt)
    this.updatedAt = new Date(data.updatedAt)
    this.completed = data.completed
    this.description = data.description
    this.creatorId = data.creatorId
    this.journalId = data.journalId
    this.createdAtInLocalTime = this.convertToLocalTime(data.createdAt)
  }

  convertToLocalTime(utcTime) {
    const detectedTimezone = Intl.DateTimeFormat().resolvedOptions().timeZone
    // let options = { timeZone: detectedTimezone };
    // const localDateTime = new Date(utcDateTime);
    let convertedTime = new Date(utcTime).toLocaleDateString('en-US', {
      timeZone: detectedTimezone,
      timeZoneName: 'short',
      minute: 'numeric',
      hour: 'numeric',
      weekday: 'short',
      day: 'numeric',
      year: 'numeric',
      month: 'short',
    })
    return convertedTime
  }
}

//TODO: fix time conversion to local time, as described here: https://stackoverflow.com/questions/14484660/time-from-utc-iana-time-zone-location