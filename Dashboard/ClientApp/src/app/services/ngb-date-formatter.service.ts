import { Injectable } from '@angular/core';
import { NgbDateParserFormatter, NgbDateStruct } from '@ng-bootstrap/ng-bootstrap';
import * as moment from 'moment';

@Injectable()
export class NgbDateFormatterService extends NgbDateParserFormatter {
  parse(value: string): NgbDateStruct {
    if (value) {
      let date = moment(value);
      return {
        day: date.get('day'),
        month: date.get('month'),
        year: date.get('year')
      };
    }
    return <any>null;
  }

  format(date: NgbDateStruct): string {
    return date ? `${this.pad(date.day)}-${this.pad(date.month)}-${date.year}` : '';
  }

  private pad(value: number): string {
    return value < 10 ? `0${value}` : `${value}`;
  }
}
