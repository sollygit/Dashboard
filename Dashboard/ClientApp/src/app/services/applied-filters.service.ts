import { Injectable } from '@angular/core';
import { Subject } from 'rxjs';
import { AppliedFilter } from '../models/filter';

@Injectable()
export class AppliedFiltersService {
  private filterSource = new Subject<AppliedFilter>();
  filters$ = this.filterSource.asObservable();

  constructor() { }

  set(appliedFilter: AppliedFilter) {
    this.filterSource.next(appliedFilter);
  }

}
