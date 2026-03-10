import { createApp } from 'vue'

type VueParams = Parameters<typeof createApp>
export type Component = VueParams[0]

export default function createVueApp(...params: VueParams) {
  return createApp(...params)
}
