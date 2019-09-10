import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { ReplaySubject, Observable } from 'rxjs';
import { map } from 'rxjs/operators';
import { Location } from '../models/location';

@Injectable()
export class UserService {
  private basePath = 'api/user';
  private cachedLocations = new ReplaySubject<Array<Location>>();
  private cachedStatisticsGuard = new ReplaySubject<boolean>();

  constructor(private http: HttpClient) {
    this.refreshLocations();
    this.refreshStatisticsGuard();
  }

  locations(): Observable<Array<Location>> {
    return this.cachedLocations.asObservable();
  }

  canViewStatistics(): Observable<boolean> {
    return this.cachedStatisticsGuard.asObservable();
  }

  refreshLocations() {
    this.http.get([this.basePath, 'locations'].join('/'))
      .pipe(map(res => { res as Array<any> }))
      .pipe(map(l => { Location.fromJS(l) }))
      .subscribe(locations => {
        this.cachedLocations.next(locations as any)
      });
  }

  refreshStatisticsGuard() {
    this.http.get([this.basePath, 'viewstatistics'].join('/'))
      .pipe(map((res: any) => res.json))
      .subscribe(guard => {
        this.cachedStatisticsGuard.next(guard)
      });
  }

}
