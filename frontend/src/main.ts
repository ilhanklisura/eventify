import './assets/main.css'
import createApp from '@/lib/core'
import i18n from './i18n'
import Main from '@/app/Main.vue'
import { useSettings } from '@/lib/settings'
import { useAuth } from '@/lib/auth'

const { initAuth } = useAuth()
const { initSettings } = useSettings()

async function main() {
  await initAuth()
  await initSettings()
  const app = createApp(Main)
  app.use(i18n)
  app.mount('#app')
}

main()
