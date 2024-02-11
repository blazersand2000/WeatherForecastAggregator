<template>
  <v-row>
    <v-col cols="12" sm="6" md="4" v-for="(source, index) in forecasts.sources" :key="index">
      <DailyForecast :forecastSource="source" />
    </v-col>
  </v-row>
</template>

<script setup lang="ts">
import type { ForecastsResponse } from '@/types/ForecastsResponse'
import DailyForecast from '@/components/DailyForecast.vue'

defineProps<{
  forecasts: ForecastsResponse
}>()
</script>

<style scoped></style>
