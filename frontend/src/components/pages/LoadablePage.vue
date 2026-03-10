<script setup lang="ts">
import { onMounted } from 'vue'
import TitledPage from './TitledPage.vue'
withDefaults(defineProps<{ loading?: boolean; skeleton?: string; title?: string; icon?: string }>(), { skeleton: 'article' })
const emit = defineEmits<{ load: [] }>()
onMounted(() => emit('load'))
</script>

<template>
  <TitledPage :title="title" :icon="icon" :loading="loading">
    <template #buttons>
      <slot name="buttons" />
      <v-btn @click="emit('load')" :disabled="loading" icon="refresh" variant="text" title="Reload" />
    </template>
    <v-card-text v-if="loading">
      <v-skeleton-loader :type="skeleton" />
    </v-card-text>
    <slot v-else />
  </TitledPage>
</template>
