import { Injectable } from '@angular/core';
import { Observable, ReplaySubject } from 'rxjs';
import { ConfigurationService } from 'src/app/services/configuration.service';

@Injectable()
export class ClockService {
  private updateSource = new ReplaySubject<{ previous: Date, current: Date }>(1);
  private date: Date;
  private timerId?: any;

  updateStream$ = this.updateSource.asObservable();

  constructor(private config: ConfigurationService) { }

  start(): Observable<{ previous: Date, current: Date }> {
    if (this.timerId)
      return this.updateStream$;
    let now = new Date();
    this.updateSource.next({ previous: now, current: now });
    let delay = this.config.reloadInterval;
    this.date = new Date(now.getFullYear(), now.getMonth(), now.getDate(), now.getHours(), now.getMinutes(), now.getSeconds() - now.getSeconds() % 10);
    setTimeout(() => {
      this.timerId = setInterval(() => this.update(), delay);
      this.update();
    }, delay - new Date().getSeconds() * 1000);

    return this.updateStream$;
  }

  stop() {
    if (this.timerId)
      clearInterval(this.timerId);
    this.timerId = undefined;
  }

  update() {
    let now = new Date();
    this.updateSource.next({ previous: this.date, current: now });
    this.date = now;
  }
}
