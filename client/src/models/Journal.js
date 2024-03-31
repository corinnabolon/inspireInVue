export class Journal {

  constructor(data) {
    this.id = data.id
    this.createdAt = data.createdAt
    this.updatedAt = data.updatedAt
    this.accountId = data.accountId
  }
}