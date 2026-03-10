<script lang="ts">
import Dashboard from './Dashboard.vue'
import EventList from './events/EventList.vue'
import CategoryList from './categories/CategoryList.vue'
import VenueList from './venues/VenueList.vue'
import TicketList from './tickets/TicketList.vue'
import BookingList from './bookings/BookingList.vue'
import UserList from './users/UserList.vue'
import { useAuth } from '@/lib/auth'

export default {
  $routes: [
    { path: '', name: 'home.default', redirect: { name: 'home.dashboard' } },
    { path: 'dashboard', name: 'home.dashboard', component: Dashboard },
    { path: 'events', name: 'home.events', component: EventList },
    { path: 'categories', name: 'home.categories', component: CategoryList },
    { path: 'venues', name: 'home.venues', component: VenueList },
    { path: 'tickets', name: 'home.tickets', component: TicketList },
    { path: 'bookings', name: 'home.bookings', component: BookingList },
    {
      path: 'users',
      name: 'home.users',
      component: UserList,
      beforeEnter: (_to: any, _from: any, next: any) => {
        const { hasPermission } = useAuth()
        if (hasPermission('user_list')) next()
        else next({ name: 'auth.unauthorized' })
      },
    },
  ],
}
</script>

<template>
  <div class="h-100 w-100">
    <SideBar v-model="drawer" />
    <TopBar v-model="drawer" />
    <v-main>
      <v-container>
        <router-view />
      </v-container>
    </v-main>
  </div>
</template>

<script setup lang="ts">
import { ref } from 'vue'
import SideBar from '@/components/layout/SideBar.vue'
import TopBar from '@/components/layout/TopBar.vue'
import { useSignalR } from '@/lib/signalr'
import { toast } from 'vue3-toastify'
const drawer = ref(true)

const { start, notificationsHub } = useSignalR()
start().catch(() => {}) // don't block UI on startup
notificationsHub.onReceiveMessage((message) => {
  toast.info(`Notification: ${message}`)
})
</script>
