<script setup lang="ts">
import { ref, computed } from 'vue'
const reveal = ref(false)
const type = computed(() => (reveal.value ? 'text' : 'password'))
const icon = computed(() => (reveal.value ? 'visibility_off' : 'visibility'))
defineProps<{ modelValue?: string; label?: string; rules?: unknown[] }>()
defineEmits<{ 'update:modelValue': [value: string] }>()
</script>

<template>
  <v-text-field
    :model-value="modelValue"
    @update:model-value="$emit('update:modelValue', $event)"
    :label="label"
    :rules="rules"
    placeholder="············"
    :type="type"
    :append-inner-icon="icon"
    @click:append-inner="reveal = !reveal"
  />
</template>
