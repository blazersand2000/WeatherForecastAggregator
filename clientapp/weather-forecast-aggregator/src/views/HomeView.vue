<script setup lang="ts">
import MultipleForecastsSearch from '@/components/MultipleForecastsSearch.vue'
</script>

<!-- <MultipleForecastsSearch /> -->

<!-- <v-container fluid>
    <v-row>
      <v-col cols="6" sm="2">
        <v-card>
          <v-card-title>Card 1</v-card-title>
          <v-card-text>This is card number 1</v-card-text>
        </v-card>
        <v-card>
          <v-card-title>Card 3</v-card-title>
          <v-card-text>This is card number 3</v-card-text>
        </v-card>
      </v-col>
      <v-col cols="6" sm="2">
        <v-card>
          <v-card-title>Card 2</v-card-title>
          <v-card-text>This is card number 2</v-card-text>
        </v-card>
        <v-card>
          <v-card-title>Card 4</v-card-title>
          <v-card-text>This is card number 4</v-card-text>
        </v-card>
      </v-col>
    </v-row>
  </v-container> -->

<template>
  <MultipleForecastsSearch />
  <!-- <v-container fluid style="max-width: 1400px">
   
    <v-row>
      <v-col cols="0" md="2"></v-col>
      <v-col v-for="source in sources" :key="source" class="text-center">
        <h3>{{ source }}</h3>
      </v-col>
    </v-row>

    <v-row v-for="day in days" :key="day">
      <v-col cols="12" md="1" class="d-flex align-center justify-center">
        <h5>{{ day }}</h5>
      </v-col>
      <v-col v-for="source in sources" :key="`${day}-${source}`">
        <v-card style="min-width: 323px">
          <v-card-title>{{ day }} - {{ source }}</v-card-title>
          <v-card-text>Forecast data...</v-card-text>
        </v-card>
      </v-col>
    </v-row>
  </v-container> -->
</template>

<!-- <script setup lang="ts">
import { ref } from 'vue'

const sources = ref<string[]>(['NWS', 'OpenWeatherMap'])
const days = ref<string[]>(['Thursday', 'Friday', 'Saturday', 'Sunday', 'Monday'])
</script>

<style scoped>
.flex {
  flex-grow: 1;
}
</style> -->
