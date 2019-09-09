import { Component, OnInit, OnDestroy } from '@angular/core';
import { ClockService } from '../../services/clock.service';
import { Subscription } from 'rxjs';

@Component({
  selector: 'clock',
  templateUrl: './clock.component.html',
  styleUrls: ['./clock.component.css']
})
export class ClockComponent implements OnInit, OnDestroy {
  date: Date;
  private subscription: Subscription;

  constructor(private clockService: ClockService) { }

  ngOnInit() {
    this.subscription = this.clockService.start().subscribe(dateUpdate => this.update(dateUpdate));
  }

  ngOnDestroy() {
    this.subscription.unsubscribe();
    this.clockService.stop();
  }

  update(dateUpdate: { previous: Date, current: Date }) {
    this.date = dateUpdate.current;
  }
}
