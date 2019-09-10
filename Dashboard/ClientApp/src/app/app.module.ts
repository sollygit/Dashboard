import { HttpClientModule } from '@angular/common/http';
import { NgModule } from '@angular/core';
import { FormsModule } from '@angular/forms';
import { BrowserModule } from '@angular/platform-browser';
import { RouterModule } from '@angular/router';
import { NgbDateParserFormatter } from '@ng-bootstrap/ng-bootstrap';

import { RssFeedsService, SampleDataService } from './dashboard-api';
import { ClockService } from './services/clock.service';
import { UserService } from './services/user.service';
import { LocationService } from './services/location.service'
import { DeliveryOrderService } from './services/delivery-order.service'
import { AppliedFiltersService } from './services/applied-filters.service';
import { FilterService } from './services/filter.service';
import { SourceService } from './services/source.service';
import { NgbDateFormatterService } from './services/ngb-date-formatter.service';
import { ConfigurationService } from './services/configuration.service';
import { PagerService } from './services/pager.service';
import { MomentPipe } from './pipes/moment.pipe';
import { SpacerPipe } from './pipes/spacer.pipe';
import { BooleanPipe } from './pipes/boolean.pipe';
import { BreakLinePipe } from './pipes/break-line.pipe';

import { AppComponent } from './app.component';
import { CounterComponent } from './components/counter/counter.component';
import { FetchDataComponent } from './components/fetch-data/fetch-data.component';
import { RssFeedComponent } from './components/rss-feed/rss-feed.component';
import { HomeComponent } from './components/home/home.component';
import { NavMenuComponent } from './components/nav-menu/nav-menu.component';
import { ClockComponent } from './components/clock/clock.component';
import { GridComponent } from './components/grid/grid.component';
import { TelesalesViewComponent } from './components/telesales-view/telesales-view.component';

@NgModule({
  declarations: [
    AppComponent,
    NavMenuComponent,
    HomeComponent,
    CounterComponent,
    FetchDataComponent,
    RssFeedComponent,
    ClockComponent,
    GridComponent,
    TelesalesViewComponent,
    MomentPipe, SpacerPipe, BooleanPipe, BreakLinePipe
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
  providers: [
    ConfigurationService,
    RssFeedsService,
    SampleDataService,
    ClockService,
    UserService,
    LocationService,
    DeliveryOrderService,
    SourceService,
    FilterService,
    AppliedFiltersService,
    NgbDateFormatterService,
    PagerService,
    { provide: NgbDateParserFormatter, useClass: NgbDateFormatterService }
  ],
  bootstrap: [AppComponent]
})
export class AppModule { }
