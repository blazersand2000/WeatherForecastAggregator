export interface ForecastsResponse {
  sources: Source[]
}

export interface Source {
  name: string
  attribution: Attribution
  dailyForecasts: DailyForecast[]
}

export interface Attribution {
  text: string
  url: string
  logoUrl: string
}

export interface DailyForecast {
  date: string
  highTemperatureF: number
  lowTemperatureF: number
  shortForecast: string
  probabilityOfPrecipitation: number
  windSpeed: number
}
