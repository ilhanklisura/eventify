import { HubConnectionBuilder, LogLevel, type HubConnection } from '@microsoft/signalr'

function hubBaseUrl(): string {
  const apiUrl = import.meta.env.VITE_API_URL as string
  // examples:
  // - http://localhost:5030/api  -> http://localhost:5030
  // - https://localhost:44335/api -> https://localhost:44335
  // - /api -> ''
  if (apiUrl.endsWith('/api')) return apiUrl.slice(0, -4)
  if (apiUrl === '/api') return ''
  return apiUrl.replace(/\/api\/?$/, '')
}

export function createConnection(path: string): HubConnection {
  const url = `${hubBaseUrl()}${path}`

  return new HubConnectionBuilder()
    .withUrl(url)
    .withAutomaticReconnect()
    .configureLogging(LogLevel.Information)
    .build()
}

