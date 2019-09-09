import { Pipe, PipeTransform } from '@angular/core';
import * as moment from 'moment';

@Pipe({
  name: 'moment'
})
export class MomentPipe implements PipeTransform {

  transform(value: string | Date, format: string = ""): string {
    if (!value || value === "")
      return "";
    return moment(value).format(format);
  }

}
