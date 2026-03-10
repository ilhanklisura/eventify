<script lang="ts">
import SignIn from './SignIn.vue'
import Register from './Register.vue'
import Unauthorized from './Unauthorized.vue'

export default {
  $routes: [
    { path: '', name: 'auth.default', redirect: { name: 'auth.sign-in' } },
    { path: 'sign-in', name: 'auth.sign-in', component: SignIn },
    { path: 'register', name: 'auth.register', component: Register },
    { path: 'unauthorized', name: 'auth.unauthorized', component: Unauthorized },
  ],
}
</script>

<template>
  <v-main>
    <v-container class="d-flex align-center justify-center h-100">
      <router-view />
    </v-container>
  </v-main>
</template>
