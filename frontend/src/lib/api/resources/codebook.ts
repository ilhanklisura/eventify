import { Resource, type ApiResult } from '../resource'

export interface CodebookItem {
  value: number | string
  text: string
}

export interface CodebookModel {
  category?: CodebookItem[]
  eventType?: CodebookItem[]
  ticketType?: CodebookItem[]
}

export class CodebookApi extends Resource {
  constructor(headers: Headers) {
    super('Codebook', headers)
  }

  async get(): Promise<ApiResult<CodebookModel>> {
    return this.callGet<CodebookModel>('')
  }
}
