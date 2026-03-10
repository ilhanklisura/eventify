import type { App } from 'vue'
import { createVuetify, type ThemeDefinition } from 'vuetify'
import { aliases, md } from 'vuetify/iconsets/md'
import 'vuetify/styles'
import 'material-design-icons-iconfont/dist/material-design-icons.css'

const defaultProps = {
  global: { density: 'comfortable', hideDetails: 'auto' as const },
  VBtn: { variant: 'flat' as const, color: 'primary' },
  VTextField: { variant: 'outlined' as const },
  VTextarea: { variant: 'outlined' as const },
  VSelect: { variant: 'outlined' as const },
}

const theme: ThemeDefinition = {
  dark: false,
  colors: {
    primary: '#3f51b5',
    secondary: '#2196f3',
  },
}

export default function useVuetify(app: App) {
  const vuetify = createVuetify({
    defaults: defaultProps,
    theme: { defaultTheme: 'eventify', themes: { eventify: theme } },
    icons: { defaultSet: 'md', aliases, sets: { md } },
  })
  app.use(vuetify)
}
