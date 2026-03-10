<script setup lang="ts">
import Navigation from './Navigation.vue'
const model = defineModel<boolean>()
</script>

<template>
  <v-navigation-drawer theme="dark" v-model="model" width="280">
    <v-list class="pt-3 pb-0">
      <v-list-item subtitle="Eventify">
        <template #prepend>
          <v-icon icon="event" size="large" />
        </template>
        <template #title>
          <h4 class="text-h5 text-uppercase">Eventify</h4>
        </template>
      </v-list-item>
    </v-list>
    <v-divider class="my-4" />
    <Navigation />
  </v-navigation-drawer>
</template>
