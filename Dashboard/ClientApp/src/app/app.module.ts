import { HttpClientModule } from '@angular/common/http';
import { NgModule } from '@angular/core';
import { FormsModule } from '@angular/forms';
import { BrowserModule } from '@angular/platform-browser';
import { RouterModule } from '@angular/router';

import { RssFeedsService, SampleDataService } from './dashboard-api';
import { ClockService } from './services/clock.service';
import { ConfigurationService } from './services/configuration.service';
import { MomentPipe } from './pipes/moment.pipe';

import { AppComponent } from './app.component';
import { CounterComponent } from './components/counter/counter.component';
import { FetchDataComponent } from './components/fetch-data/fetch-data.component';
import { RssFeedComponent } from './components/rss-feed/rss-feed.component';
import { HomeComponent } from './components/home/home.component';
import { NavMenuComponent } from './components/nav-menu/nav-menu.component';
import { ClockComponent } from './components/clock/clock.component';


@NgModule({
  declarations: [
    AppComponent,
    NavMenuComponent,
    HomeComponent,
    CounterComponent,
    FetchDataComponent,
    RssFeedComponent,
    ClockComponent,
    MomentPipe
  ],
  imports: [
    BrowserModule.withServerTransition({ appId: 'ng-cli-universal' }),
    HttpClientModule,
    FormsModule,
    RouterModule.forRoot([
      { path: '', component: HomeComponent, pathMatch: 'full' },
      { path: 'counter', component: CounterComponent },
      { path: 'fetch-data', component: FetchDataComponent },
      { path: 'rss-feed', component: RssFeedComponent }
    ])
  ],
  providers: [ConfigurationService, RssFeedsService, SampleDataService, ClockService ],
  bootstrap: [AppComponent]
})
export class AppModule { }
