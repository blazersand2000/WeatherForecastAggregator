import { ref } from 'vue'
import type { ForecastsResponse } from '@/types/ForecastsResponse'
import type { LocationResponse } from '@/types/LocationResponse'

const baseUrl = 'https://localhost:7004/api/WeatherForecast'

export function useWeatherApi() {
  const isFetching = ref(false)
  const error = ref('')
  const location = ref<LocationResponse | null>(null)
  const forecasts = ref<ForecastsResponse | null>(null)

  const getForecasts = async (searchTerm: string) => {
    isFetching.value = true
    error.value = ''
    try {
      const locationResponse = await fetch(`${baseUrl}/location?search=${searchTerm}`)
      if (!locationResponse.ok) {
        error.value = 'Error fetching location data: ' + locationResponse.statusText
        return
      }
      const locationData: LocationResponse = await locationResponse.json()
      location.value = locationData
      const forecastsResponse = await fetch(
        `${baseUrl}/forecasts?lat=${locationData.coordinates.latitude}&lon=${locationData.coordinates.longitude}`
      )
      if (!forecastsResponse.ok) {
        error.value = 'Error fetching forecast data: ' + forecastsResponse.statusText
        return
      }
      const forecastData: ForecastsResponse = await forecastsResponse.json()
      forecasts.value = forecastData
    } catch (e) {
      error.value = 'Error: ' + e.message
    } finally {
      isFetching.value = false
    }
  }

  return { isFetching, error, location, forecasts, getForecasts }
}
