import { Component, ElementRef, OnDestroy, OnInit, ViewChild } from '@angular/core';
import { ActivatedRoute } from '@angular/router';
import { DeliveryOrderService } from '../../services/delivery-order.service';

@Component({
  selector: 'telesales-view',
  templateUrl: './telesales-view.component.html',
  styleUrls: ['./telesales-view.component.css']
})
export class TelesalesViewComponent implements OnInit, OnDestroy {
  future: boolean = false;
  extended: boolean = false;
  maxHeight: number;

  @ViewChild('header', { static: false }) headerElement: ElementRef;

  get title(): string {
    let title = 'Orders';
    return title;
  }

  constructor(
    private route: ActivatedRoute,
    private orderService: DeliveryOrderService) { }

  ngOnInit() {
    this.orderService.start();
    this.route.url.subscribe(segments => {
      this.future = false;
      this.extended = false;
    });
  }

  ngOnDestroy() {
    this.orderService.stop();
  }
}
