<script setup lang="ts">
import { computed, onMounted, ref } from 'vue'
import { useRouter } from 'vue-router'
import { toast } from 'vue3-toastify'

import LoadablePage from '@/components/pages/LoadablePage.vue'
import CardPage from '@/components/pages/CardPage.vue'

import { useApi } from '@/lib/api'
import { useAuth } from '@/lib/auth'

import type { Event } from '@/lib/api/resources/event'
import type { Booking } from '@/lib/api/resources/booking'
import type { User } from '@/lib/api/resources/user'

const router = useRouter()
const { identity, hasPermission } = useAuth()
const { eventApi, bookingApi, userApi } = useApi()

const loading = ref(true)
const events = ref<Event[]>([])
const bookings = ref<Booking[]>([])
const users = ref<User[]>([])

const roleLabel = computed(() => identity.value?.role ?? 'user')
const isAdmin = computed(() => roleLabel.value === 'admin' || hasPermission('user_list'))
const canSeeUsers = computed(() => hasPermission('user_list'))
const canSeeBookings = computed(() => hasPermission('booking_list'))
const canSeeEvents = computed(() => hasPermission('event_list'))

const now = () => new Date()
const upcomingEvents = computed(() => {
  const t = now().getTime()
  return events.value
    .filter((e) => new Date(e.date).getTime() >= t)
    .sort((a, b) => new Date(a.date).getTime() - new Date(b.date).getTime())
})

const nextEvent = computed(() => upcomingEvents.value[0] ?? null)

const recentBookings = computed(() => {
  return bookings.value
    .slice()
    .sort((a, b) => new Date(b.createdAt).getTime() - new Date(a.createdAt).getTime())
    .slice(0, 5)
})

const counts = computed(() => {
  return {
    users: users.value.length,
    events: events.value.length,
    upcoming: upcomingEvents.value.length,
    bookings: bookings.value.length,
  }
})

async function load() {
  loading.value = true

  const tasks: Promise<any>[] = []

  if (canSeeEvents.value) {
    tasks.push(
      eventApi.getAll().then((r) => {
        if (r.ok) events.value = r.data
      })
    )
  } else {
    events.value = []
  }

  if (canSeeBookings.value) {
    tasks.push(
      bookingApi.getAll().then((r) => {
        if (r.ok) bookings.value = r.data
      })
    )
  } else {
    bookings.value = []
  }

  if (canSeeUsers.value) {
    tasks.push(
      userApi.getAll().then((r) => {
        if (r.ok) users.value = r.data
      })
    )
  } else {
    users.value = []
  }

  try {
    await Promise.all(tasks)
  } catch (e) {
    toast.error(String(e))
  } finally {
    loading.value = false
  }
}

function go(name: string) {
  router.push({ name })
}

onMounted(load)
</script>

<template>
  <LoadablePage title="Dashboard" icon="dashboard" :loading="loading" @load="load" skeleton="article">
    <v-row class="mb-2">
      <v-col cols="12">
        <div class="d-flex align-center justify-space-between flex-wrap gap-2">
          <div>
            <div class="text-h5">Welcome back, {{ identity?.name ?? 'User' }}</div>
            <div class="text-body-2 text-medium-emphasis">
              Role: <strong>{{ roleLabel }}</strong>
            </div>
          </div>

          <v-chip color="primary" variant="tonal" prepend-icon="verified_user">
            {{ isAdmin ? 'Administrator view' : 'User view' }}
          </v-chip>
        </div>
      </v-col>
    </v-row>

    <!-- Stat cards -->
    <v-row>
      <v-col cols="12" sm="6" md="3" v-if="canSeeUsers">
        <CardPage class="h-100">
          <div class="d-flex align-center justify-space-between">
            <div>
              <div class="text-body-2 text-medium-emphasis">Users</div>
              <div class="text-h4">{{ counts.users }}</div>
            </div>
            <v-icon size="40" color="primary">people</v-icon>
          </div>
          <div class="mt-3">
            <v-btn size="small" variant="text" @click="go('home.users')">Manage users</v-btn>
          </div>
        </CardPage>
      </v-col>

      <v-col cols="12" sm="6" md="3" v-if="canSeeEvents">
        <CardPage class="h-100">
          <div class="d-flex align-center justify-space-between">
            <div>
              <div class="text-body-2 text-medium-emphasis">Events</div>
              <div class="text-h4">{{ counts.events }}</div>
            </div>
            <v-icon size="40" color="primary">event</v-icon>
          </div>
          <div class="mt-3">
            <v-btn size="small" variant="text" @click="go('home.events')">View events</v-btn>
          </div>
        </CardPage>
      </v-col>

      <v-col cols="12" sm="6" md="3" v-if="canSeeEvents">
        <CardPage class="h-100">
          <div class="d-flex align-center justify-space-between">
            <div>
              <div class="text-body-2 text-medium-emphasis">Upcoming</div>
              <div class="text-h4">{{ counts.upcoming }}</div>
            </div>
            <v-icon size="40" color="primary">schedule</v-icon>
          </div>
          <div class="mt-3" v-if="nextEvent">
            <div class="text-body-2">
              Next: <strong>{{ nextEvent.title }}</strong>
            </div>
            <div class="text-body-2 text-medium-emphasis">
              {{ new Date(nextEvent.date).toLocaleString() }}
            </div>
          </div>
          <div class="mt-3" v-else>
            <div class="text-body-2 text-medium-emphasis">No upcoming events.</div>
          </div>
        </CardPage>
      </v-col>

      <v-col cols="12" sm="6" md="3" v-if="canSeeBookings">
        <CardPage class="h-100">
          <div class="d-flex align-center justify-space-between">
            <div>
              <div class="text-body-2 text-medium-emphasis">Bookings</div>
              <div class="text-h4">{{ counts.bookings }}</div>
            </div>
            <v-icon size="40" color="primary">book_online</v-icon>
          </div>
          <div class="mt-3">
            <v-btn size="small" variant="text" @click="go('home.bookings')">View bookings</v-btn>
          </div>
        </CardPage>
      </v-col>
    </v-row>

    <!-- Quick actions + recent -->
    <v-row class="mt-2">
      <v-col cols="12" md="5">
        <CardPage>
          <div class="text-h6 mb-2">Quick actions</div>
          <div class="d-flex flex-wrap gap-2">
            <v-btn variant="tonal" prepend-icon="event" @click="go('home.events')" v-if="canSeeEvents"
              >Events</v-btn
            >
            <v-btn variant="tonal" prepend-icon="category" @click="go('home.categories')">Categories</v-btn>
            <v-btn variant="tonal" prepend-icon="place" @click="go('home.venues')">Venues</v-btn>
            <v-btn variant="tonal" prepend-icon="confirmation_number" @click="go('home.tickets')">Tickets</v-btn>
            <v-btn variant="tonal" prepend-icon="book_online" @click="go('home.bookings')" v-if="canSeeBookings"
              >Bookings</v-btn
            >
            <v-btn variant="tonal" prepend-icon="people" @click="go('home.users')" v-if="canSeeUsers"
              >Users</v-btn
            >
          </div>

          <v-divider class="my-4" />

          <div class="text-body-2 text-medium-emphasis">
            Tip: Use the “Add” buttons inside each page to create new items.
          </div>
        </CardPage>
      </v-col>

      <v-col cols="12" md="7" v-if="canSeeBookings">
        <CardPage>
          <div class="d-flex align-center justify-space-between mb-2">
            <div class="text-h6">Recent bookings</div>
            <v-btn size="small" variant="text" @click="go('home.bookings')">Open</v-btn>
          </div>

          <v-table density="compact">
            <thead>
              <tr>
                <th>User</th>
                <th>Event</th>
                <th>Created</th>
              </tr>
            </thead>
            <tbody>
              <tr v-for="b in recentBookings" :key="b.id">
                <td>{{ b.user?.name ?? b.userId }}</td>
                <td>{{ b.event?.title ?? b.eventId }}</td>
                <td class="text-medium-emphasis">{{ new Date(b.createdAt).toLocaleString() }}</td>
              </tr>
            </tbody>
          </v-table>

          <div v-if="!loading && recentBookings.length === 0" class="text-body-2 text-medium-emphasis mt-2">
            No bookings yet.
          </div>
        </CardPage>
      </v-col>
    </v-row>
  </LoadablePage>
</template>

<style scoped>
.gap-2 {
  gap: 8px;
}
</style>
