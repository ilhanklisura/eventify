/// <reference types="vite/client" />

interface ImportMetaEnv {
  readonly VITE_API_URL: string
  readonly VITE_TOKEN_KEY: string
  readonly VITE_USER_KEY: string
  readonly VITE_PERMISSIONS_KEY: string
  readonly VITE_REMEMBER_KEY: string
}

interface ImportMeta {
  readonly env: ImportMetaEnv
}
