<script setup lang="ts">
import { ref, onMounted } from 'vue'
import { useApi } from '@/lib/api'
import { toast } from 'vue3-toastify'
import LoadablePage from '@/components/pages/LoadablePage.vue'
import type { Booking } from '@/lib/api/resources/booking'
import type { Event } from '@/lib/api/resources/event'
import type { User } from '@/lib/api/resources/user'
import ConfirmDialog from '@/components/dialogs/ConfirmDialog.vue'

const { bookingApi, eventApi, userApi } = useApi()
const loading = ref(true)
const items = ref<Booking[]>([])
const events = ref<Event[]>([])
const users = ref<User[]>([])

const dialog = ref(false)
const editing = ref<Booking | null>(null)
const form = ref({
  userId: 0,
  eventId: 0,
})
const confirmOpen = ref(false)
const pendingDeleteId = ref<number | null>(null)

async function load() {
  loading.value = true
  const [res, evRes, uRes] = await Promise.all([bookingApi.getAll(), eventApi.getAll(), userApi.getAll()])
  if (res.ok) items.value = res.data
  if (evRes.ok) events.value = evRes.data
  if (uRes.ok) users.value = uRes.data
  loading.value = false
}

function openCreate() {
  editing.value = null
  form.value = {
    userId: users.value[0]?.id ?? 0,
    eventId: events.value[0]?.id ?? 0,
  }
  dialog.value = true
}

async function save() {
  if (!form.value.userId || !form.value.eventId) return toast.error('User and event are required')
  const res = await bookingApi.create({ userId: form.value.userId, eventId: form.value.eventId })
  if (!res.ok) return toast.error(res.error.error)
  toast.success('Booking created')
  dialog.value = false
  await load()
}

async function remove(id: number) {
  pendingDeleteId.value = id
  confirmOpen.value = true
}

async function confirmDelete() {
  const id = pendingDeleteId.value
  if (!id) return
  const res = await bookingApi.delete(id)
  if (res.ok) {
    toast.success('Booking deleted')
    load()
  } else toast.error(res.error.error)
}

onMounted(load)
</script>

<template>
  <LoadablePage title="Bookings" icon="book_online" :loading="loading" @load="load">
    <template #buttons>
      <v-btn color="primary" prepend-icon="add" @click="openCreate">Add</v-btn>
    </template>
    <v-table>
      <thead>
        <tr>
          <th>User</th>
          <th>Event</th>
          <th>Date</th>
          <th>Actions</th>
        </tr>
      </thead>
      <tbody>
        <tr v-for="b in items" :key="b.id">
          <td>{{ b.user?.name ?? b.userId }}</td>
          <td>{{ b.event?.title ?? b.eventId }}</td>
          <td>{{ b.event?.date ? new Date(b.event.date).toLocaleDateString() : '-' }}</td>
          <td>
            <v-btn size="small" variant="text" color="error" @click="remove(b.id)">Delete</v-btn>
          </td>
        </tr>
      </tbody>
    </v-table>

    <v-dialog v-model="dialog" max-width="720">
      <v-card>
        <v-card-title class="text-h6">New booking</v-card-title>
        <v-card-text>
          <v-row>
            <v-col cols="12" md="6">
              <v-select
                v-model="form.userId"
                :items="users.map(u => ({ title: `${u.name} (${u.email})`, value: u.id }))"
                label="User"
              />
            </v-col>
            <v-col cols="12" md="6">
              <v-select
                v-model="form.eventId"
                :items="events.map(e => ({ title: e.title, value: e.id }))"
                label="Event"
              />
            </v-col>
          </v-row>
        </v-card-text>
        <v-card-actions>
          <v-spacer />
          <v-btn variant="text" @click="dialog = false">Cancel</v-btn>
          <v-btn color="primary" @click="save">Save</v-btn>
        </v-card-actions>
      </v-card>
    </v-dialog>

    <ConfirmDialog
      v-model="confirmOpen"
      title="Delete booking"
      message="Are you sure you want to delete this booking?"
      confirm-text="Delete"
      confirm-color="error"
      @confirm="confirmDelete"
    />

    <p v-if="!loading && items.length === 0" class="text-medium-emphasis">No bookings yet.</p>
  </LoadablePage>
</template>
