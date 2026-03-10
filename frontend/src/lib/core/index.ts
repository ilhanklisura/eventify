import createVueApp, { type Component } from './vue'
import useRouter, { type RoutesOptions } from './router'
import useVuetify from './vuetify'
import useToastify from './toastify'

type MainComponent = Component & RoutesOptions

export default function createApp(main: MainComponent) {
  const app = createVueApp(main)
  useRouter(app, main)
  useVuetify(app)
  useToastify(app)
  return app
}
