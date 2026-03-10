export interface PermissionListItem {
  id: number
  name: string
  group: string
  displayName?: string
}

export interface PermissionList {
  items: PermissionListItem[]
}
