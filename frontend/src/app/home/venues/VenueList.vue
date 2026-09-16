<script setup lang="ts">
import { ref, onMounted } from 'vue'
import { useApi } from '@/lib/api'
import { toast } from 'vue3-toastify'
import LoadablePage from '@/components/pages/LoadablePage.vue'
import type { Venue } from '@/lib/api/resources/venue'
import ConfirmDialog from '@/components/dialogs/ConfirmDialog.vue'

const { venueApi } = useApi()
const loading = ref(true)
const items = ref<Venue[]>([])
const dialog = ref(false)
const editing = ref<Venue | null>(null)
const name = ref('')
const location = ref('')
const confirmOpen = ref(false)
const pendingDeleteId = ref<number | null>(null)

async function load() {
  loading.value = true
  const res = await venueApi.getAll()
  if (res.ok) items.value = res.data
  loading.value = false
}

function openCreate() {
  editing.value = null
  name.value = ''
  location.value = ''
  dialog.value = true
}

function openEdit(v: Venue) {
  editing.value = v
  name.value = v.name
  location.value = v.location
  dialog.value = true
}

async function save() {
  const n = name.value.trim()
  const loc = location.value.trim()
  if (!n || !loc) {
    toast.error('Name and location are required')
    return
  }

  if (editing.value) {
    const res = await venueApi.update(editing.value.id, { id: editing.value.id, name: n, location: loc })
    if (!res.ok) return toast.error(res.error.error)
    toast.success('Venue updated')
  } else {
    const res = await venueApi.create({ name: n, location: loc })
    if (!res.ok) return toast.error(res.error.error)
    toast.success('Venue created')
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
  const res = await venueApi.delete(id)
  if (res.ok) {
    toast.success('Venue deleted')
    load()
  } else toast.error(res.error.error)
}

onMounted(load)
</script>

<template>
  <LoadablePage title="Venues" icon="place" :loading="loading" @load="load">
    <template #buttons>
      <v-btn color="primary" prepend-icon="add" @click="openCreate">Add</v-btn>
    </template>

    <v-table>
      <thead>
        <tr>
          <th>Name</th>
          <th>Location</th>
          <th>Actions</th>
        </tr>
      </thead>
      <tbody>
        <tr v-for="v in items" :key="v.id">
          <td>{{ v.name }}</td>
          <td>{{ v.location }}</td>
          <td>
            <v-btn size="small" variant="text" @click="openEdit(v)">Edit</v-btn>
            <v-btn size="small" variant="text" color="error" @click="remove(v.id)">Delete</v-btn>
          </td>
        </tr>
      </tbody>
    </v-table>

    <v-dialog v-model="dialog" max-width="520">
      <v-card>
        <v-card-title class="text-h6">
          {{ editing ? 'Edit venue' : 'New venue' }}
        </v-card-title>
        <v-card-text>
          <v-text-field v-model="name" label="Name" />
          <v-text-field v-model="location" label="Location" />
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
      title="Delete venue"
      message="Are you sure you want to delete this venue?"
      confirm-text="Delete"
      confirm-color="error"
      @confirm="confirmDelete"
    />

    <p v-if="!loading && items.length === 0" class="text-medium-emphasis">No venues yet.</p>
  </LoadablePage>
</template>
