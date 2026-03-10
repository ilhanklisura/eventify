import { Resource, type ApiResult } from '../resource'

export interface Ticket {
  id: number
  eventId: number
  userId: number
  price: number
  status: string
  createdAt: string
  event?: { id: number; title: string }
  user?: { id: number; name: string; email: string }
}

export interface CreateTicketRequest {
  eventId: number
  userId: number
  price: number
  status?: string
}

export interface UpdateTicketRequest {
  id: number
  eventId: number
  userId: number
  price: number
  status: string
}

export class TicketApi extends Resource {
  constructor(headers: Headers) {
    super('Ticket', headers)
  }

  async getAll(): Promise<ApiResult<Ticket[]>> {
    return this.callGet<Ticket[]>('')
  }

  async getById(id: number): Promise<ApiResult<Ticket>> {
    return this.callGet<Ticket>(String(id))
  }

  async create(data: CreateTicketRequest): Promise<ApiResult<Ticket>> {
    return this.callPost('', data)
  }

  async update(id: number, data: UpdateTicketRequest): Promise<ApiResult<Ticket>> {
    return this.callPut(String(id), data)
  }

  async delete(id: number): Promise<ApiResult<void>> {
    return this.callDelete<void>(String(id))
  }
}
