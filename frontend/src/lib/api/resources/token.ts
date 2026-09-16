import { Resource, type ApiResult } from '../resource'
import type { User } from './user'
import type { PermissionList } from './permission'

export interface Token {
  value: string
  user: User
  permissions?: PermissionList
  isFirstLogin: boolean
}

export class TokenApi extends Resource {
  constructor(headers: Headers) {
    super('Token', headers)
  }

  async login(email: string, password: string): Promise<ApiResult<Token>> {
    return this.callPost('login', { email, password })
  }

  async register(name: string, email: string, password: string): Promise<ApiResult<Token>> {
    return this.callPost('register', { name, email, password })
  }
}
