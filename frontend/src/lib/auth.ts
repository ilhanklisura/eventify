import { ref, computed } from 'vue'
import { useStorage } from '@vueuse/core'
import type { Token } from './api/resources/token'
import type { User } from './api/resources/user'
import type { PermissionList } from './api/resources/permission'
import { useApi } from './api'

const tokenKey = import.meta.env.VITE_TOKEN_KEY || 'eventify_token'
const userKey = import.meta.env.VITE_USER_KEY || 'eventify_user'
const permissionsKey = import.meta.env.VITE_PERMISSIONS_KEY || 'eventify_permissions'
const rememberKey = import.meta.env.VITE_REMEMBER_KEY || 'eventify_remember'

const token = useStorage<string | null>(tokenKey, null, localStorage)
const storedUser = useStorage<string | null>(userKey, null, localStorage)
const storedPermissions = useStorage<string | null>(permissionsKey, null, localStorage)
const remembered = useStorage<string>(rememberKey, '', localStorage)

const identity = ref<User | null>(null)
const permissions = ref<PermissionList | null>(null)
const isFirstLogin = ref(false)
const isSignedIn = computed(() => identity.value !== null)

function parseUser(): User | null {
  try {
    const raw = storedUser.value
    return raw ? JSON.parse(raw) : null
  } catch {
    return null
  }
}

function parsePermissions(): PermissionList | null {
  try {
    const raw = storedPermissions.value
    return raw ? JSON.parse(raw) : null
  } catch {
    return null
  }
}

export function useAuth() {
  const { clearToken, setToken } = useApi()

  function clearIdentity() {
    token.value = null
    storedUser.value = null
    storedPermissions.value = null
    identity.value = null
    permissions.value = null
    isFirstLogin.value = false
    clearToken()
  }

  function setIdentity(tokenResponse: Token) {
    token.value = tokenResponse.value
    identity.value = tokenResponse.user
    permissions.value = tokenResponse.permissions ?? null
    isFirstLogin.value = tokenResponse.isFirstLogin
    storedUser.value = JSON.stringify(tokenResponse.user)
    storedPermissions.value = tokenResponse.permissions ? JSON.stringify(tokenResponse.permissions) : null
    setToken(tokenResponse.value)
  }

  async function init(): Promise<void> {
    if (!token.value) return
    setToken(token.value)
    identity.value = parseUser()
    permissions.value = parsePermissions()
    if (!identity.value) identity.value = null
  }

  async function signIn(email: string, password: string, remember: boolean): Promise<boolean> {
    remembered.value = remember ? email : ''
    const { tokenApi } = useApi()
    const resp = await tokenApi.login(email, password)
    if (!resp.ok) {
      clearIdentity()
      return false
    }
    setIdentity(resp.data)
    return true
  }

  function signOut() {
    clearIdentity()
  }

  function hasPermission(name: string): boolean {
    const list = permissions.value?.items
    return !!list?.some((p) => p.name === name)
  }

  return {
    initAuth: init,
    signIn,
    signOut,
    setIdentity,
    isSignedIn,
    remembered,
    identity,
    permissions,
    isFirstLogin,
    hasPermission,
  }
}
