function required(name: string) {
  return function (value: string | null | undefined) {
    if (value === '' || value === null || value === undefined) return `${name} is required`
    return true
  }
}

function minLength(name: string, length: number) {
  return function (value: string | null | undefined) {
    if (value == null || value.length < length) return `${name} must be at least ${length} characters`
    return true
  }
}

function email(name: string) {
  return function (value: string) {
    if (!/^[^@]+@[^@]+\.[^@]+$/i.test(value)) return `${name} must be a valid email`
    return true
  }
}

export const validators = { required, minLength, email }

export function useValidation<T>(fn: (v: typeof validators) => T) {
  return fn(validators)
}
