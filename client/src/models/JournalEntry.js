import moment from 'moment-timezone';
moment().tz("America/Los_Angeles").format();


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
    const localDateTime = new Date(utcTime);
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
    // var m = moment(utcTime).tz(detectedTimezone);
    // var s = m.format(); 
    // var a = moment.tz(utcTime, detectedTimezone);
    // var a = moment.utc(utcTime).tz(detectedTimezone);
    // var s = m.format();
    let m = moment.utc(utcTime).tz(detectedTimezone).format();
    m.substring(0, 10)
    let dateNum = m.substring(8, 10)
    let month = m.substring(5, 7)
    let year = m.substring(0, 4)
    let rearrangedDateString = month + "-" + dateNum + "-" + year
    return rearrangedDateString
  }
}

//TODO: fix time conversion to local time, as described here: https://stackoverflow.com/questions/14484660/time-from-utc-iana-time-zone-location