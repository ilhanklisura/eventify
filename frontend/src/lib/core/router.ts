import type { App } from 'vue'
import { type RouteRecordRaw, createRouter, createWebHashHistory } from 'vue-router'

export interface RoutesOptions {
  $routes: RouteRecordRaw[]
}

declare module 'vue' {
  interface ComponentCustomOptions extends RoutesOptions {}
}

export default function useRouter(app: App, main: RoutesOptions) {
  const router = createRouter({
    history: createWebHashHistory(),
    routes: main.$routes,
  })
  app.use(router)
}
