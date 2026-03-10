import type { App } from 'vue'
import Vue3Toastify, { type ToastContainerOptions } from 'vue3-toastify'
import 'vue3-toastify/dist/index.css'

export default function useToastify(app: App) {
  app.use(Vue3Toastify, {
    position: 'top-right',
    autoClose: 3000,
    theme: 'auto',
  } as ToastContainerOptions)
}
