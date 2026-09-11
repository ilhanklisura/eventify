<script setup lang="ts">
import { reactive, useTemplateRef } from 'vue'
import { useRouter } from 'vue-router'
import { useAuth } from '@/lib/auth'
import { useValidation } from '@/lib/validation'
import { toast } from 'vue3-toastify'
import FormField from '@/components/forms/FormField.vue'
import PasswordField from '@/components/forms/PasswordField.vue'
import LockingForm, { type ILockingForm } from '@/components/forms/LockingForm.vue'

const router = useRouter()
const { remembered, signIn } = useAuth()
const form = useTemplateRef<ILockingForm>('form')

const formData = reactive({
  email: remembered.value,
  password: '',
  remember: !!remembered.value,
})

const rules = useValidation((v) => ({
  email: [v.required('Email'), v.email('Email')],
  password: [v.required('Password')],
}))

const onSubmit = async () => {
  const ok = await signIn(formData.email, formData.password, formData.remember)
  if (ok) router.replace({ name: 'home.dashboard' })
  else toast.error('Invalid email or password')
  formData.password = ''
  form.value?.reset()
}
</script>

<template>
  <v-card class="pa-5 py-8" width="28rem" elevation="3">
    <v-card-item class="pa-4 pb-6">
      <div class="d-flex align-center justify-center ga-2">
        <v-icon icon="event" color="primary" size="large" />
        <v-card-title class="text-h4 text-uppercase pa-0">Eventify</v-card-title>
      </div>
    </v-card-item>
    <v-card-text class="pt-3">
      <h5 class="text-h5 mb-1">{{ $t('welcome') }}</h5>
      <p class="text-medium-emphasis">Sign in with your email and password.</p>
    </v-card-text>
    <v-card-text>
      <LockingForm ref="form" @submit="onSubmit" v-slot="{ locked }">
        <v-row>
          <FormField>
            <v-text-field
              v-model.trim="formData.email"
              :rules="rules.email"
              :label="$t('login.email')"
              type="email"
            />
          </FormField>
          <FormField>
            <PasswordField v-model.trim="formData.password" :rules="rules.password" :label="$t('login.password')" />
          </FormField>
          <FormField class="pt-0">
            <v-checkbox v-model="formData.remember" :label="$t('login.rememberMe')" />
          </FormField>
          <FormField class="pb-2">
            <v-btn block type="submit" :disabled="locked">{{ $t('login.signIn') }}</v-btn>
          </FormField>
          <FormField class="pb-0">
            <v-btn block variant="outlined" :to="{ name: 'auth.register' }">Register</v-btn>
          </FormField>
        </v-row>
      </LockingForm>
    </v-card-text>
  </v-card>
</template>
