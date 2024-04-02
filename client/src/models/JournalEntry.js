export class JournalEntry {
  constructor(data) {
    this.id = data.id
    this.createdAt = data.createdAt
    this.updatedAt = data.updatedAt
    this.completed = data.completec
    this.description = data.description
    this.entryDate = data.entryDate
    this.creatorId = data.creatorId
    this.journalId = data.journalId
  }
}