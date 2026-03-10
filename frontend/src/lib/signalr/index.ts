import { ref } from 'vue'
import { createConnection } from './conn'
import { NotificationsHub } from './hubs/notifications'

const started = ref(false)
const connection = createConnection('/hubs/notifications')
const notificationsHub = new NotificationsHub(connection)

export function useSignalR() {
  const start = async () => {
    if (started.value) return
    await connection.start()
    started.value = true
  }

  const stop = async () => {
    if (!started.value) return
    await connection.stop()
    started.value = false
  }

  return { start, stop, started, notificationsHub }
}

