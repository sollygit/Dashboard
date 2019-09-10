import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Router } from '@angular/router';
import { Observable, BehaviorSubject, Subscription, of } from 'rxjs';
import { map, filter, delay, switchMap, tap, finalize, catchError } from 'rxjs/operators';
import { ClockService } from './clock.service';
import { AppliedFiltersService } from './applied-filters.service';
import { DeliveryOrder } from '../models/delivery-order';
import { AppliedFilter } from '../models/filter';
import * as _ from 'lodash';
import * as moment from 'moment';

@Injectable()
export class DeliveryOrderService {
  orders: Array<DeliveryOrder> = [];
  filteredOrders: Array<DeliveryOrder> = [];
  private filters: Array<AppliedFilter> = [];
  private updateSource = new BehaviorSubject<boolean>(false);
  private subscription: Subscription | undefined;
  private basePath = 'api/statistics';
  private pollingSubject = new BehaviorSubject<Date | null>(null);

  updated$ = this.updateSource
    .asObservable()
    .pipe(filter(x => x));

  constructor(
    private http: HttpClient,
    private router: Router,
    private clockService: ClockService,
    private filterService: AppliedFiltersService
  ) { }

  start() {
    this.stop();
    this.subscription = this.filterService.filters$.subscribe(filter => this.applyFilters(filter));
    this.subscription.add(
      this.clockService.updateStream$.subscribe(({ current }) => this.setEtaWarning(current))
    );
    const initialLoad$ = this.initialLoad();
    const polling$ = this.pollingSubject
      .pipe(filter(since => !!since))
      .pipe(tap(since => localStorage.setItem('lastUpdate', since!.toISOString())))
      .pipe(delay(15000))
      .pipe(switchMap(since => this.apiCall([since!.toISOString()])))
      .pipe(tap(() => this.pollingSubject.next(new Date())));
    this.subscription.add(
      polling$.subscribe(orders => this.update(orders))
    );
    this.subscription.add(
      initialLoad$.subscribe(orders => this.initialiseOrders(orders))
    );
  }

  stop() {
    if (this.subscription)
      this.subscription.unsubscribe();
    this.filters = [];
    this.orders = [];
    this.filteredOrders = [];
    this.pollingSubject.next(null);
  }

  retrieveNationalOrderStatuses(): Observable<Array<{ fulfilmentType: string, complete: number }>> {
    return this.http.get([this.basePath, 'national'].join('/'))
      .pipe(map((res: any) => (res.json() as Array<any>).map(item => {
        return {
          fulfilmentType: item.FulfilmentType,
          complete: item.CompletedOrders
        };
      })));
  }

  private initialiseOrders(orders: Array<DeliveryOrder>) {
    this.orders = orders
      .filter(order => order.pickStatus.toLowerCase() !== 'cancelled');
    localStorage.setItem('orders', JSON.stringify(orders));
    this.applyFilters();
  }

  private update(orders: Array<DeliveryOrder>) {
    if (orders.length < 1)
      return;
    orders.forEach(order => {
      var index = this.orders.findIndex(e => e.deliveryOrderId === order.deliveryOrderId && e.transCode === order.transCode && e.branchId === order.branchId);
      if (index !== -1 && order.pickStatus.toLowerCase() === 'cancelled') {
        this.orders.splice(index, 1);
      } else if (index !== -1) {
        this.orders[index] = order;
      } else if (order.pickStatus.toLowerCase() !== 'cancelled') {
        this.orders.push(order);
      }
    });
    localStorage.setItem('orders', JSON.stringify(this.orders));
    this.applyFilters();
  }

  private applyFilters(filter?: AppliedFilter) {
    if (filter) {
      let index = this.filters.findIndex(f => f.property === filter.property);
      if (index === -1)
        this.filters.push(filter);
      else
        this.filters[index] = filter;
    }
    this.filteredOrders = this.orders;
    if (this.filteredOrders && this.filters)
      this.filters.forEach(filter => this.filteredOrders = this.filteredOrders.filter(filter.selector as any));
    this.updateSource.next(true);
  }

  private initialLoad(): Observable<Array<DeliveryOrder>> {
    const lastUpdate = localStorage.getItem('lastUpdate'),
      storedOrders = localStorage.getItem('orders');
    if (lastUpdate && storedOrders) {
      return of(JSON.parse(storedOrders))
        .pipe(map((items: Array<any>) => items.map((order: any) => DeliveryOrder.fromJson(order))))
        .pipe(map(items => items.map((order: DeliveryOrder) => this.fromPackageNotes(order))))
        .pipe(finalize(() => this.pollingSubject.next(new Date(lastUpdate))));
    }
    return this.apiCall().pipe(finalize(() => this.pollingSubject.next(new Date())));
  }

  private apiCall(fragments?: Array<any>): Observable<Array<DeliveryOrder>> {
    let path = fragments || [];
    path.unshift(this.basePath);
    return this.http.get(path.join('/'))
      .pipe(map((res: any) => res.map((order: any) => DeliveryOrder.fromJson(order))))
      .pipe(map(orders => orders.map((order: DeliveryOrder) => this.fromPackageNotes(order))))
      .pipe(catchError(e => {
        console.log(e);
        if (e.status === 401)
          this.router.navigateByUrl('/unauthorised');
        return Observable.throw(e);
      }));
  }

  private setEtaWarning(current: Date) {
    const now = moment(current);
    this.orders
      .filter(order => ['collected', 'delivered'].indexOf(order.pickStatus.toLowerCase()) < 0)
      .forEach(order => {
        if (!order.warnings.some(w => w === 'ETA')) {
          const pickupDate = moment(order.pickupDateTime);
          const requestDate = moment(order.requestDate);
          if (
            (
              order.fulfilmentType.toLowerCase() === 'pickup'
              && (
                (order.pickStatus.toLowerCase() === 'not started' && pickupDate.subtract(20, 'm').isBefore(now))
                || (order.pickStatus.toLowerCase() !== 'packed' && pickupDate.subtract(10, 'm').isBefore(now))
              )
            ) || (
              order.fulfilmentType.toLowerCase() === 'delivery'
              && order.pickStatus.toLowerCase() !== 'delivered' && requestDate.isBefore(now, 'day')
            )
          ) {
            order.warnings.push('ETA');
          }
        }
      });
  }

  private fromPackageNotes(order: DeliveryOrder): DeliveryOrder {
    var pickers = [];
    var packaging = [];
    var stagingAreas = [];
    var groupsByPicker = _.groupBy(order.packageNotes, x => x.packer);
    for (var picker in groupsByPicker) {
      var index = 0;
      if (groupsByPicker.hasOwnProperty(picker)) {
        var groupsByPackage = _.groupBy(groupsByPicker[picker], x => x.packaging);
        for (var pkg in groupsByPackage) {
          if (groupsByPackage.hasOwnProperty(pkg)) {
            var groupsByArea = _.groupBy(groupsByPackage[pkg], x => x.stagingArea);
            for (var area in groupsByArea) {
              if (groupsByArea.hasOwnProperty(area)) {
                pickers.push(index++ == 0 ? picker : '');
                packaging.push(`${groupsByArea[area].length} x ${pkg}`);
                stagingAreas.push(`${groupsByArea[area].length} x ${area}`);
              }
            }
          }
        }
      }
    }
    return Object.assign(order, {
      pickers: pickers,
      packaging: packaging,
      stagingAreas: stagingAreas
    });
  }
}
