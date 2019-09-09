import { Pipe, PipeTransform } from '@angular/core';

@Pipe({
  name: 'spacer'
})
export class SpacerPipe implements PipeTransform {

  transform(value: string): string {
    return value.replace(/([A-Z])/g, ' $1');
  }

}
