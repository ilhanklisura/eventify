import { Resource, type ApiResult } from '../resource'

export interface Event {
  id: number
  title: string
  description: string
  date: string
  categoryId: number
  venueId: number
  organizerId: number
  createdAt: string
  category?: { id: number; name: string }
  venue?: { id: number; name: string; location: string }
  organizer?: { id: number; name: string; email: string; role: string }
}

export interface CreateEventRequest {
  title: string
  description: string
  date: string
  categoryId: number
  venueId: number
  organizerId: number
}

export interface UpdateEventRequest extends CreateEventRequest {
  id: number
}

export class EventApi extends Resource {
  constructor(headers: Headers) {
    super('Event', headers)
  }

  async getAll(): Promise<ApiResult<Event[]>> {
    return this.callGet<Event[]>('')
  }

  async getById(id: number): Promise<ApiResult<Event>> {
    return this.callGet<Event>(String(id))
  }

  async create(data: CreateEventRequest): Promise<ApiResult<Event>> {
    return this.callPost('', data)
  }

  async update(id: number, data: UpdateEventRequest): Promise<ApiResult<Event>> {
    return this.callPut(String(id), { ...data, id })
  }

  async delete(id: number): Promise<ApiResult<void>> {
    return this.callDelete<void>(String(id))
  }
}
