<template>
  <v-card class="pa-6" width="24rem" elevation="3">
    <v-card-title class="text-h5">Unauthorized</v-card-title>
    <v-card-text>You do not have permission to view this page.</v-card-text>
    <v-card-actions>
      <v-btn :to="{ name: 'auth.sign-in' }">Sign in</v-btn>
    </v-card-actions>
  </v-card>
</template>
