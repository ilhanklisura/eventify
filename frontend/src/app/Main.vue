<script lang="ts">
import Home from './home/Index.vue'
import Auth from './auth/Index.vue'
import { useAuth } from '@/lib/auth'

const { isSignedIn } = useAuth()

export default {
  $routes: [
    { path: '/auth', name: 'auth', component: Auth, children: Auth.$routes },
    {
      path: '/home',
      name: 'home',
      component: Home,
      children: Home.$routes,
      beforeEnter: (_to, _from, next) => {
        if (isSignedIn.value) next()
        else next({ name: 'auth.sign-in' })
      },
    },
    { path: '/', redirect: { name: 'home.dashboard' } },
    { path: '/:pathMatch(.*)*', redirect: { name: 'home.dashboard' } },
  ],
}
</script>

<template>
  <v-app>
    <router-view v-slot="{ Component }">
      <transition name="fade" mode="out-in">
        <component :is="Component" />
      </transition>
    </router-view>
  </v-app>
</template>

<style scoped>
.fade-enter-active,
.fade-leave-active {
  transition: opacity 0.2s ease;
}
.fade-enter-from,
.fade-leave-to {
  opacity: 0;
}
</style>
