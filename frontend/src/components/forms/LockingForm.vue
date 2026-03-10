<script setup lang="ts">
import { ref, useTemplateRef } from 'vue'
import { VForm } from 'vuetify/components'
import type { SubmitEventPromise } from 'vuetify'

const emit = defineEmits<{ submit: [event: SubmitEventPromise] }>()
const locked = defineModel<boolean>('locked', { default: false })
const form = useTemplateRef<VForm>('form')

const lock = () => (locked.value = true)
const unlock = () => (locked.value = false)
const reset = () => {
  form.value?.resetValidation()
  unlock()
}

const submitted = async (event: SubmitEventPromise) => {
  if (locked.value) return
  lock()
  const result = await event
  if (!result.valid) {
    unlock()
    return
  }
  emit('submit', event)
}

defineExpose<{ locked: typeof locked; lock: () => void; unlock: () => void; reset: () => void }>({
  locked,
  lock,
  unlock,
  reset,
})
</script>

<template>
  <v-form ref="form" @submit.prevent="submitted" :disabled="locked" validate-on="submit">
    <slot :locked="locked" />
  </v-form>
</template>
