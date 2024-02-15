<template>
  <v-card>
    <v-row align="center">
      <v-col>
        <v-card-title>
          <span class="text-h5"> {{ high }}</span>
          <span class="text-h6"> /{{ low }}</span>
        </v-card-title>
      </v-col>

      <v-spacer></v-spacer>

      <v-col>
        <v-card-subtitle>{{ sourceName }}</v-card-subtitle>
      </v-col>
    </v-row>
    <v-card-text class="py-0">{{ shortForecast }}</v-card-text>
    <div class="d-flex py-3 justify-space-between">
      <v-list-item density="compact">
        <v-list-item-subtitle>Wind</v-list-item-subtitle>
        <v-list-item-title>{{ windSpeed }}</v-list-item-title>
      </v-list-item>

      <v-list-item density="compact">
        <v-list-item-subtitle>% Precip.</v-list-item-subtitle>
        <v-list-item-title>{{ pop }}</v-list-item-title>
      </v-list-item>
    </div>
  </v-card>
</template>

<script setup lang="ts">
import type { DailyForecast } from '@/types/ForecastsResponse'
import { computed } from 'vue'

const props = defineProps<{
  sourceName: string
  dailyForecast: DailyForecast
}>()

const high = computed(() => `${Math.round(props.dailyForecast.highTemperatureF)}°F`)
const low = computed(() => `${Math.round(props.dailyForecast.lowTemperatureF)}°F`)
const shortForecast = computed(() => `${props.dailyForecast.shortForecast}`)
const pop = computed(() => `${Math.round(props.dailyForecast.probabilityOfPrecipitation * 100)}`)
const windSpeed = computed(() => `${props.dailyForecast.windSpeed} Mph`)
</script>

<style scoped></style>
