<script setup lang="ts">
import { useRouter } from 'vue-router'
import { useAuth } from '@/lib/auth'

const router = useRouter()
const { identity, signOut } = useAuth()

const logOut = () => {
  signOut()
  router.replace({ name: 'auth.sign-in' })
}

const drawer = defineModel<boolean>()
</script>

<template>
  <v-app-bar elevation="1">
    <v-app-bar-nav-icon class="mx-2" @click="drawer = !drawer" />
    <v-divider vertical />
    <v-toolbar-title class="text-h6">Eventify</v-toolbar-title>
    <v-spacer />
    <v-menu :close-on-content-click="false">
      <template #activator="{ props: menuProps }">
        <v-btn class="mx-2" v-bind="menuProps" icon="account_circle" />
      </template>
      <v-list nav>
        <v-list-item
          prepend-icon="person"
          :title="identity?.name ?? 'User'"
          :subtitle="identity?.email"
        />
        <v-divider class="my-2" />
        <v-list-item prepend-icon="logout" title="Sign out" @click="logOut" />
      </v-list>
    </v-menu>
  </v-app-bar>
</template>
