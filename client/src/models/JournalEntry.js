export class JournalEntry {
  constructor(data) {
    this.id = data.id
    this.createdAt = new Date(data.createdAt)
    this.updatedAt = new Date(data.updatedAt)
    this.completed = data.completec
    this.description = data.description
    this.creatorId = data.creatorId
    this.journalId = data.journalId
  }
}