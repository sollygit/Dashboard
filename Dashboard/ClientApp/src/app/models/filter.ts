import { Observable } from 'rxjs';
import { DeliveryOrder } from 'src/app/dashboard-api';

export type FilterType = 'Date' | 'Select' | 'Text' | 'MultiSelect';

export interface FilterOption {
  displayText: string;
  value: any;
}

export interface FilterFactory {
  (value?: any): (order: DeliveryOrder) => boolean;
}

export interface Filter {
  displayText: string;
  property: string;
  type: FilterType;
  factory: FilterFactory;
  options?: Observable<Array<FilterOption>>;
  value?: any;
  inputProps?: object;
}

export interface AppliedFilter {
  property: string;
  selector: (item: DeliveryOrder) => boolean;
}
