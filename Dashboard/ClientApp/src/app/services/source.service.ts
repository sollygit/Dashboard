import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { map, share } from 'rxjs/operators';
import { Source } from '../models/source';

@Injectable()
export class SourceService {
  sources: Observable<Array<Source>>;

  constructor(private http: HttpClient) {
    this.sources = this.http.get('api/source')
      .pipe(map((res:any) => res.json().map(Source.fromJson)))
      .pipe(share());
  }

}
