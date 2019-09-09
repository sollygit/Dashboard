import { Component, OnInit } from '@angular/core';
import { SampleDataService, WeatherForecast } from 'src/app/dashboard-api';

@Component({
  selector: 'app-fetch-data',
  templateUrl: './fetch-data.component.html'
})
export class FetchDataComponent implements OnInit {
  forecasts: WeatherForecast[] = [];

  constructor(private service: SampleDataService) { }

  public ngOnInit() {
    this.service.weatherForecasts().subscribe(result => {
      this.forecasts = result;
    }, error => console.error(error));
  }
}
