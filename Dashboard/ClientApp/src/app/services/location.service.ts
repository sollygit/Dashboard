import { Injectable } from '@angular/core';
import { UserService } from './user.service';
import { Location } from '../models/location';
import { Observable, ReplaySubject } from 'rxjs';
import { map } from 'rxjs/operators';

@Injectable()
export class LocationService {
  private allLocations: Observable<Array<Location>>;
  private locationSubject = new ReplaySubject<Array<Location>>();

  get branches(): Observable<Array<Location>> {
    return this.allLocations
      .pipe(map(locations => locations.filter(location => !location.isDepot).sort((a, b) => a.name.localeCompare(b.name))));
  }

  get locations(): Observable<Array<Location>> {
    return this.locationSubject.asObservable();
  }

  constructor(private userService: UserService) {
    this.allLocations = this.userService.locations();
    this.allLocations.subscribe(locations => this.locationSubject.next(locations.sort((a, b) => a.locationId - b.locationId)));
  }

  changeBranch(branch: Location | undefined) {
    let locations = branch ?
      this.allLocations
        .pipe(map(locations => locations.filter(location => location.tradingAs === (branch as Location).tradingAs))) :
      this.allLocations;
    locations.subscribe(locs => {
      this.locationSubject.next(locs.sort((a, b) => a.locationId - b.locationId));
    });
  }

}
