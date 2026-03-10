export type Ok<T> = { ok: true; data: T }
export type Err<E> = { ok: false; error: E; stack?: string }
export type Result<T, E> = Ok<T> | Err<E>

export function Ok<T>(data: T): Ok<T> {
  return { ok: true, data }
}

export function Err<E>(error: E): Err<E> {
  return { ok: false, error, stack: new Error().stack }
}
