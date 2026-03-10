<script setup lang="ts">
import CardPage from './CardPage.vue'
defineProps<{ title?: string; subtitle?: string; icon?: string; loading?: boolean }>()
</script>

<template>
  <CardPage nowrap>
    <v-card-item :title="title" :subtitle="subtitle">
      <template v-if="icon" #prepend>
        <v-icon class="mr-2" :icon="icon" size="large" />
      </template>
      <template #append>
        <slot name="buttons" />
      </template>
    </v-card-item>
    <v-divider />
    <v-card-text>
      <slot />
    </v-card-text>
  </CardPage>
</template>
