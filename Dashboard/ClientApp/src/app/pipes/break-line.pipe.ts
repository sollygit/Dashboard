import { Pipe, PipeTransform } from '@angular/core';
import { DomSanitizer } from '@angular/platform-browser';

@Pipe({
  name: 'breakLine'
})
export class BreakLinePipe implements PipeTransform {

  constructor(private domSanitizer: DomSanitizer) {}

  transform(value: string | String[]): any {
    if (!value)
      return value;
    if (value instanceof Array)
      return value.join('<br />');
    return value.replace(/\r?\n/g, '<br />');
  }

}
