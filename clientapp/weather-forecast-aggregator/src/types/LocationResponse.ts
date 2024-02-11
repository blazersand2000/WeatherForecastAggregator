export interface LocationResponse {
  name: string
  coordinates: Coordinates
  timeZone: TimeZone
}

export interface Coordinates {
  latitude: number
  longitude: number
}

export interface TimeZone {
  id: string
  hasIanaId: boolean
  displayName: string
  standardName: string
  daylightName: string
  baseUtcOffset: string
  supportsDaylightSavingTime: boolean
}
