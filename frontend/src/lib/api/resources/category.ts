import { Resource, type ApiResult } from '../resource'

export interface Category {
  id: number
  name: string
  createdAt: string
}

export interface CreateCategoryRequest {
  name: string
}

export interface UpdateCategoryRequest {
  id: number
  name: string
}

export class CategoryApi extends Resource {
  constructor(headers: Headers) {
    super('Category', headers)
  }

  async getAll(): Promise<ApiResult<Category[]>> {
    return this.callGet<Category[]>('')
  }

  async getById(id: number): Promise<ApiResult<Category>> {
    return this.callGet<Category>(String(id))
  }

  async create(data: CreateCategoryRequest): Promise<ApiResult<Category>> {
    return this.callPost('', data)
  }

  async update(id: number, data: UpdateCategoryRequest): Promise<ApiResult<Category>> {
    return this.callPut(String(id), { ...data, id })
  }

  async delete(id: number): Promise<ApiResult<void>> {
    return this.callDelete<void>(String(id))
  }
}
