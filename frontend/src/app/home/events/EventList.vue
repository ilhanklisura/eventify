<script setup lang="ts">
import { ref, onMounted } from 'vue'
import { useApi } from '@/lib/api'
import { useAuth } from '@/lib/auth'
import { toast } from 'vue3-toastify'
import LoadablePage from '@/components/pages/LoadablePage.vue'
import type { Event } from '@/lib/api/resources/event'
import type { Category } from '@/lib/api/resources/category'
import type { Venue } from '@/lib/api/resources/venue'
import ConfirmDialog from '@/components/dialogs/ConfirmDialog.vue'

const { eventApi, categoryApi, venueApi } = useApi()
const { hasPermission, identity } = useAuth()

const loading = ref(true)
const events = ref<Event[]>([])
const categories = ref<Category[]>([])
const venues = ref<Venue[]>([])

const dialog = ref(false)
const editing = ref<Event | null>(null)
const form = ref({
  title: '',
  description: '',
  date: new Date().toISOString().slice(0, 10),
  categoryId: 0,
  venueId: 0,
})
const confirmOpen = ref(false)
const pendingDeleteId = ref<number | null>(null)

async function load() {
  loading.value = true
  const [evRes, catRes, venRes] = await Promise.all([
    eventApi.getAll(),
    categoryApi.getAll(),
    venueApi.getAll(),
  ])
  if (evRes.ok) events.value = evRes.data
  if (catRes.ok) categories.value = catRes.data
  if (venRes.ok) venues.value = venRes.data
  loading.value = false
}

async function remove(id: number) {
  pendingDeleteId.value = id
  confirmOpen.value = true
}

async function confirmDelete() {
  const id = pendingDeleteId.value
  if (!id) return
  const res = await eventApi.delete(id)
  if (res.ok) {
    toast.success('Event deleted')
    load()
  } else toast.error(res.error.error)
}

function openCreate() {
  editing.value = null
  form.value = {
    title: '',
    description: '',
    date: new Date().toISOString().slice(0, 10),
    categoryId: categories.value[0]?.id ?? 0,
    venueId: venues.value[0]?.id ?? 0,
  }
  dialog.value = true
}

function openEdit(e: Event) {
  editing.value = e
  form.value = {
    title: e.title,
    description: e.description,
    date: new Date(e.date).toISOString().slice(0, 10),
    categoryId: e.categoryId,
    venueId: e.venueId,
  }
  dialog.value = true
}

async function save() {
  if (!form.value.title.trim() || !form.value.description.trim()) {
    toast.error('Title and description are required')
    return
  }
  if (!form.value.categoryId || !form.value.venueId) {
    toast.error('Category and venue are required')
    return
  }
  const organizerId = identity.value?.id
  if (!organizerId) {
    toast.error('You must be signed in')
    return
  }

  const payload = {
    title: form.value.title.trim(),
    description: form.value.description.trim(),
    date: new Date(form.value.date).toISOString(),
    categoryId: form.value.categoryId,
    venueId: form.value.venueId,
    organizerId,
  }

  if (editing.value) {
    const res = await eventApi.update(editing.value.id, { id: editing.value.id, ...payload })
    if (!res.ok) return toast.error(res.error.error)
    toast.success('Event updated')
  } else {
    const res = await eventApi.create(payload)
    if (!res.ok) return toast.error(res.error.error)
    toast.success('Event created')
  }

  dialog.value = false
  await load()
}

function categoryName(id: number) {
  return categories.value.find((c) => c.id === id)?.name ?? id
}
function venueName(id: number) {
  return venues.value.find((v) => v.id === id)?.name ?? id
}

onMounted(load)
</script>

<template>
  <LoadablePage title="Events" icon="event" :loading="loading" @load="load">
    <template #buttons>
      <v-btn v-if="hasPermission('event_create')" color="primary" prepend-icon="add" @click="openCreate">Add</v-btn>
    </template>
    <v-table>
      <thead>
        <tr>
          <th>Title</th>
          <th>Date</th>
          <th>Category</th>
          <th>Venue</th>
          <th>Actions</th>
        </tr>
      </thead>
      <tbody>
        <tr v-for="e in events" :key="e.id">
          <td>{{ e.title }}</td>
          <td>{{ new Date(e.date).toLocaleDateString() }}</td>
          <td>{{ e.category?.name ?? categoryName(e.categoryId) }}</td>
          <td>{{ e.venue?.name ?? venueName(e.venueId) }}</td>
          <td>
            <v-btn v-if="hasPermission('event_edit')" size="small" variant="text" @click="openEdit(e)"
              >Edit</v-btn
            >
            <v-btn size="small" variant="text" color="error" @click="remove(e.id)">Delete</v-btn>
          </td>
        </tr>
      </tbody>
    </v-table>

    <v-dialog v-model="dialog" max-width="720">
      <v-card>
        <v-card-title class="text-h6">{{ editing ? 'Edit event' : 'New event' }}</v-card-title>
        <v-card-text>
          <v-row>
            <v-col cols="12" md="6">
              <v-text-field v-model="form.title" label="Title" />
            </v-col>
            <v-col cols="12" md="6">
              <v-text-field v-model="form.date" label="Date" type="date" />
            </v-col>
            <v-col cols="12">
              <v-textarea v-model="form.description" label="Description" />
            </v-col>
            <v-col cols="12" md="6">
              <v-select
                v-model="form.categoryId"
                :items="categories.map(c => ({ title: c.name, value: c.id }))"
                label="Category"
              />
            </v-col>
            <v-col cols="12" md="6">
              <v-select
                v-model="form.venueId"
                :items="venues.map(v => ({ title: v.name, value: v.id }))"
                label="Venue"
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
      title="Delete event"
      message="Are you sure you want to delete this event?"
      confirm-text="Delete"
      confirm-color="error"
      @confirm="confirmDelete"
    />

    <p v-if="!loading && events.length === 0" class="text-medium-emphasis">No events yet.</p>
  </LoadablePage>
</template>
