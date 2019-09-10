import { Component, OnInit, Input, Output, OnDestroy, EventEmitter, ViewChild, ElementRef, AfterViewInit } from '@angular/core';
import { Subscription } from 'rxjs';
import { delay } from 'rxjs/operators';
import { DeliveryOrderService } from '../../services/delivery-order.service';
import { SortSettings, sorterFactory, orderByMultiple, Comparer } from '../../models/sort-settings';
import { DeliveryOrder } from '../../models/delivery-order';

class DefaultComparer implements Comparer<DeliveryOrder> {

  constructor(private property: keyof DeliveryOrder) { }

  compare(a: DeliveryOrder, b: DeliveryOrder): number {
    if (!a[this.property] || !a[this.property]!.toString())
      return -1;
    if (!b[this.property] || !b[this.property]!.toString())
      return 1;
    if (typeof a[this.property] === 'number' && typeof b[this.property] === 'number')
      return Number(a[this.property]) - Number(b[this.property]);
    return a[this.property]!
      .toString()
      .localeCompare(b[this.property]!.toString());
  }
}

class PromiseComparer implements Comparer<DeliveryOrder> {
  compare(a: DeliveryOrder, b: DeliveryOrder): number {
    if (a.customerPromise === 'Specific Time' && b.customerPromise !== 'Specific Time')
      return 1;
    if (a.customerPromise !== 'Specific Time' && b.customerPromise === 'Specific Time')
      return -1;
    if (a.customerPromise === 'Specific Time' && b.customerPromise === 'Specific Time')
      return new DefaultComparer('requestDate').compare(a, b);
    if (a.customerPromise === 'AM' && b.customerPromise !== 'AM')
      return 1;
    if (a.customerPromise !== 'AM' && b.customerPromise === 'AM')
      return -1;
    if (a.customerPromise === 'PM' && b.customerPromise !== 'PM')
      return 1;
    if (a.customerPromise !== 'PM' && b.customerPromise === 'PM')
      return -1;
    if (a.customerPromise === 'Anytime' && b.customerPromise !== 'Anytime')
      return 1;
    if (a.customerPromise !== 'Anytime' && b.customerPromise === 'Anytime')
      return -1;
    return (a.customerPromise || '')
      .toString()
      .localeCompare(b.customerPromise || '');
  }
}

@Component({
  selector: 'grid',
  templateUrl: './grid.component.html',
  styleUrls: ['./grid.component.css']
})
export class GridComponent implements OnInit, OnDestroy, AfterViewInit {
  @Input() future: boolean = false;
  @Input() extended: boolean = false;
  @Input() maxHeight: number;
  @Output() onReloadOrders = new EventEmitter();
  @ViewChild('tableHeader', { static: false }) headerElement: ElementRef;
  bodyHeight: number;

  private subscription: Subscription;
  private sortSettings: SortSettings<DeliveryOrder> = new SortSettings<DeliveryOrder>();

  items: any = null;

  hasSpecificTime(row: DeliveryOrder): boolean {
    return row.customerPromise.toLowerCase() === 'specific time' || row.fulfilmentType.toLowerCase() === 'pickup';
  }

  constructor(private deliveryOrderService: DeliveryOrderService) { }

  ngOnInit() {
    this.subscription = this.deliveryOrderService.updated$
      .pipe(delay(2))
      .subscribe(() => this.update());
  }

  ngOnDestroy() {
    this.subscription.unsubscribe();
  }

  ngAfterViewInit() {
    this.bodyHeight = this.maxHeight - (this.headerElement.nativeElement.offsetHeight || 0);
  }

  update() {
    if (!this.sortSettings.column) {
      this.items = this.deliveryOrderService.filteredOrders
        .sort(orderByMultiple(
          { asc: false, comparer: new DefaultComparer('requestDate') },
          { asc: false, comparer: new DefaultComparer('fulfilmentType') },
          { asc: false, comparer: new PromiseComparer() }
        ));
    } else {
      this.items = this.sort(this.deliveryOrderService.filteredOrders);
    }
  }

  changeSort(property: keyof DeliveryOrder) {
    if (this.sortSettings.column === property) {
      this.sortSettings.ascending = !this.sortSettings.ascending;
    } else {
      this.sortSettings.column = property;
      this.sortSettings.ascending = false;
    }
    this.items = this.sort(this.items);
  }

  sort(items: Array<DeliveryOrder>): Array<DeliveryOrder> {
    if (this.sortSettings.column)
      return items.sort(sorterFactory(new DefaultComparer(this.sortSettings.column), this.sortSettings.ascending));
    return items;
  }

  isSortAsc(property: string) {
    return this.sortSettings.column === property && this.sortSettings.ascending;
  }

  isSortDesc(property: string) {
    return this.sortSettings.column === property && !this.sortSettings.ascending;
  }

  statusColour(status: string): Array<string> {
    const classes = new Array<string>();
    if (status !== 'Not Started') {
      classes.push('font-weight-bold');
    }
    switch (status) {
      case 'Being Picked':
        classes.push('status-being-picked');
        break;
      case 'Part Picked':
      case 'Missed Delivery':
        classes.push('status-part-picked');
        break;
      case 'Pick Complete':
      case 'On Route':
        classes.push('status-pick-complete');
        break;
      case 'Packed':
      case 'Delivered':
        classes.push('status-packed');
        break;
    }
    return classes;
  }
}
