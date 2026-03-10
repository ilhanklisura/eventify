<script setup lang="ts">
import { ref, onMounted } from 'vue'
import { useApi } from '@/lib/api'
import { toast } from 'vue3-toastify'
import LoadablePage from '@/components/pages/LoadablePage.vue'
import type { Ticket } from '@/lib/api/resources/ticket'
import type { Event } from '@/lib/api/resources/event'
import type { User } from '@/lib/api/resources/user'
import ConfirmDialog from '@/components/dialogs/ConfirmDialog.vue'

const { ticketApi, eventApi, userApi } = useApi()
const loading = ref(true)
const items = ref<Ticket[]>([])
const events = ref<Event[]>([])
const users = ref<User[]>([])

const dialog = ref(false)
const editing = ref<Ticket | null>(null)
const form = ref({
  eventId: 0,
  userId: 0,
  price: 0,
  status: 'Available',
})
const confirmOpen = ref(false)
const pendingDeleteId = ref<number | null>(null)

async function load() {
  loading.value = true
  const [res, evRes, uRes] = await Promise.all([ticketApi.getAll(), eventApi.getAll(), userApi.getAll()])
  if (res.ok) items.value = res.data
  if (evRes.ok) events.value = evRes.data
  if (uRes.ok) users.value = uRes.data
  loading.value = false
}

function openCreate() {
  editing.value = null
  form.value = {
    eventId: events.value[0]?.id ?? 0,
    userId: users.value[0]?.id ?? 0,
    price: 0,
    status: 'Available',
  }
  dialog.value = true
}

function openEdit(t: Ticket) {
  editing.value = t
  form.value = {
    eventId: t.eventId,
    userId: t.userId,
    price: t.price,
    status: t.status,
  }
  dialog.value = true
}

async function save() {
  if (!form.value.eventId || !form.value.userId) return toast.error('Event and user are required')
  if (form.value.price < 0) return toast.error('Price must be >= 0')

  if (editing.value) {
    const res = await ticketApi.update(editing.value.id, {
      id: editing.value.id,
      eventId: form.value.eventId,
      userId: form.value.userId,
      price: form.value.price,
      status: form.value.status,
    })
    if (!res.ok) return toast.error(res.error.error)
    toast.success('Ticket updated')
  } else {
    const res = await ticketApi.create({
      eventId: form.value.eventId,
      userId: form.value.userId,
      price: form.value.price,
      status: form.value.status,
    })
    if (!res.ok) return toast.error(res.error.error)
    toast.success('Ticket created')
  }

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
  const res = await ticketApi.delete(id)
  if (res.ok) {
    toast.success('Ticket deleted')
    load()
  } else toast.error(res.error.error)
}

onMounted(load)
</script>

<template>
  <LoadablePage title="Tickets" icon="confirmation_number" :loading="loading" @load="load">
    <template #buttons>
      <v-btn color="primary" prepend-icon="add" @click="openCreate">Add</v-btn>
    </template>
    <v-table>
      <thead>
        <tr>
          <th>Event</th>
          <th>User</th>
          <th>Price</th>
          <th>Status</th>
          <th></th>
        </tr>
      </thead>
      <tbody>
        <tr v-for="t in items" :key="t.id">
          <td>{{ t.event?.title ?? t.eventId }}</td>
          <td>{{ t.user?.name ?? t.userId }}</td>
          <td>{{ t.price }}</td>
          <td>{{ t.status }}</td>
          <td>
            <v-btn size="small" variant="text" @click="openEdit(t)">Edit</v-btn>
            <v-btn size="small" variant="text" color="error" @click="remove(t.id)">Delete</v-btn>
          </td>
        </tr>
      </tbody>
    </v-table>

    <v-dialog v-model="dialog" max-width="720">
      <v-card>
        <v-card-title class="text-h6">{{ editing ? 'Edit ticket' : 'New ticket' }}</v-card-title>
        <v-card-text>
          <v-row>
            <v-col cols="12" md="6">
              <v-select
                v-model="form.eventId"
                :items="events.map(e => ({ title: e.title, value: e.id }))"
                label="Event"
              />
            </v-col>
            <v-col cols="12" md="6">
              <v-select
                v-model="form.userId"
                :items="users.map(u => ({ title: `${u.name} (${u.email})`, value: u.id }))"
                label="User"
              />
            </v-col>
            <v-col cols="12" md="6">
              <v-text-field v-model.number="form.price" label="Price" type="number" />
            </v-col>
            <v-col cols="12" md="6">
              <v-select
                v-model="form.status"
                :items="['Available', 'Sold', 'Cancelled']"
                label="Status"
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
      title="Delete ticket"
      message="Are you sure you want to delete this ticket?"
      confirm-text="Delete"
      confirm-color="error"
      @confirm="confirmDelete"
    />

    <p v-if="!loading && items.length === 0" class="text-medium-emphasis">No tickets yet.</p>
  </LoadablePage>
</template>
