import { Resource, type ApiResult } from '../resource'

export interface User {
  id: number
  name: string
  email: string
  role: string
  createdAt: string
}

export interface CreateUserRequest {
  name: string
  email: string
  password: string
  role?: string
}

export interface UpdateUserRequest {
  id: number
  name: string
  email: string
  role: string
  password?: string
}

export class UserApi extends Resource {
  constructor(headers: Headers) {
    super('User', headers)
  }

  async getAll(): Promise<ApiResult<User[]>> {
    return this.callGet<User[]>('')
  }

  async getById(id: number): Promise<ApiResult<User>> {
    return this.callGet<User>(String(id))
  }

  async create(data: CreateUserRequest): Promise<ApiResult<User>> {
    return this.callPost('', data)
  }

  async update(id: number, data: UpdateUserRequest): Promise<ApiResult<User>> {
    return this.callPut(String(id), { ...data, id })
  }

  async delete(id: number): Promise<ApiResult<void>> {
    return this.callDelete<void>(String(id))
  }
}
