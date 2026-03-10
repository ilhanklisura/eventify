<script setup lang="ts">
import NavigationItem from './NavigationItem.vue'
import { useAuth } from '@/lib/auth'

const { hasPermission } = useAuth()
</script>

<template>
  <v-list density="compact" class="px-3" nav>
    <NavigationItem title="Dashboard" to="home.dashboard" icon="dashboard" />
    <v-divider class="my-2" />
    <NavigationItem title="Events" to="home.events" icon="event" />
    <NavigationItem title="Categories" to="home.categories" icon="category" />
    <NavigationItem title="Venues" to="home.venues" icon="place" />
    <NavigationItem title="Tickets" to="home.tickets" icon="confirmation_number" />
    <NavigationItem title="Bookings" to="home.bookings" icon="book_online" />
    <NavigationItem v-if="hasPermission('user_list')" title="Users" to="home.users" icon="people" />
  </v-list>
</template>
