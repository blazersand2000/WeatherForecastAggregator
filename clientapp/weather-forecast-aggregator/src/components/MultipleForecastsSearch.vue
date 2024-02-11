<template>
  <v-row>
    <v-col>
      <v-form @submit.prevent="search">
        <v-row>
          <v-col cols="12" md="6">
            <v-text-field v-model="searchTerm" label="Search" required></v-text-field>
          </v-col>
          <v-col cols="12" md="6">
            <v-btn type="submit" color="primary">Search</v-btn>
          </v-col>
        </v-row>
      </v-form>
    </v-col>
  </v-row>
  <v-row>
    <v-col>
      <v-progress-circular v-if="isFetching" indeterminate></v-progress-circular>
      <v-alert v-if="error" type="error">{{ error }}</v-alert>
      <AggregatedForecasts v-if="forecasts" :forecasts="forecasts" />
    </v-col>
  </v-row>
</template>

<script setup lang="ts">
import { ref, watch } from 'vue'
import { useWeatherApi } from '@/composables/useWeatherApi'
import AggregatedForecasts from '@/components/AggregatedForecasts.vue'

const baseUrl = 'https://localhost:7004/api/WeatherForecast'

const searchTerm = ref('')
const { isFetching, error, forecasts, getForecasts } = useWeatherApi()

const search = async () => {
  await getForecasts(searchTerm.value)
}
</script>
