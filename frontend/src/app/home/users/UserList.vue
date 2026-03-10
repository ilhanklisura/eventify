<script setup lang="ts">
import { ref, onMounted } from 'vue'
import { useApi } from '@/lib/api'
import { useAuth } from '@/lib/auth'
import { toast } from 'vue3-toastify'
import LoadablePage from '@/components/pages/LoadablePage.vue'
import type { User } from '@/lib/api/resources/user'
import ConfirmDialog from '@/components/dialogs/ConfirmDialog.vue'

const { userApi } = useApi()
const { hasPermission } = useAuth()
const loading = ref(true)
const items = ref<User[]>([])
const dialog = ref(false)
const editing = ref<User | null>(null)
const form = ref({
  name: '',
  email: '',
  role: 'attendee',
  password: '',
})
const confirmOpen = ref(false)
const pendingDeleteId = ref<number | null>(null)

async function load() {
  loading.value = true
  const res = await userApi.getAll()
  if (res.ok) items.value = res.data
  loading.value = false
}

function openCreate() {
  editing.value = null
  form.value = { name: '', email: '', role: 'attendee', password: '' }
  dialog.value = true
}

function openEdit(u: User) {
  editing.value = u
  form.value = { name: u.name, email: u.email, role: u.role, password: '' }
  dialog.value = true
}

async function save() {
  if (!hasPermission('user_list')) return toast.error('Forbidden')
  const name = form.value.name.trim()
  const email = form.value.email.trim()
  if (!name || !email) return toast.error('Name and email are required')

  if (editing.value) {
    const res = await userApi.update(editing.value.id, {
      id: editing.value.id,
      name,
      email,
      role: form.value.role,
      password: form.value.password ? form.value.password : undefined,
    })
    if (!res.ok) return toast.error(res.error.error)
    toast.success('User updated')
  } else {
    if (!form.value.password) return toast.error('Password is required for new user')
    const res = await userApi.create({
      name,
      email,
      password: form.value.password,
      role: form.value.role,
    })
    if (!res.ok) return toast.error(res.error.error)
    toast.success('User created')
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
  const res = await userApi.delete(id)
  if (res.ok) {
    toast.success('User deleted')
    load()
  } else toast.error(res.error.error)
}

onMounted(load)
</script>

<template>
  <LoadablePage title="Users" icon="people" :loading="loading" @load="load">
    <template #buttons>
      <v-btn v-if="hasPermission('user_list')" color="primary" prepend-icon="add" @click="openCreate"
        >Add</v-btn
      >
    </template>
    <v-table>
      <thead>
        <tr>
          <th>Name</th>
          <th>Email</th>
          <th>Role</th>
          <th v-if="hasPermission('user_list')"></th>
        </tr>
      </thead>
      <tbody>
        <tr v-for="u in items" :key="u.id">
          <td>{{ u.name }}</td>
          <td>{{ u.email }}</td>
          <td>{{ u.role }}</td>
          <td v-if="hasPermission('user_list')">
            <v-btn size="small" variant="text" @click="openEdit(u)">Edit</v-btn>
            <v-btn size="small" variant="text" color="error" @click="remove(u.id)">Delete</v-btn>
          </td>
        </tr>
      </tbody>
    </v-table>

    <v-dialog v-model="dialog" max-width="720">
      <v-card>
        <v-card-title class="text-h6">{{ editing ? 'Edit user' : 'New user' }}</v-card-title>
        <v-card-text>
          <v-row>
            <v-col cols="12" md="6">
              <v-text-field v-model="form.name" label="Name" />
            </v-col>
            <v-col cols="12" md="6">
              <v-text-field v-model="form.email" label="Email" type="email" />
            </v-col>
            <v-col cols="12" md="6">
              <v-select
                v-model="form.role"
                :items="[
                  { title: 'Attendee', value: 'attendee' },
                  { title: 'Organizer', value: 'organizer' },
                  { title: 'Admin', value: 'admin' },
                ]"
                label="Role"
              />
            </v-col>
            <v-col cols="12" md="6">
              <v-text-field
                v-model="form.password"
                label="Password (leave empty to keep)"
                type="password"
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
      title="Delete user"
      message="Are you sure you want to delete this user?"
      confirm-text="Delete"
      confirm-color="error"
      @confirm="confirmDelete"
    />

    <p v-if="!loading && items.length === 0" class="text-medium-emphasis">No users yet.</p>
  </LoadablePage>
</template>
