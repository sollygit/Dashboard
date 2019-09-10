import { Injectable } from '@angular/core';
import { ClockService } from './clock.service';
import { Pager, PagerRow } from '../models/pager';

@Injectable()
export class PagerService {

  constructor(private clockService: ClockService) { }

  create<T extends PagerRow>(lifeSpan: number, lineHeight: number, maxHeight: number): Pager<T> {
    return new Pager<T>(this.clockService, lifeSpan, lineHeight, maxHeight);
  }
}
