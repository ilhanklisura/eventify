import type { HubConnection } from '@microsoft/signalr'

export class NotificationsHub {
  private conn: HubConnection

  constructor(conn: HubConnection) {
    this.conn = conn
  }

  onReceiveMessage(handler: (message: string) => void) {
    this.conn.on('ReceiveMessage', handler)
  }

  sendToAll(message: string) {
    return this.conn.invoke('SendToAll', message)
  }

  sendToUser(userName: string, message: string) {
    return this.conn.invoke('SendToUser', userName, message)
  }

  add(a: number, b: number) {
    return this.conn.invoke<number>('Add', a, b)
  }
}

