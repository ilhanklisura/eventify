import { createI18n } from 'vue-i18n'

const messages = {
  en: {
    welcome: 'Welcome to Eventify',
    login: {
      signIn: 'Sign in',
      signUp: 'Sign up',
      email: 'Email',
      password: 'Password',
      rememberMe: 'Remember me',
      logout: 'Sign out',
    },
    common: { save: 'Save', cancel: 'Cancel', delete: 'Delete', edit: 'Edit', add: 'Add' },
    dashboard: 'Dashboard',
    events: 'Events',
    categories: 'Categories',
    venues: 'Venues',
    tickets: 'Tickets',
    bookings: 'Bookings',
    users: 'Users',
  },
}

export default createI18n({
  legacy: false,
  locale: 'en',
  fallbackLocale: 'en',
  messages,
})
