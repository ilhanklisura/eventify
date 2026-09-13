<script setup lang="ts">
import { ref, onMounted } from 'vue'
import { useApi } from '@/lib/api'
import { toast } from 'vue3-toastify'
import LoadablePage from '@/components/pages/LoadablePage.vue'
import type { Category } from '@/lib/api/resources/category'
import ConfirmDialog from '@/components/dialogs/ConfirmDialog.vue'

const { categoryApi } = useApi()
const loading = ref(true)
const items = ref<Category[]>([])
const dialog = ref(false)
const editing = ref<Category | null>(null)
const name = ref('')
const confirmOpen = ref(false)
const pendingDeleteId = ref<number | null>(null)

async function load() {
  loading.value = true
  const res = await categoryApi.getAll()
  if (res.ok) items.value = res.data
  loading.value = false
}

function openCreate() {
  editing.value = null
  name.value = ''
  dialog.value = true
}

function openEdit(c: Category) {
  editing.value = c
  name.value = c.name
  dialog.value = true
}

async function save() {
  const value = name.value.trim()
  if (!value) {
    toast.error('Name is required')
    return
  }

  if (editing.value) {
    const res = await categoryApi.update(editing.value.id, { id: editing.value.id, name: value })
    if (!res.ok) return toast.error(res.error.error)
    toast.success('Category updated')
  } else {
    const res = await categoryApi.create({ name: value })
    if (!res.ok) return toast.error(res.error.error)
    toast.success('Category created')
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
  const res = await categoryApi.delete(id)
  if (res.ok) {
    toast.success('Category deleted')
    load()
  } else toast.error(res.error.error)
}

onMounted(load)
</script>

<template>
  <LoadablePage title="Categories" icon="category" :loading="loading" @load="load">
    <template #buttons>
      <v-btn color="primary" prepend-icon="add" @click="openCreate">Add</v-btn>
    </template>

    <v-table>
      <thead>
        <tr>
          <th>Name</th>
          <th>Actions</th>
        </tr>
      </thead>
      <tbody>
        <tr v-for="c in items" :key="c.id">
          <td>{{ c.name }}</td>
          <td>
            <v-btn size="small" variant="text" @click="openEdit(c)">Edit</v-btn>
            <v-btn size="small" variant="text" color="error" @click="remove(c.id)">Delete</v-btn>
          </td>
        </tr>
      </tbody>
    </v-table>

    <v-dialog v-model="dialog" max-width="520">
      <v-card>
        <v-card-title class="text-h6">
          {{ editing ? 'Edit category' : 'New category' }}
        </v-card-title>
        <v-card-text>
          <v-text-field v-model="name" label="Name" />
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
      title="Delete category"
      message="Are you sure you want to delete this category?"
      confirm-text="Delete"
      confirm-color="error"
      @confirm="confirmDelete"
    />

    <p v-if="!loading && items.length === 0" class="text-medium-emphasis">No categories yet.</p>
  </LoadablePage>
</template>
