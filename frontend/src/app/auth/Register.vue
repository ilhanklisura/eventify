<script setup lang="ts">
import { reactive, useTemplateRef } from 'vue'
import { useRouter } from 'vue-router'
import { useApi } from '@/lib/api'
import { useAuth } from '@/lib/auth'
import { useValidation } from '@/lib/validation'
import { toast } from 'vue3-toastify'
import FormField from '@/components/forms/FormField.vue'
import PasswordField from '@/components/forms/PasswordField.vue'
import LockingForm, { type ILockingForm } from '@/components/forms/LockingForm.vue'

const router = useRouter()
const { tokenApi } = useApi()
const { setIdentity } = useAuth()
const form = useTemplateRef<ILockingForm>('form')

const formData = reactive({
  name: '',
  email: '',
  password: '',
})

const rules = useValidation((v) => ({
  name: [v.required('Name')],
  email: [v.required('Email'), v.email('Email')],
  password: [v.required('Password'), v.minLength('Password', 6)],
}))

const onSubmit = async () => {
  const resp = await tokenApi.register(formData.name, formData.email, formData.password)
  if (!resp.ok) {
    toast.error(resp.error.error || 'Registration failed')
    return
  }
  setIdentity(resp.data)
  toast.success('Account created')
  router.replace({ name: 'home.dashboard' })
  form.value?.reset()
}
</script>

<template>
  <v-card class="pa-5 py-8" width="28rem" elevation="3">
    <v-card-item class="justify-center pa-4 pb-6">
      <v-icon icon="person_add" color="primary" size="large" />
      <v-card-title class="text-h4 text-uppercase ms-2">Register</v-card-title>
    </v-card-item>
    <v-card-text>
      <LockingForm ref="form" @submit="onSubmit" v-slot="{ locked }">
        <v-row>
          <FormField>
            <v-text-field v-model.trim="formData.name" :rules="rules.name" label="Name" />
          </FormField>
          <FormField>
            <v-text-field v-model.trim="formData.email" :rules="rules.email" label="Email" type="email" />
          </FormField>
          <FormField>
            <PasswordField v-model.trim="formData.password" :rules="rules.password" label="Password" />
          </FormField>
          <FormField class="pb-2">
            <v-btn block type="submit" :disabled="locked">Create account</v-btn>
          </FormField>
          <FormField class="pb-0">
            <v-btn block variant="outlined" :to="{ name: 'auth.sign-in' }">Back to sign in</v-btn>
          </FormField>
        </v-row>
      </LockingForm>
    </v-card-text>
  </v-card>
</template>
