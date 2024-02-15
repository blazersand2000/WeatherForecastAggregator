<template>
  <!-- <v-row>
    <v-col cols="12" sm="6" md="4" v-for="(source, index) in forecasts.sources" :key="index">
      <DailyForecast :forecastSource="source" />
    </v-col>
  </v-row> -->

  <v-container fluid style="max-width: 1400px">
    <!-- <v-row>
      <v-col cols="0" md="2"></v-col>
      <v-col v-for="source in sources" :key="source" class="text-center">
        <h3>{{ source }}</h3>
      </v-col>
    </v-row> -->
    <v-row v-for="day in daySourceForecasts" :key="day[0]">
      <v-col cols="12" md="1" class="d-flex align-center justify-center">
        <h5>{{ getFormattedDate(day[0]) }}</h5>
        <!-- Row headers -->
      </v-col>
      <v-col v-for="sourceForecast in day[1]" :key="`${day[1]}-${sourceForecast[0]}`">
        <div style="min-width: 323px">
          <DailyForecastCard
            v-if="sourceForecast[1]"
            :sourceName="sourceForecast[0]"
            :dailyForecast="sourceForecast[1]"
          />
        </div>
      </v-col>
    </v-row>
  </v-container>
</template>

<script setup lang="ts">
import type { ForecastsResponse, Source, DailyForecast } from '@/types/ForecastsResponse'
import DailyForecastCard from '@/components/DailyForecastCard.vue'
import { computed, ref } from 'vue'

const props = defineProps<{
  forecasts: ForecastsResponse
}>()

const days = computed(() => {
  const allDates = props.forecasts.sources.flatMap((source) =>
    source.dailyForecasts.map((forecast) => forecast.date)
  )
  return Array.from(new Set(allDates)).sort()
})

const getSourceForecastForDay = (source: Source, day: string) => {
  return source.dailyForecasts.find((forecast) => forecast.date === day)
}

const daySourceForecasts = computed(() => {
  const result = new Map<string, Map<string, DailyForecast | undefined>>()
  days.value.forEach((day) => {
    const sourceMap = new Map<string, DailyForecast | undefined>()
    props.forecasts.sources.forEach((source) => {
      const forecast = getSourceForecastForDay(source, day)
      sourceMap.set(source.name, forecast)
    })
    result.set(day, sourceMap)
  })
  return result
})

const getFormattedDate = (date: string) => {
  const [year, month, day] = date.split('-').map(Number)
  const options = { weekday: 'short', day: 'numeric', month: 'short' }
  return new Date(year, month - 1, day).toLocaleDateString(
    'en-US',
    options as Intl.DateTimeFormatOptions
  )
}
</script>

<style scoped></style>
