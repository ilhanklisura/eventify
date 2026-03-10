import { type Result, Ok, Err } from '@/lib/result'

export interface ApiError {
  status: number
  statusText: string
  error: string
}

export type ApiResult<T> = Result<T, ApiError>

export class Resource {
  protected baseUrl: string
  protected headers: Headers

  constructor(baseUrl: string, headers: Headers) {
    this.baseUrl = baseUrl
    this.headers = headers
  }

  private get resourceUrl(): string {
    const base = import.meta.env.VITE_API_URL as string
    return base.endsWith('/') ? base + this.baseUrl : `${base}/${this.baseUrl}`
  }

  private async try<T>(fn: () => Promise<ApiResult<T>>): Promise<ApiResult<T>> {
    try {
      return await fn()
    } catch (e) {
      return Err({ status: 0, statusText: 'api exception', error: String(e) })
    }
  }

  private async fetch<T>(method: string, path: string, body: object | null = null): Promise<ApiResult<T>> {
    return this.try(async () => {
      const headers = new Headers(this.headers)
      const options: RequestInit = { method, headers }
      if (body) {
        headers.set('Content-Type', 'application/json')
        options.body = JSON.stringify(body)
      }
      const p = path.replace(/^\//, '')
      const url = p ? `${this.resourceUrl}/${p}` : this.resourceUrl
      const response = await fetch(url, options)
      const text = await response.text()
      if (!response.ok) {
        return Err({ status: response.status, statusText: response.statusText, error: text || response.statusText })
      }
      return Ok((text ? JSON.parse(text) : {}) as T)
    })
  }

  protected async callGet<T>(path = ''): Promise<ApiResult<T>> {
    return this.fetch<T>('GET', path, null)
  }

  protected async callPost<T>(path: string, body: object | null): Promise<ApiResult<T>> {
    return this.fetch<T>('POST', path, body)
  }

  protected async callPut<T>(path: string, body: object | null): Promise<ApiResult<T>> {
    return this.fetch<T>('PUT', path, body)
  }

  protected async callDelete<T>(path: string): Promise<ApiResult<T>> {
    return this.fetch<T>('DELETE', path, null)
  }
}
