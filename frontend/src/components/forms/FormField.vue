<script lang="ts" setup>
import { useId } from 'vue'
withDefaults(defineProps<{ cols?: number; label?: string }>(), { cols: 12 })
const id = useId()
</script>

<template>
  <v-col :cols="cols">
    <label v-if="label" :for="id" class="d-block pb-1 text-body-1">{{ label }}</label>
    <slot :id="id" />
  </v-col>
</template>
