<template>
  <v-card>
    <v-card-title>{{ forecastSource.name }}</v-card-title>
    <v-card-subtitle
      >Attribution:
      <a :href="forecastSource.attribution.url">{{
        forecastSource.attribution.text
      }}</a></v-card-subtitle
    >
    <v-card-text>
      <v-list>
        <v-list-item v-for="(forecast, index) in forecastSource.dailyForecasts" :key="index">
          <v-list-item-title>{{ forecast.date }}</v-list-item-title>
          <v-card>
            <div class="text-subtitle-2">
              High: {{ forecast.highTemperatureF }}F, Low: {{ forecast.lowTemperatureF }}F
            </div>
            <div class="text-subtitle-2">Forecast: {{ forecast.shortForecast }}</div>
            <div class="text-subtitle-2">
              Probability of Precipitation: {{ forecast.probabilityOfPrecipitation }}%
            </div>
            <div class="text-subtitle-2">Wind Speed: {{ forecast.windSpeed }} mph</div>
          </v-card>
        </v-list-item>
      </v-list>
    </v-card-text>
  </v-card>
</template>

<script setup lang="ts">
import type { Source } from '@/types/ForecastsResponse'

defineProps<{
  forecastSource: Source
}>()
</script>

<style scoped></style>
