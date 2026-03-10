<script setup lang="ts">
const model = defineModel<boolean>({ default: false })

withDefaults(
  defineProps<{
    title?: string
    message?: string
    confirmText?: string
    cancelText?: string
    confirmColor?: string
  }>(),
  {
    title: 'Confirm',
    message: 'Are you sure?',
    confirmText: 'Confirm',
    cancelText: 'Cancel',
    confirmColor: 'error',
  }
)

const emit = defineEmits<{
  confirm: []
  cancel: []
}>()

const onCancel = () => {
  model.value = false
  emit('cancel')
}

const onConfirm = () => {
  model.value = false
  emit('confirm')
}
</script>

<template>
  <v-dialog v-model="model" max-width="520">
    <v-card>
      <v-card-title class="text-h6">{{ title }}</v-card-title>
      <v-card-text>{{ message }}</v-card-text>
      <v-card-actions>
        <v-spacer />
        <v-btn variant="text" @click="onCancel">{{ cancelText }}</v-btn>
        <v-btn :color="confirmColor" @click="onConfirm">{{ confirmText }}</v-btn>
      </v-card-actions>
    </v-card>
  </v-dialog>
</template>

