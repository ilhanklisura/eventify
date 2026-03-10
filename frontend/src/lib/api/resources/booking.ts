import { Resource, type ApiResult } from '../resource'

export interface Booking {
  id: number
  userId: number
  eventId: number
  createdAt: string
  user?: { id: number; name: string; email: string }
  event?: { id: number; title: string; date: string }
}

export interface CreateBookingRequest {
  userId: number
  eventId: number
}

export class BookingApi extends Resource {
  constructor(headers: Headers) {
    super('Booking', headers)
  }

  async getAll(): Promise<ApiResult<Booking[]>> {
    return this.callGet<Booking[]>('')
  }

  async getById(id: number): Promise<ApiResult<Booking>> {
    return this.callGet<Booking>(String(id))
  }

  async create(data: CreateBookingRequest): Promise<ApiResult<Booking>> {
    return this.callPost('', data)
  }

  async delete(id: number): Promise<ApiResult<void>> {
    return this.callDelete<void>(String(id))
  }
}
