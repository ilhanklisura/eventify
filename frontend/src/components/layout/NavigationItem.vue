<script lang="ts" setup>
import { computed } from 'vue'

const props = withDefaults(
  defineProps<{ to?: string; icon?: string; title?: string; color?: string }>(),
  { color: 'primary' }
)

const to = computed(() => (props.to ? { name: props.to } : undefined))
</script>

<template>
  <v-list-item :to="to" :prepend-icon="props.icon" :color="props.color" variant="flat" rounded="lg">
    <template #title>
      <span class="text-body-2">{{ props.title }}</span>
    </template>
  </v-list-item>
</template>
