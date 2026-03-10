import { Resource, type ApiResult } from '../resource'

export interface Venue {
  id: number
  name: string
  location: string
  createdAt: string
}

export interface CreateVenueRequest {
  name: string
  location: string
}

export interface UpdateVenueRequest {
  id: number
  name: string
  location: string
}

export class VenueApi extends Resource {
  constructor(headers: Headers) {
    super('Venue', headers)
  }

  async getAll(): Promise<ApiResult<Venue[]>> {
    return this.callGet<Venue[]>('')
  }

  async getById(id: number): Promise<ApiResult<Venue>> {
    return this.callGet<Venue>(String(id))
  }

  async create(data: CreateVenueRequest): Promise<ApiResult<Venue>> {
    return this.callPost('', data)
  }

  async update(id: number, data: UpdateVenueRequest): Promise<ApiResult<Venue>> {
    return this.callPut(String(id), { ...data, id })
  }

  async delete(id: number): Promise<ApiResult<void>> {
    return this.callDelete<void>(String(id))
  }
}
