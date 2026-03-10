<script setup lang="ts">
withDefaults(
  defineProps<{ nowrap?: boolean; loading?: boolean; disabled?: boolean; elevation?: number; color?: string }>(),
  { color: 'primary', elevation: 1 }
)
</script>

<template>
  <section>
    <v-card :loading="loading" :disabled="disabled" :elevation="elevation" rounded="lg" v-bind="$attrs">
      <template v-slot:loader="{ isActive }">
        <v-progress-linear :active="isActive" :color="color" height="4" indeterminate />
      </template>
      <v-card-text v-if="!nowrap">
        <slot />
      </v-card-text>
      <slot v-else />
    </v-card>
  </section>
</template>
