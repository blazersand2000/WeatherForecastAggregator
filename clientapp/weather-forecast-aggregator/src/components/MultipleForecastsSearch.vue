<template>
  <v-container fluid style="max-width: 1400px">
    <v-row justify="center">
      <v-col style="max-width: 500px">
        <v-form @submit.prevent="search">
          <v-row>
            <v-col>
              <v-text-field
                autofocus
                v-model="searchTerm"
                :disabled="isFetching"
                :loading="isFetching"
                label="Search"
                hint="Search any place in the United States"
                variant="solo"
                single-line
                clearable
                append-icon="mdi-magnify"
                @click:append="search"
                @focus="$event.target.select()"
              ></v-text-field>
            </v-col>
          </v-row>
        </v-form>
      </v-col>
    </v-row>
    <v-row>
      <v-col>
        <v-alert v-if="error" type="error">{{ error }}</v-alert>
        <AggregatedForecasts v-if="forecasts" :forecasts="forecasts" />
      </v-col>
    </v-row>
  </v-container>
</template>

<script setup lang="ts">
import { ref } from 'vue'
import { useWeatherApi } from '@/composables/useWeatherApi'
import AggregatedForecasts from '@/components/AggregatedForecasts.vue'

const searchTerm = ref('')
const { isFetching, error, location, forecasts, getForecasts } = useWeatherApi()

const search = async () => {
  await getForecasts(searchTerm.value)
  if (location.value) {
    searchTerm.value = location.value.name
  }
}
</script>
